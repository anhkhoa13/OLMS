using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OLMS.Application.Services;
using OLMS.Domain.Repositories;

namespace OLMS.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {   
        services.AddScoped<IMaterialRepository>();
        services.AddScoped<ICourseMaterialRepository>();
        var assembly = typeof(DependencyInjection).Assembly;
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
        });

        services.AddValidatorsFromAssembly(assembly);

        services.AddSingleton<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();
        builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();

        //Register Discussion Service
        services.AddSingleton<IDiscussionService, DiscussionService>();

        return services;
    }
}
