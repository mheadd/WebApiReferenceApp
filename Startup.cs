using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using WebApiReferenceApp.Models;

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
            var server = Environment.GetEnvironmentVariable("SERVER") ?? "localhost";
            var port = Environment.GetEnvironmentVariable("PORT") ?? "1401";
            var user = Environment.GetEnvironmentVariable("SQLUSER") ?? "SA";
            var password = Environment.GetEnvironmentVariable("SQLSERVER_PASSWORD") ?? "BaldEagle123";
            var db_name = Environment.GetEnvironmentVariable("DBNAME") ?? "TestDB";
            var connString = $"Server={server},{port};Database={db_name};User ID={user};Password={password};";
            services.AddDbContext<ApiContext>(options => options.UseSqlServer(connString));

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
            app.UseCors("CorsPolicy");
            app.UseMvc();
        }
    }
}
