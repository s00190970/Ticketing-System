using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TicketingSystem.Core.Helpers;
using TicketingSystem.Core.Services;
using TicketingSystem.Database.Context;
using TicketingSystem.Database.IRepositories;
using TicketingSystem.Database.Repositories;

namespace TicketingSystem.Api
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
            services.AddDbContextPool<DatabaseContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("DbConnectionString"),
                b => b.MigrationsAssembly("TicketingSystem.Api")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddAutoMapper(typeof(Startup));

            services.AddTransient<PriorityRepository>();
            services.AddTransient<PriorityService>();

            services.AddTransient<ServiceTypeRepository>();
            services.AddTransient<ServiceTypeService>();

            services.AddTransient<StatusRepository>();
            services.AddTransient<StatusService>();

            services.AddTransient<TicketTypeRepository>();
            services.AddTransient<TicketTypeService>();

            services.AddTransient<SettingRepository>();
            services.AddTransient<SettingService>();

            services.AddTransient<TicketRepository>();
            services.AddTransient<TicketService>();

            services.AddTransient<TicketConverter>();
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

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
