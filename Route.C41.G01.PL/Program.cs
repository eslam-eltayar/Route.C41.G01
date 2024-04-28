using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Route.C41.G01.BBL.Interfaces;
using Route.C41.G01.BBL.Repositories;
using Route.C41.G01.DAL.Data;
using Route.C41.G01.DAL.Models;
using Route.C41.G01.PL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Route.C41.G01.PL.Extentions;
using Route.C41.G01.PL.Services;

namespace Route.C41.G01.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region configure services

            var WebApplicationBuilder = WebApplication.CreateBuilder(args);
            WebApplicationBuilder.Services.AddControllersWithViews();

            //services.AddScoped<ApplicationDbContext>();
            //services.AddScoped<DbContextOptions<ApplicationDbContext>>();

            WebApplicationBuilder.Services.AddDbContext<ApplicationDbContext>(
                options => {
                    options.UseSqlServer(WebApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
                }/*,ServiceLifetime.Singleton*/);
            //ApplicationServicesExtensions.AddApplicationServices(services); Static Method

            //Extention Method
            WebApplicationBuilder.Services.AddApplicationServices();

            WebApplicationBuilder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
            WebApplicationBuilder.Services.AddIdentity<ApplicationUser, IdentityRole>(
                options =>
                {
                    options.Password.RequiredLength = 5;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;

                    options.Lockout.AllowedForNewUsers = true;
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(2);

                    options.User.RequireUniqueEmail = true;
                }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            WebApplicationBuilder.Services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Home/Error";
                options.LoginPath = "/Account/SignIn";
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
            });
            WebApplicationBuilder.Services.AddAuthentication();
            WebApplicationBuilder.Services.AddTransient<IEmailSender, EmailSender>();

            var app = WebApplicationBuilder.Build();
            #endregion

            #region configre kestrel middlewares

            if (WebApplicationBuilder.Environment.IsDevelopment())
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
            #endregion

            app.Run();
        }
    }
}

