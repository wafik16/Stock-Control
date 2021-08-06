﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockMVC.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockMVC.Models;

namespace StockMVC
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDefaultIdentity<IdentityUser>()
            //    .AddRoles<IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentity<IdentityUser, IdentityRole>()
                 .AddDefaultUI()
                 .AddEntityFrameworkStores<ApplicationDbContext>()
                 .AddDefaultTokenProviders();




            services.AddAuthorization(options =>
            {
                //options.AddPolicy("allowpolicy",
                //    builder => builder.RequireRole("Admin", "Manager"));
                //options.AddPolicy("writepolicy",
                //    builder => builder.RequireRole("wholesale", "Retail"));
                //options.AddPolicy("deletepolicy",
                //    builder => builder.RequireRole("Manager"));
                //options.AddPolicy("editpolicy",
                //    builder => builder.RequireRole("Account"));
                options.AddPolicy("adminpolicy",
                    builder => builder.RequireUserName("wafik16@yahoo.co.uk"));

                options.AddPolicy("writepolicy", policy =>
                  policy.RequireRole("wholesale", "Retail"));

                options.AddPolicy("allowpolicy", policy =>
                  policy.RequireRole("Admin", "Manager"));

                options.AddPolicy("deletepolicy", policy =>
                  policy.RequireRole("Manager"));

                options.AddPolicy("editpolicy", policy =>
                  policy.RequireRole("Account"));

            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

         
        }
    }
}
