using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Ports;
using Banhcafe.Microservices.ServiceChargingSystem.Core.UserServices.Models;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.UserServices.Ports
{   
    public interface IUserServicesRepository: IGenericRepository<UserServicesBase, ViewUserServicesDto, CreateUserServicesDto, UpdateUserServicesDto, DeleteUserServicesDto>
    {
        Task<IEnumerable<UserServicesBase>> List(
            ViewUserServicesDto filtersDto,
            CancellationToken cancellationToken
        );
    }
}