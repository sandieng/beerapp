using BeerRosterAPI.Entities;
using BeerRosterAPI.ViewModels;
using BeerRosterAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BeerRosterAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["ConnectionStrings:BeerRosterDBConnectionString"];
            services.AddDbContext<BeerRosterContext>(o => o.UseSqlServer(connectionString));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        // builder.WithOrigins("http://localhost:8083")
                        builder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials();
                    }
                    );
            });

            // var key = Encoding.ASCII.GetBytes("401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1");
            var key = Encoding.ASCII.GetBytes(Configuration["JwtToken:EncryptionKey"]);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(x =>
             {
                 x.RequireHttpsMetadata = false;
                 x.SaveToken = true;
                 x.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(key),
                     ValidateIssuer = false,
                     ValidateAudience = false
                 };
             });


            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IRosterService, RosterService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped(typeof(IBeerRosterRepository<>), typeof(BeerRosterRepository<>));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowSpecificOrigin");

            //app.UseMiddleware<JwtMiddleware>();

            app.UseAuthentication();

            app.UseMvc();

            // AutoMapper
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<RosterVM, Roster>();
                cfg.CreateMap<Roster, RosterVM>();
                cfg.CreateMap<MemberVM, Member>();
                cfg.CreateMap<Member, MemberVM>();

                //cfg.CreateMap<Roster, Member>()
                //    .ForMember(m => m.ID, o => o.MapFrom(s => s.Member.ID));

                cfg.CreateMap<Roster, RosterVM>()
                    .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.Member.FirstName))
                    .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.Member.LastName))
                    .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Member.Email))
                    .ForMember(d => d.DateJoined, opt => opt.MapFrom(s => s.Member.DateJoined))
                    .ForMember(d => d.IsActive, opt => opt.MapFrom(s => s.Member.IsActive));
            });

        }
    }
}
