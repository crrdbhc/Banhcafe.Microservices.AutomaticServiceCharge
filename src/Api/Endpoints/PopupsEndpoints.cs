using Banhcafe.Microservices.AutomaticServiceCharge.Api.Endpoints.Filters;
using Banhcafe.Microservices.AutomaticServiceCharge.Api.Options;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Contracts.Response;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Popups.Commands.Create;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Popups.Commands.Update;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Popups.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Api.Endpoints;

public static class PopupsEndpoints {
    public static IEndpointRouteBuilder AddPopupsEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var versionSet = endpoints
            .NewApiVersionSet()
            .HasApiVersion(new(1) {})
            .ReportApiVersions()
            .Build();
        
        var backOfficeEndpoints = endpoints
            .MapGroup("/backOffice")
            .AddEndpointFilter<FeatureGateEndpointFilter>()
            .WithTags("Popups")
            .WithApiVersionSet(versionSet)
            .RequireAuthorization(AuthenticationPolicy.BackOffice);

        _ = backOfficeEndpoints
            .MapGet(
                "/popups",
                static async (
                    IFeatureManager features,
                    IMediator mediator,
                    [FromQuery] int? terminalId,
                    [FromQuery] int? page,
                    [FromQuery] int? size
                ) => 
                {
                    var result = await mediator.Send(new ListAllPopupsQuery {});

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
            .WithDisplayName("GetAllPopups")
            .WithName("GetAllPopups")
            .WithMetadata(new FeatureGateAttribute("BOF-show_all_popups"))
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem();

        _ = backOfficeEndpoints
            .MapGet(
                "/popups/{popupId}",
                static async (
                    IFeatureManager features,
                    IMediator mediator,
                    [FromQuery] int? popupId,
                    [FromQuery] int? terminalId,
                    [FromQuery] int? page,
                    [FromQuery] int? size
                ) =>
                {
                    var result = await mediator.Send(new ListPopupsQuery { PopupId = popupId });

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
            .WithDisplayName("GetPopupById")
            .WithName("GetPopupById")
            .WithMetadata(new FeatureGateAttribute("BOF-show_popup_by_id"))
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem();

        _ = backOfficeEndpoints
            .MapPost(
                "/popups",
                static async (
                    IFeatureManager manager,
                    IMediator mediator,
                    CreatePopupCommand request
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
            .WithDisplayName("CreatePopup")
            .WithName("CreatePopup")
            .WithMetadata(new FeatureGateAttribute("BOF-create_popups"))
            .Produces<ApiResponse<CreatePopupCommand>>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem();

        _ = backOfficeEndpoints
            .MapPut(
                "/popups",
                static async (
                    IFeatureManager features,
                    IMediator mediator,
                    UpdatePopupsCommand request
                ) =>
                {
                    var result = await mediator.Send(request);

                    if (result.ValidationErrors.Count > 0 )
                    {
                        return Results.BadRequest(result);
                    }

                    if (result.Errors.Count > 0 )
                    {
                        return Results.BadRequest(result);
                    }

                    return Results.Ok(result);
                }
            )
            .WithDisplayName("UpdatePopup")
            .WithName("UpdatePopup")
            .WithMetadata(new FeatureGateAttribute("BOF-update_popup"))
            .Produces<ApiResponse<CreatePopupCommand>>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem();

        _ = backOfficeEndpoints
            .MapPut(
                "/popups/content",
                static async (
                    IFeatureManager features,
                    IMediator mediator,
                    UpdatePopupContentCommand request
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
            .WithDisplayName("UpdatePopupContent")
            .WithName("UpdatePopupContent")
            .WithMetadata(new FeatureGateAttribute("BOF-update_popup_content"))
            .Produces<ApiResponse<CreatePopupCommand>>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem();

        return endpoints;
    }
}