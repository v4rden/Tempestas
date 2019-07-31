﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Tempestas.WebApi
{
    using System.Reflection;
    using Application.Infrastructure;
    using Application.Interfaces;
    using Application.Weather;
    using Infrastructure;
    using MediatR;

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
            services.AddMediatR(typeof(GetCurrentWeatherByCityNameHandler).GetTypeInfo().Assembly);

            services.AddTransient<IWeatherClient, ApixuWeatherClient>();
            services.AddTransient<IWeatherProvider, TempestasWeatherProvider>();
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            services.AddSwaggerDocument();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseOpenApi();
            app.UseSwaggerUi3(options =>
                {
                    // Define web UI route
                    options.Path = "/swagger";

                    // Define OpenAPI/Swagger document route (defined with UseSwaggerWithApiExplorer)
                    options.DocumentPath = "/swagger/v1/swagger.json";
                }
                );
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseMvc(routes =>
                        {
                            routes.MapRoute(
                                name: "default",
                                template: "{controller}/{action=Index}/{id?}");
                        });
        }
    }
}