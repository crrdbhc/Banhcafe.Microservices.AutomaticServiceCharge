using Banhcafe.Microservices.ServiceChargingSystem.Api.Endpoints.Filters;
using Banhcafe.Microservices.ServiceChargingSystem.Api.Options;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Services.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace Banhcafe.Microservices.ServiceChargingSystem.Api.Endpoints;

public static class ServicesEndpoints {
    public static IEndpointRouteBuilder AddServicesEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var versionSet = endpoints
            .NewApiVersionSet()
            .HasApiVersion(new(1) {})
            .ReportApiVersions()
            .Build();
        
        var backOfficeEndpoints = endpoints
            .MapGroup("/backOffice")
            .AddEndpointFilter<FeatureGateEndpointFilter>()
            .WithTags("Services")
            .WithApiVersionSet(versionSet)
            .RequireAuthorization(AuthenticationPolicy.BackOffice);

        _ = backOfficeEndpoints
            .MapGet(
                "/services",
                static async(
                    IFeatureManager features,
                    IMediator mediator,
                    [FromQuery] int? terminalId,
                    [FromQuery] int? page,
                    [FromQuery] int? size
                ) =>
                {
                    var result = await mediator.Send(new ListAllServicesQuery {});

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
            .WithDisplayName("GetAllServices")
            .WithName("GetAllServices")
            .WithMetadata(new FeatureGateAttribute("BOF-show_all_services"))
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem();

        _ = backOfficeEndpoints
            .MapGet(
                "/services/{serviceId}",
                static async(
                    IFeatureManager features,
                    IMediator mediator,
                    [FromQuery] int? serviceId,
                    [FromQuery] int? terminalId,
                    [FromQuery] int? page,
                    [FromQuery] int? size
                ) => 
                {
                    var result = await mediator.Send(new ListServicesQuery { ServiceId = serviceId });
                    
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
            .WithDisplayName("GetServiceById")
            .WithName("GetServiceById")
            .WithMetadata(new FeatureGateAttribute("BOF-show_service_by_id"))
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem();
        
        return endpoints;
    }
}