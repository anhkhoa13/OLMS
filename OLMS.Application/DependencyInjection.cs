using Microsoft.Extensions.DependencyInjection;
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

        return services;
    }
}
