using System.Security.Cryptography;
using Banhcafe.Microservices.AutomaticServiceCharge.Api.Endpoints.Filters;
using Banhcafe.Microservices.AutomaticServiceCharge.Api.Options;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Contracts.Response;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.UserPopups.Command.Create;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.UserPopups.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Api.Endpoints;

public static class UserPopupsEndpoints
{
    public static IEndpointRouteBuilder AddUserPopupsEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var versionSet = endpoints
            .NewApiVersionSet()
            .HasApiVersion(new(1) { })
            .ReportApiVersions()
            .Build();

        var backOfficeEndpoints = endpoints
            .MapGroup("/backOffice")
            .AddEndpointFilter<FeatureGateEndpointFilter>()
            .WithTags("UserPopups")
            .WithApiVersionSet(versionSet)
            .RequireAuthorization(AuthenticationPolicy.BackOffice);

        _ = backOfficeEndpoints
            .MapGet(
                "/users/{coreClientId}/popups/",
                static async (
                    IFeatureManager features,
                    IMediator mediator,
                    [FromQuery] int? coreClientId,
                    [FromQuery] int? terminalId,
                    [FromQuery] int? page,
                    [FromQuery] int? size
                ) =>
                {
                    var result = await mediator.Send(new ListUserPopupsQuery { CoreClientId = coreClientId });

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
            .WithDisplayName("UserPopups")
            .WithName("UserPopups")
            .WithMetadata(new FeatureGateAttribute("BOF-show_popups_by_user"))
            .Produces<ApiResponse<ListUserPopupsQuery>>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem();

        _ = backOfficeEndpoints
            .MapPost(
                "/popups/hide",
                static async (
                    IFeatureManager features,
                    IMediator mediator,
                    HideUserPopupCommand request
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
            .WithDisplayName("HideUserPopup")
            .WithName("HideUserPopup")
            .WithMetadata(new FeatureGateAttribute("BOF-hide_user_popup"))
            .Produces<ApiResponse<HideUserPopupCommand>>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem();
            
        return endpoints;
    }
}
