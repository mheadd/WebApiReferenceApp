using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using WebApiReferenceApp.Models;
using WebApiReferenceApp.Connectors;

namespace WebApiReferenceApp
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
            // SQL Server access credentials.
            var server = Environment.GetEnvironmentVariable("SERVER") ?? "localhost";
            var port = Environment.GetEnvironmentVariable("PORT") ?? "1401";
            var user = Environment.GetEnvironmentVariable("SQLUSER") ?? "SA";
            var password = Environment.GetEnvironmentVariable("SQLSERVER_PASSWORD") ?? "BaldEagle123";
            var db_name = Environment.GetEnvironmentVariable("DBNAME") ?? "TestDB";
            
            var connString = $"Server={server},{port};Database={db_name};User ID={user};Password={password};";
            services.AddDbContext<ApiContext>(options => options.UseSqlServer(connString));

            // Dependency Injection for RestController.
            var rest_endpoint = Environment.GetEnvironmentVariable("REST_URI") ?? "https://catalog.data.gov";
            services.AddSingleton<IRestConnector>(new RestConnector(new Uri(rest_endpoint)));

            // Define CORS policy.
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Apply CORS policy.
            app.UseCors("CorsPolicy");
            app.UseMvc();
        }
    }
}
