using AspNetCore.JwtAuthentication.PasswordHasing.Plugin;
using DinkToPdf;
using DinkToPdf.Contracts;
using Jewellery.ReportHelper.IReportServices;
using Jewellery.ReportHelper.ReportService;
using JewelleryStore.DataAccess;
using JewelleryStore.DataAccess.Domain.Context;
using JewelleryStore.DataAccess.UnitOfWork;
using JewelleryStoreAPI.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryStoreAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, Microsoft.Extensions.Hosting.IHostEnvironment env)
        {
            Configuration = configuration;
            _hostingEnvironment = env;
        }
        private readonly Microsoft.Extensions.Hosting.IHostEnvironment _hostingEnvironment;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped<DataContext>(e => new DataContext(fullPath));
            services.AddDbContext<JewelleryStoreDBContext>(options => options.UseInMemoryDatabase(databaseName: "JewelleryStore"));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
           
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed(_ => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders(new string[] { "content-disposition" }));

            });
            services.AddScoped<ITokenService, TokenService>();
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddScoped<IReportService, ReportService>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "JewelleryStoreAPI", Version = "v1" });
            });
            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = Configuration["Tokens:Issuer"],
                       ValidAudience = Configuration["Tokens:Issuer"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])),
                       ClockSkew = TimeSpan.Zero,
                   };
                   options.Events = new JwtBearerEvents
                   {
                       OnTokenValidated = context =>
                       {
                           var userName = context.Principal.Identity.Name;
                           //var user = userService.GetById(userId);
                           if (userName == null)
                           {
                               // return unauthorized if user no longer exists
                               context.Fail("Unauthorized");
                           }
                           return Task.CompletedTask;
                       },
                       OnAuthenticationFailed = context =>
                       {
                           var a = context;
                           return Task.CompletedTask;
                       }
                   };
               });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JewelleryStoreAPI v1"));
            }

            app.UseCors("CorsPolicy");

            app.UseExceptionHandler(
             options => {
                 options.Run(
                 async context =>
                 {
                     context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                     context.Response.ContentType = "text/html";
                     var ex = context.Features.Get<IExceptionHandlerFeature>();
                     if (ex != null)
                     {
                         var err = $"<h1>Error: {ex.Error.Message}</h1>{ex.Error.StackTrace }";
                         await context.Response.WriteAsync(err).ConfigureAwait(false);
                     }
                 });
             }
            );


            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
