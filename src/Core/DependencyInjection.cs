using System.Reflection;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core;
public static class DependencyInjection
{
    public static IServiceCollection RegisterCoreServices(this IServiceCollection services)
    {
        _ = services.AddMediatR(cfg =>
        {
            _ = cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            _ = cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            _ = cfg.AddBehavior(
                typeof(IPipelineBehavior<,>),
                typeof(UnhandledExceptionBehaviour<,>)
            );
        });

        return services;
    }
}
