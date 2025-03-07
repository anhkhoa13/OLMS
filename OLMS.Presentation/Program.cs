using OLMS.Application;
using OLMS.Domain.Repositories;
using OLMS.Infrastructure;
using OLMS.Infrastructure.Database;

namespace OLMS.Presentation;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);

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

        app.UseAuthorization();


        app.MapControllerRoute(
            name: "default",
            pattern: $"{{controller={defaultController}}}/{{action={defaultAction}}}/{{id?}}");



        app.Run();
    }
}
