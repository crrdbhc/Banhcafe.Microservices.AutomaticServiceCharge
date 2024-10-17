using Banhcafe.Microservices.ServiceChargingSystem.Api.Endpoints.Filters;
using Banhcafe.Microservices.ServiceChargingSystem.Api.Options;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Contracts.Response;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Popups.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace Banhcafe.Microservices.ServiceChargingSystem.Api.Endpoints;

public static class PopupsEndpoints
{
    public static IEndpointRouteBuilder AddPopupsEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var versionSet = endpoints
            .NewApiVersionSet()
            .HasApiVersion(new(1) { })
            .ReportApiVersions()
            .Build();

        var backOfficeEndpoints = endpoints
            .MapGroup("/backOffice/popups")
            .AddEndpointFilter<FeatureGateEndpointFilter>()
            .WithTags("Popups")
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
                    var result = await mediator.Send(new ListPopupsQuery { CoreClientId = coreClientId });

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
            .WithDisplayName("Popups")
            .WithName("Popups")
            .WithMetadata(new FeatureGateAttribute("BOF-show_popups_by_user"))
            .Produces<ApiResponse<ListPopupsQuery>>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem();
        return endpoints;
    }
}
