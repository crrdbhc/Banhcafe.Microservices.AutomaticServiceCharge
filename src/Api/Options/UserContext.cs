using System.Security.Claims;
using Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Ports;

namespace Banhcafe.Microservices.ServiceChargingSystem.Api.Options;
public class UserContext : IUserContext
{
    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        User = httpContextAccessor.HttpContext!.User;
    }

    public ClaimsPrincipal User { get; }
}
