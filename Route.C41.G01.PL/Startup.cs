using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Route.C41.G01.BBL.Interfaces;
using Route.C41.G01.BBL.Repositories;
using Route.C41.G01.DAL.Data;
using Route.C41.G01.DAL.Models;
using Route.C41.G01.PL.Extentions;
using Route.C41.G01.PL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Route.C41.G01.PL
{
    public class Startup
    {
        public IConfiguration Configuration { get; } = null;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the DI container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(); // Register Built-In Services Required by MVC

            //services.AddTransient<ApplicationDbContext>();
            //services.AddSingleton<ApplicationDbContext>();

            //services.AddScoped<ApplicationDbContext>();
            //services.AddScoped<DbContextOptions<ApplicationDbContext>>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }, ServiceLifetime.Scoped /*By Default*/);


            //ApplicationServicesExtention.AddApplicationServices(services);
            // or
            
            services.AddApplicationServices(); // Extention Method 

            services.AddAutoMapper(M=>M.AddProfile(new MappingProfiles()));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            ///services.AddScoped<UserManager<ApplicationUser>>();
            ///services.AddScoped<SignInManager<ApplicationUser>>();
            ///services.AddScoped<RoleManager<IdentityRole>>();


            services.AddIdentity<ApplicationUser, IdentityRole>( // Allow [ DI ] for three Main Services ( User Manager - Role Manager - SignInManager )
                options =>
                {
                    options.Password.RequiredUniqueChars = 2;
                    options.Password.RequireDigit = true;
                    options.Password.RequireNonAlphanumeric = true; // @#$%
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequiredLength = 5; // Min Length


                    options.Lockout.AllowedForNewUsers = true;
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(5);


                    //options.User.AllowedUserNameCharacters = "asfavaewfvavavsc";

                    options.User.RequireUniqueEmail = true;

                }).AddEntityFrameworkStores<ApplicationDbContext>();

           // services.AddAuthentication(); // Called By Default when I Use => [ services.AddIdentity ]


		}

            ///services.AddScoped<UserManager<ApplicationUser>>();
            ///services.AddScoped<SignInManager<ApplicationUser>>();
            ///services.AddScoped<RoleManager<IdentityRole>>();


            services.AddIdentity<ApplicationUser, IdentityRole>();

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
