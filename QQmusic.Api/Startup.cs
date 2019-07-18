using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using QQmusic.Api.Extensions;
using QQmusic.Infrastructure.Extensions;
using Serilog;

namespace QQmusic.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(Path.Combine("logs", @"log.txt"), rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //将appsettings.json中的JwtSettings部分文件读取到JwtSettings中，这是给其他地方用的
            //services.Configure<JwtSettings>(Configuration.GetSection("JWT"));

            services.AddMvc(options =>
            {
                options.ReturnHttpNotAcceptable = true;

            }).AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            })
            .AddFluentValidation();
            services.AddMediaTypes();
            //services.AddApiVersioning(option =>
            //{
            //    option.ReportApiVersions = true;
            //    option.AssumeDefaultVersionWhenUnspecified = true;
            //    option.DefaultApiVersion = new ApiVersion(1, 0);
            //    option.ErrorResponses = new MyVersionErrorResponseProvider();
            //});

            //services.AddMyAuthentication();
            //services.AddMyAuthorization();

            //services.AddDbContext<MyContext>(
            //    options =>
            //    {
            //        options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection"));

            //        options.ConfigureWarnings(
            //            w => w.Ignore(CoreEventId.IncludeIgnoredWarning));
            //    });

            //services.AddCaches(Configuration);

            //重定向https
            //services.AddHttpsRedirection(options =>
            //{
            //    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
            //    options.HttpsPort = 5001;
            //});

            //注册需要创建uri的服务
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(factory =>
            {
                var actionContext = factory.GetService<IActionContextAccessor>().ActionContext;
                return new UrlHelper(actionContext);
            });
            //设置跨域
            services.AddCors();
            //注册资源映射关系 MappingProfile
            services.AddAutoMapper();
            //校验资源
            services.AddFluentValidators();
            //注册映射关系
            services.AddPropertyMappings();
            //注册数据仓库
            services.AddRepositories();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseMyExceptionHandler(loggerFactory);
                app.UseHsts();
            }

            app.UseCors(builder =>
            {
                //builder.WithOrigins("http://www.example.com");
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowCredentials();
                //builder.WithExposedHeaders(new string[] { "count" });
            });
            //app.UseHttpsRedirection();
            app.UseStatusCodeHandling();
            app.UseAuthentication();

            app.UseStaticFiles("/wwwroot");
            app.UseMvc();
        }
    }
}
