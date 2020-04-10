using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Earless.WebApi.Services;
using Earless.WebApi.Data;
using Earless.WebApi.Interfaces;

namespace Earless.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IConfiguration configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        { 
            string webApiConnectionString = configuration.GetValue<string>("ConnectionStrings:EarlessWebApiDBConnection");

            Func<string, Action<DbContextOptionsBuilder>> optionActionCreator = connectionString =>
            {
                return options => options.UseSqlServer(connectionString);
            };
            // Bind options using the miscellaneous sub-section of the appsettings.json file.
            services.Configure<InitializationOptions>(configuration.GetSection("Initialization"));

            services.AddLogging();
            services.AddDbContext<EarlessContext>(optionActionCreator(webApiConnectionString));
            services.AddScoped<DbInitializer>();
            services.AddScoped<Mapper>();
            services.AddScoped<IOrderLineService, OrderLineService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddControllers();

            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("AllowSpecificOrigin", policy =>
                {
                    policy.WithOrigins(configuration.GetSection("Web:SchemeAndHost").Value)
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowSpecificOrigin");

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
