using System.Net.Security;
using System.Reflection;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Behaviours;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Popups.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Popups.Validators;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.PopupTypes.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.PopupTypes.Validators;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Services.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Services.Validators;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.UserPopups.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.UserPopups.Validators;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.UserSubscriptions.Models;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.UserSubscriptions.Validators;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Core;
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

        _ = services.AddAutoMapper(c => c.AddProfile(new UserPopups.Mapper.AutoMapperProfile()));
        _ = services.AddAutoMapper(c => c.AddProfile(new UserSubscriptions.Mapper.AutoMapperProfile()));
        _ = services.AddAutoMapper(c => c.AddProfile(new UserServices.Mapper.AutoMapperProfile()));
        _ = services.AddAutoMapper(c => c.AddProfile(new Services.Mapper.AutoMapperProfile()));
        _ = services.AddAutoMapper(c => c.AddProfile(new Popups.Mapper.AutoMapperProfile()));
        _ = services.AddAutoMapper(c => c.AddProfile(new PopupTypes.Mapper.AutoMapperProfile()));
        _ = services.AddAutoMapper(c => c.AddProfile(new Currencies.Mapper.AutoMapperProfile()));
        _ = services.AddAutoMapper(c => c.AddProfile(new RenewalTypes.Mapper.AutoMapperProfile()));

        // Create validators
        services.AddScoped<IValidator<CreateUserSubscriptions>, CreateUserSubscriptionsValidator>();
        services.AddScoped<IValidator<HideUserPopup>, HideUserPopupsValidator>();
        services.AddScoped<IValidator<CreatePopupType>, CreatePopupTypesValidator>();
        services.AddScoped<IValidator<CreateServices>, CreateServicesValidator>();
        services.AddScoped<IValidator<CreatePopup>, CreatePopupsValidator>();

        return services;
    }
}
