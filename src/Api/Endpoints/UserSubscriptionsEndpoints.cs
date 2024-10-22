using Banhcafe.Microservices.AutomaticServiceCharge.Api.Endpoints.Filters;
using Banhcafe.Microservices.AutomaticServiceCharge.Api.Options;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Contracts.Response;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.UserSubscriptions.Queries;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.UserSubscriptions.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Api.Endpoints;

public static class UserSubscriptionsEndpoints {
    public static IEndpointRouteBuilder AddUserSubscriptionsEndpoints(this IEndpointRouteBuilder endpoints) 
    {
        var versionSet = endpoints
            .NewApiVersionSet()
            .HasApiVersion(new(1) { })
            .ReportApiVersions()
            .Build();

        var backOfficeEndpoints = endpoints
            .MapGroup("/backOffice/")
            .AddEndpointFilter<FeatureGateEndpointFilter>()
            .WithTags("UserSubscriptions")
            .WithApiVersionSet(versionSet)
            .RequireAuthorization(AuthenticationPolicy.BackOffice);

        _ = backOfficeEndpoints
           .MapGet(
               "/users/{coreClientId}/subscriptions",
               static async (
                   IFeatureManager features,
                   IMediator mediator,
                   [FromQuery] int? coreClientId,
                   [FromQuery] int? terminalId,
                   [FromQuery] int? page,
                   [FromQuery] int? size
               ) =>
               {
                   var result = await mediator.Send(new ListUserSubscriptionsQuery { CoreClientId = coreClientId });

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
                "/subscriptions",
                static async (
                    IFeatureManager features,
                    IMediator mediator,
                    CreateUserSubscriptionCommand request
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
            .Produces<ApiResponse<CreateUserSubscriptionCommand>>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem();

        return endpoints;
    }
}