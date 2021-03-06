using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using DataAccessLayer.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using RetirementAgeAPI.Middlewares;
using Microsoft.AspNetCore.Http;
using System.IO;
using RetirementAgeAPI.Extensions;
using RetirementAgeAPI.AutoMapper;

namespace RetirementAgeAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //Inject services 
            services.AddServices(Configuration);
            // Register AutoMapper 
            services.ConfigureAutoMapper();

            services.AddControllers();
            services.AddAuthentication(Configuration);

            ////read the secret for Authorization 
            //var appSettingSection = Configuration.GetSection("AppSettings");
            //services.Configure<AppSettings>(appSettingSection);
            //var appSettings = appSettingSection.Get<AppSettings>();
            //var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(x =>
            //{
            //    x.RequireHttpsMetadata = false;
            //    x.SaveToken = true;
            //    x.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(key),
            //        ValidateIssuer = false,
            //        ValidateAudience = false
            //    };
            //});
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("user", new AuthorizationPolicyBuilder()
            //        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
            //        .RequireAuthenticatedUser()
            //        .Build());
            //});

            //add logging
            services.AddLogging(builder =>
               builder
                   .AddDebug()
                   .AddConsole()
                   .AddConfiguration(Configuration.GetSection("Logging"))
                   .SetMinimumLevel(LogLevel.Information)
           );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RetirementAgeAPI", Version = "v1" });

                //Locate the XML file being generated by ASP.NET...
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                //... and tell Swagger to use those XML comments.
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RetirementAgeAPI v1"));
            }

            //exception handling
            app.UseExceptionHandlerMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //var mapper = MapperProvider.GetMapper();
            //app.AddSingleton<IMapper>(mapper);

        }
    }

    //jwt key
    public class AppSettings
    {
        public string Secret { get; set; }
    }
}
