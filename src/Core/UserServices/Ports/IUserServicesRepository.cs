using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Ports;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.UserServices.Models;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Core.UserServices.Ports
{   
    public interface IUserServicesRepository: IGenericRepository<UserServicesBase, ViewUserServicesDto, CreateUserServicesDto, UpdateUserServicesDto, DeleteUserServicesDto>
    {
        Task<IEnumerable<UserServicesBase>> List(
            ViewUserServicesDto filtersDto,
            CancellationToken cancellationToken
        );
    }
}