using Banhcafe.Microservices.ServiceChargingSystem.Api.Endpoints.Filters;
using Banhcafe.Microservices.ServiceChargingSystem.Api.Options;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Contracts.Response;
using Banhcafe.Microservices.ServiceChargingSystem.Core.PopupTypes.Commands.Create;
using Banhcafe.Microservices.ServiceChargingSystem.Core.PopupTypes.Commands.Update;
using Banhcafe.Microservices.ServiceChargingSystem.Core.PopupTypes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace Banhcafe.Microservices.ServiceChargingSystem.Api.Endpoints;

public static class PopupTypesEndpoints 
{
    public static IEndpointRouteBuilder AddPopupTypesEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var versionSet = endpoints
            .NewApiVersionSet()
            .HasApiVersion(new(1) {})
            .ReportApiVersions()
            .Build();
        
        var backOfficeEndpoints = endpoints
            .MapGroup("/backOffice/popups")
            .AddEndpointFilter<FeatureGateEndpointFilter>()
            .WithTags("PopupTypes")
            .WithApiVersionSet(versionSet)
            .RequireAuthorization(AuthenticationPolicy.BackOffice);
        
        _ = backOfficeEndpoints
            .MapGet(
                "/types",
                static async(
                    IFeatureManager features,
                    IMediator mediator,
                    [FromQuery] int? terminalId,
                    [FromQuery] int? page,
                    [FromQuery] int? size
                ) =>
                {
                    var result = await mediator.Send(new ListAllPopupTypesQuery {});

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
            .WithDisplayName("GetAllPopupTypes")
            .WithName("GetAllPopupTypes")
            .WithMetadata(new FeatureGateAttribute("BOF-show_all_popup_types"))
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem();
        
        _ = backOfficeEndpoints
            .MapPost(
                "/types",
                static async(
                    IFeatureManager features,
                    IMediator mediator,
                    CreatePopupTypeCommand request
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
            .WithDisplayName("CreatePopupTypes")
            .WithName("CreatePopupTypes")
            .WithMetadata(new FeatureGateAttribute("BOF-hide_user_popup"))
            .Produces<ApiResponse<CreatePopupTypeCommand>>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem();

        _ = backOfficeEndpoints
            .MapPut(
                "types",
                static async (
                    IFeatureManager features,
                    IMediator mediator,
                    UpdatePopupTypesCommand request
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
            .WithDisplayName("UpdatePopupType")
            .WithName("UpdatePopupType")
            .WithMetadata(new FeatureGateAttribute("BOF-update_popup_type"))
            .Produces<ApiResponse<CreatePopupTypeCommand>>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem();

        return endpoints;
    }
}