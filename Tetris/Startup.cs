using BL;
using DAL;
using Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ORMDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tetris
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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                options =>
                {
                    options.LoginPath = new PathString("/Users/Login");
                    options.AccessDeniedPath = new PathString("/Users/Login");
                    options.ExpireTimeSpan = new TimeSpan(7, 0, 0, 0);
                });

            services.AddAuthorization();
            services.AddControllersWithViews();

            //services.AddSingleton<IUsersDal>(new UsersDal());

            // Два варианта:
            // 1. services.AddTransient<IUsersDal, UsersDal>();
            // 2. services.AddTransient<IUsersDal, OrmUsersDal>();

            services.AddTransient<IUsersDal, OrmUsersDal>();
            services.AddTransient<IUsersBL, UsersBL>();

            services.AddTransient<IGamesDal, OrmGamesDal>();
            services.AddTransient<IGamesBL, GamesBL>();

            //services.AddScoped<IUsersDal, UsersDal>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}