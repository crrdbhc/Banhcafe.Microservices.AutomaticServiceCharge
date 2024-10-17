using Banhcafe.Microservices.ServiceChargingSystem.Api.Endpoints.Filters;
using Banhcafe.Microservices.ServiceChargingSystem.Api.Options;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Contracts.Response;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Subscriptions.Queries;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Subscriptions.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace Banhcafe.Microservices.ServiceChargingSystem.Api.Endpoints;

public static class SubscriptionsEndpoints {
    public static IEndpointRouteBuilder AddSubscriptionsEndpoints(this IEndpointRouteBuilder endpoints) 
    {
        var versionSet = endpoints
            .NewApiVersionSet()
            .HasApiVersion(new(1) { })
            .ReportApiVersions()
            .Build();

        var backOfficeEndpoints = endpoints
            .MapGroup("/backOffice/subscriptions")
            .AddEndpointFilter<FeatureGateEndpointFilter>()
            .WithTags("Subscriptions")
            .WithApiVersionSet(versionSet)
            .RequireAuthorization(AuthenticationPolicy.BackOffice);

        _ = backOfficeEndpoints
           .MapGet(
               "",
               static async (
                   IFeatureManager features,
                   IMediator mediator,
                   [FromQuery] int? coreClientId,
                   [FromQuery] int? terminalId,
                   [FromQuery] int? page,
                   [FromQuery] int? size
               ) =>
               {
                   var result = await mediator.Send(new ListSubscriptionsQuery { CoreClientId = coreClientId });

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
           .WithDisplayName("GetSubscriptionsByUser")
           .WithName("GetSubscriptionsByUser")
           .WithMetadata(new FeatureGateAttribute("BOF-show_subscriptions_by_user"))
           .ProducesProblem(StatusCodes.Status400BadRequest)
           .ProducesProblem(StatusCodes.Status500InternalServerError)
           .ProducesValidationProblem();

        _ = backOfficeEndpoints
            .MapPost(
                "",
                static async (
                    IFeatureManager features,
                    IMediator mediator,
                    CreateSubscriptionCommand request
                ) => 
                {
                    var result = await mediator.Send(request);

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
            .WithDisplayName("PostUserSubscriptions")
            .WithName("PostUserSubscriptions")
            .WithMetadata(new FeatureGateAttribute("BOF-create_user_subscription"))
            .Produces<ApiResponse<CreateSubscriptionCommand>>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem();

        return endpoints;
    }
}