using Banhcafe.Microservices.AutomaticServiceCharge.Api.Endpoints.Filters;
using Banhcafe.Microservices.AutomaticServiceCharge.Api.Options;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.UserServices.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Api.Endpoints;

public static class UserServicesEndpoints {
    public static IEndpointRouteBuilder AddUserServicesEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var versionSet = endpoints
            .NewApiVersionSet()
            .HasApiVersion(new(1) {})
            .ReportApiVersions()
            .Build();
        
        var backOfficeEndpoints = endpoints
            .MapGroup("/backOffice")
            .AddEndpointFilter<FeatureGateEndpointFilter>()
            .WithTags("UserServices")
            .WithApiVersionSet(versionSet)
            .RequireAuthorization(AuthenticationPolicy.BackOffice);

         _ = backOfficeEndpoints
            .MapGet(
                "/users/{coreClientId}/services/{serviceId}",
                static async(
                    IFeatureManager features,
                    IMediator mediator,
                    [FromQuery] int? coreClientId,
                    [FromQuery] int? serviceId,
                    [FromQuery] int? terminalId,
                    [FromQuery] int? page,
                    [FromQuery] int? size
                ) => 
                {
                    var result = await mediator.Send(new ListUserServicesQuery { CoreClientId = coreClientId, ServiceId = serviceId });
                    
                    if (result.ValidationErrors.Count > 0)
                    {
                        return Results.BadRequest(result);
                    }

                    if (result.Errors.Count > 0)
                    {
                        return Results.BadRequest(result);
                    }

                    return Results.Ok(result);
                }
            )
            .WithDisplayName("GetSubscriptionsByService")
            .WithName("GetSubscriptionsByService")
            .WithMetadata(new FeatureGateAttribute("BOF-show_subscriptions_by_service"))
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem();
        
        return endpoints;
    }
}