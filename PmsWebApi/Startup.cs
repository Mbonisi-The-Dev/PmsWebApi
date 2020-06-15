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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
