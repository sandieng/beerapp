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
            var connectionString = Configuration["connectionStrings:beerRosterDBConnectionString"];
            services.AddDbContext<BeerRosterContext>(o => o.UseSqlServer(connectionString));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors(options => {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => {
                        // builder.WithOrigins("http://localhost:8083")
                        builder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                        }
                    );
            });

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

            app.UseMiddleware<JwtMiddleware>();

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
