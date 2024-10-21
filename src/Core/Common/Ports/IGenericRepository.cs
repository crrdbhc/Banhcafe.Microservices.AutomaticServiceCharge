namespace Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Ports;
public interface IGenericRepository<TResponse, TFilterRequest, TCreateRequest, TUpdateRequest, TDeleteRequest>
{
    Task<IEnumerable<TResponse>> List(
        TFilterRequest dto = default,
        CancellationToken cancellationToken = default
    );

    Task<TResponse> View(
        TFilterRequest dto = default,
        CancellationToken cancellationToken = default
    );

    Task<TResponse> Create(
        TCreateRequest dto = default,
        CancellationToken cancellationToken = default
    );

    Task<TResponse> Update(
        TUpdateRequest dto = default,
        CancellationToken cancellationToken = default
    );

    Task<TResponse> Delete(
        TDeleteRequest dto = default,
        CancellationToken cancellationToken = default
    );
}
