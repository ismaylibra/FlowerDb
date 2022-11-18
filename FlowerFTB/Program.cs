using FlowerFTB.DAL;
using FlowerFTB.Data;
using FlowerFTB.Models.IdentityModels;
using FlowerFTB.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;

namespace FlowerFTB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddMvc();
            builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddIdentity<User, IdentityRole>(opt =>
            {
                opt.SignIn.RequireConfirmedEmail = false;

                opt.User.RequireUniqueEmail = true;

                opt.Password.RequiredLength = 6;

                opt.Password.RequireUppercase = false;

                opt.Password.RequireLowercase = false;
            })
                 .AddEntityFrameworkStores<AppDbContext>()
                 .AddDefaultTokenProviders();
            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
            builder.Services.AddTransient<IMailService, MailManager>();
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();    
            }
            else
            {
                app.UseExceptionHandler("/ErrorPage");
            }
            app.UseStatusCodePagesWithReExecute("/ErrorPage/ErrorAction", "?code={0}");

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(

                name: "areas",
                pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });













            app.Run();
        }
    }
}