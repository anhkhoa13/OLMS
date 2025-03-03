using OLMS.Application;
using OLMS.Application.Feature.CourseUC;
using OLMS.Domain.Repositories;
using OLMS.Infrastructure;
using OLMS.Infrastructure.Database.Repositories;

namespace OLMS.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            {
                builder.Services.AddControllersWithViews();

                builder.Services.AddMediatR(configuration =>
                {
                    configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
                });
                builder.Services.AddApplication();

                /*builder.Services.AddScoped<IUserRepository, UserRepository>();
                builder.Services.AddScoped<ICourseRepository, CourseRepository>();*/

                builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCourseCommandHandler).Assembly));
                builder.Services.AddInfrastructure(builder.Configuration);
            }
            // Add services to the container.
            

            var app = builder.Build();
            {
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
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                app.Run();
            }
            
        }
    }
}
