using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using OLMS.Application;
using OLMS.Application.Services;
using OLMS.Infrastructure;
using OLMS.Presentation.Services;
using System.Text;

namespace OLMS.Presentation;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container
        builder.Services.AddControllersWithViews();
        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<JwtHttpClientHandler>();
        builder.Services.AddHttpClient("ApiClient", options =>
        {
            var apiUrl = builder.Configuration["ApiSettings:BaseUrl"];
            if (string.IsNullOrWhiteSpace(apiUrl)) throw new ArgumentNullException("ApiSettings:BaseUrl", "Missing BaseUrl configuration");

            options.BaseAddress = new Uri(apiUrl);
        }).AddHttpMessageHandler<JwtHttpClientHandler>();

        // Cấu hình Cookie Authentication
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Authentication/Index";
                options.AccessDeniedPath = "/Shared/AccessDenied";
            });
        builder.Services.AddAuthorization();

        var app = builder.Build();

        // Get default values from configuration
        var defaultController = builder.Configuration["DefaultPage:Controller"] ?? "Authentication";
        var defaultAction = builder.Configuration["DefaultPage:Action"] ?? "Login";

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
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

        app.MapControllerRoute(
            name: "MyArea",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        app.MapControllerRoute(
            name: "default",
            pattern: $"{{controller={defaultController}}}/{{action={defaultAction}}}/{{id?}}");

        app.Run();
            
    }
}

