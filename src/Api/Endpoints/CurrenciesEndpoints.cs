using Banhcafe.Microservices.AutomaticServiceCharge.Api.Endpoints.Filters;
using Banhcafe.Microservices.AutomaticServiceCharge.Api.Options;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Currencies.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Api.Endpoints;

public static class CurrenciesEndpoints {
    public static IEndpointRouteBuilder AddCurrenciesEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var versionSet = endpoints
            .NewApiVersionSet()
            .HasApiVersion(new(1) {})
            .ReportApiVersions()
            .Build();
        
        var backOfficeEndpoints = endpoints
            .MapGroup("/backOffice/currencies")
            .AddEndpointFilter<FeatureGateEndpointFilter>()
            .WithTags("Currencies")
            .WithApiVersionSet(versionSet)
            .RequireAuthorization(AuthenticationPolicy.BackOffice);

         _ = backOfficeEndpoints
            .MapGet(
                "/",
                static async(
                    IFeatureManager features,
                    IMediator mediator,
                    [FromQuery] int? terminalId,
                    [FromQuery] int? page,
                    [FromQuery] int? size
                ) => 
                {
                    var result = await mediator.Send(new ListCurrenciesQuery {});
                    
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
            .WithDisplayName("GetCurrencies")
            .WithName("GetCurrencies")
            .WithMetadata(new FeatureGateAttribute("BOF-show_currencies"))
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem();
        
        return endpoints;
    }
}