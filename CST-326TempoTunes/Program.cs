using CST_326TempoTunes.Services.Business;
using CST_326TempoTunes.Services.DataAccess;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CST_326TempoTunes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Register DAO and Service
            builder.Services.AddSingleton<PlaylistDAO>();
            builder.Services.AddSingleton<PlaylistCollection>();
            builder.Services.AddSingleton<UserDAO>();       // one instance for the whole app
            builder.Services.AddScoped<UserCollection>();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login";
                    options.LogoutPath = "/Login/Logout";
                });

            builder.Services.AddAuthorization();

            var app = builder.Build();
            app.UseAuthentication();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
