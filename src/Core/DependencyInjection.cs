using System.Reflection;
using System.Security.Cryptography;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Behaviours;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Subscriptions.Models;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Subscriptions.Validators;
using FluentValidation;
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

        _ = services.AddAutoMapper(c => c.AddProfile(new Popups.Mapper.AutoMapperProfile()));
        _ = services.AddAutoMapper(c => c.AddProfile(new Subscriptions.Mapper.AutoMapperProfile()));
        _ = services.AddAutoMapper(c => c.AddProfile(new Services.Mapper.AutoMapperProfile()));

        // Create validators
        services.AddScoped<IValidator<CreateSubscriptions>, CreateSubscriptionsValidator>();

        return services;
    }
}
