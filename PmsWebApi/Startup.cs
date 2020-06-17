using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using PmsWebApi.Models;
using Swashbuckle.AspNetCore;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.OpenApi.Models;

namespace PmsWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var contact = new OpenApiContact()
            {
                Name = "Mbonisi Tshuma",
                Email = "mbonisitshuma287@gmail.com",
                Url = new Uri("http://www.example.com")
            };

            var license = new OpenApiLicense()
            {
                Name = "My License",
                Url = new Uri("http://www.example.com")
            };

            var info = new OpenApiInfo()
            {
                Version = "v1",
                Title = "Property Management System API",
                Description = "Using Swagger API Management Tool to display the structrue of the API",
                //TermsOfService = new Uri("http://www.example.com"),
                //Contact = contact,
                //License = license
            };

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", info);
                //c.SwaggerDoc("v2", info2);
            });



        services.AddDbContext<UsersContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultPMSConnection")));
            services.AddDbContext<TenantContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("DefaultPMSConnection")));
            services.AddDbContext<TenantLeaseContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("DefaultPMSConnection")));
            services.AddDbContext<TenantLeaseRenewalContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("DefaultPMSConnection")));
            services.AddDbContext<TenantLeaseTerminationContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultPMSConnection")));
            services.AddDbContext<RentContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("DefaultPMSConnection")));
            services.AddDbContext<MaintenanceContext>(options =>
         options.UseSqlServer(Configuration.GetConnectionString("DefaultPMSConnection")));
            services.AddDbContext<NotificationsContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultPMSConnection")));

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins, builder =>
                {
                    builder.WithOrigins("https://pmswebapi-dev.azurewebsites.net/",
                                          "https://pmswebapi-dev.azurewebsites.net/api/tenant",
                                          "https://pmswebapi-dev.azurewebsites.net/api/tenantlease",
                                          "https://pmswebapi-dev.azurewebsites.net/api/tenantleaserenewal",
                                          "https://pmswebapi-dev.azurewebsites.net/api/tenantleasetermination",
                                          "https://pmswebapi-dev.azurewebsites.net/api/maintenance",
                                          "https://pmswebapi-dev.azurewebsites.net/api/notifications",
                                          "https://pmswebapi-dev.azurewebsites.net/api/rent",
                                          "https://pmswebapi-dev.azurewebsites.net/api/users").AllowAnyHeader().AllowAnyMethod();

                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                "Property Management System API");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
