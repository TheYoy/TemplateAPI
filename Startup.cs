using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APITemplate.DependencyInjecyions;
using APITemplate.DependencyInjecyions.Microservices;
using APITemplate.Process;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Reflection;
using Microsoft.OpenApi.Models;

namespace APITemplate
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
            services.AddControllers();
            services.AddCommon();
            services.AddSwagger();
            services.AddMicroservices();
            services.AddScoped<apiProcess>();
            services.AddScoped<Microservices>();
            services.AddIserviceClient(Configuration);
            
            // services.AddControllersWithViews()
            //     .AddJsonOptions(options =>
            //     options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            // );
        //    services.AddRazorPages();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
