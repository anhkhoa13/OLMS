using Microsoft.Extensions.DependencyInjection;
using OLMS.Application.Services;
using OLMS.Domain.Repositories;

namespace OLMS.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {   
        var assembly = typeof(DependencyInjection).Assembly;
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
        });
        services.AddSingleton<IJwtService, JwtService>();
        return services;
    }
}
