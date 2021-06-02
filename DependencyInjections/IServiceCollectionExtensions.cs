using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using APITemplate.Helper;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using System.IO;
using APITemplate.DependencyInjecyions.Microservices.Lottery;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace APITemplate.DependencyInjecyions
{
    internal static class IServiceCollectionExtensions
    {
        // internal static void AddClient(this IServiceCollection services,IConfiguration config)
        // {
        //     // var httpClient = services.AddHttpClient();

        //     services.AddHttpClient("tempClient", c =>
        //     {  
        //         c.DefaultRequestHeaders.Add("Accept", "application/json");
        //         // Github requires a user-agent
        //         c.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
        //     });
        // }
        internal static void AddCommon(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
            services.AddSingleton<DependencyInjectionHelper>();
        }

        internal static void AddSwagger(this IServiceCollection services)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fileVersionInfo.ProductVersion;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = $"Portal API v{version}",
                    Version = $"v{version}",
                    Description = "Portal API",
                    TermsOfService = new Uri("http://uspaces.in.th"),
                    Contact = new OpenApiContact
                    {
                        Name = "TheYoy",
                        Email = string.Empty,
                        Url = new Uri("https://www.facebook.com/pee.thongchutthesthim/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under uspaces",
                        Url = new Uri("http://uspaces.in.th"),
                    }
                });
            });
        }
        internal static void AddMicroservices(this IServiceCollection services)
        {
            services.AddScoped<lotteryAPI>();
        }

        internal static void AddIserviceClient(this IServiceCollection services,IConfiguration config)
        {
            var httpClientBuilder = services.AddHttpClient("serviceclient");
            httpClientBuilder.ConfigurePrimaryHttpMessageHandler(() => {
                var serviceHeadersHandler = new IServiceHeadersHandler();

                return serviceHeadersHandler;
            });
        }
    }
}
