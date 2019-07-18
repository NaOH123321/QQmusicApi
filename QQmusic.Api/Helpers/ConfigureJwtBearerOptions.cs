using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QQmusic.Api.Messages;
using QQmusic.Core.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace QQmusic.Api.Helpers
{
    public class ConfigureJwtBearerOptions : IConfigureNamedOptions<JwtBearerOptions>
    {
        public IConfiguration Configuration { get; }

        public ConfigureJwtBearerOptions(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Configure(JwtBearerOptions options)
        {
            Configure(string.Empty, options);
        }

        public void Configure(string name, JwtBearerOptions options)
        {
            if (name == JwtBearerDefaults.AuthenticationScheme)
            {
                options.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Query["access_token"];
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenInvalidSignatureException) ||
                            context.Exception.GetType() == typeof(ArgumentException))
                        {
                            context.HttpContext.Response.Headers.Add("X-Error",
                                ErrorCodeStatus.ErrorCode40009.ToString());
                            return Task.CompletedTask;
                        }

                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.HttpContext.Response.Headers.Add("X-Error",
                                ErrorCodeStatus.ErrorCode40010.ToString());
                            return Task.CompletedTask;
                        }
                        return Task.CompletedTask;
                    }
                };

                var serverSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:ServerSecret"]));
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Configuration["JWT:Issuer"],
                    ValidAudience = Configuration["JWT:Audience"],
                    IssuerSigningKey = serverSecret,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            }
        }
    }
}