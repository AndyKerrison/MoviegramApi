using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MoviegramApi.Data;
using Swashbuckle.AspNetCore.Swagger;

namespace MoviegramApi
{
    /// <summary>
    /// Startip class for the web api. Most settings have been left as default. We have added settings for Swagger, and an EF Core database connection
    /// </summary>
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //in a production environment, our connection string would be held in a secret store, accessed via e.g certificate authentication,
            //AWS roles assigned to the instance this code is running on, or something similar.
            string connectionString = "Data Source=moviegram.cmotchajvce8.us-east-2.rds.amazonaws.com;Initial Catalog=moviegram;User id=sa;Password=moviegram1!;";

            services.AddEntityFrameworkSqlServer().AddDbContext<MoviegramContext>(options =>
                options.UseSqlServer(connectionString)
            );

            // Register the Swagger generator
            // Comments are taken from the Xml published during the build process (enabled in VS build settings)
            // Launch settings have also been adjusted to load the swagger doc when the solution is run.
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Moviegram Api", Version = "v1" });
                c.IncludeXmlComments(string.Format(@"{0}\MoviegramApi.XML", System.AppDomain.CurrentDomain.BaseDirectory));
            });
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

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Moviegram Api v1");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
