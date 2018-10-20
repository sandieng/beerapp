using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BeerRosterAPI.Services
{
    public class JwtMiddleware
    {
        private const string METHOD_POST = "post";
        private const string LOGIN_PATH = "/api/member/login";
        private const string SIGNUP_PATH = "/api/member/signup";

        private RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var httpMethod = context.Request.Method.ToLower();
            var apiPath = context.Request.Path.Value.ToLower();

            // Check for login and signup
            if (httpMethod == METHOD_POST && (apiPath == LOGIN_PATH || apiPath == SIGNUP_PATH))
            {
                // Let the respected method in MemberController to handle JwtToken generation
                await _next(context);
            }
            else
            {
                // Extend the JwtToken expiry timestamp for all other api calls
                var authHeader = context.Request.Headers["Authorization"];

                if (!string.IsNullOrEmpty(authHeader))
                {
                    var jwtToken = JwtService.DecodeJwt(authHeader);
                    var userName = jwtToken.Payload["userName"].ToString();

                    if (!string.IsNullOrEmpty(userName))
                    {
                        // This is how to get the required service in middleware. Earlier, tried to inject into the constructor but failed
                        IMemberService memberService = context.RequestServices.GetService(typeof(IMemberService)) as IMemberService;

                        var memberExist = memberService.GetByEmail(userName);
                        if (memberExist != null)
                        {
                            var extendedToken = JwtService.UpdateJwt(authHeader);
                            var keyValue = new KeyValuePair<string, StringValues>("jwtToken", extendedToken);
                            context.Response.Headers.Add(keyValue);
                            await _next(context);
                        }
                        else
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        }
                    }
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                }
            }
        }
    }
}
