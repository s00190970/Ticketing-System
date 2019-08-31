using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using TicketingSystem.Core.Helpers;
using TicketingSystem.Core.JWT;
using TicketingSystem.Core.Services;
using TicketingSystem.Database.Context;
using TicketingSystem.Database.Entities;
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

            ConfigureJwt(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "TicketingSystem API", Version = "v1" });
            });

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

            services.AddTransient<UserRepository>();
            services.AddTransient<UserService>();

            services.AddTransient<TicketConverter>();
            services.AddTransient<UserConverter>();

            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<DatabaseContext>();

            services.AddTransient<JwtFactory>();

            services.AddMvc().AddJsonOptions(options =>
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local);

            services.AddCors();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        private void ConfigureJwt(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Configuration.GetSection("JwtOptions").GetValue<string>("Issuer"),
                    ValidAudience = Configuration.GetSection("JwtOptions").GetValue<string>("Audience"),
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration.GetSection("JwtOptions").GetValue<string>("Key"))),
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager, DatabaseContext context)
        {
            app.Use(async (ctx, next) =>
            {
                await next();
                if (ctx.Response.StatusCode == 204)
                {
                    ctx.Response.ContentLength = 0;
                }
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TicketingSystem API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();

            DatabaseInitializer.Initialize(userManager, roleManager, context).Wait();
        }
    }
}
