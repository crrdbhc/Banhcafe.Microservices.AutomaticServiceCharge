using System.Security.Claims;
using Banhcafe.Microservices.AutomaticServiceCharge.Core.Common.Ports;

namespace Banhcafe.Microservices.AutomaticServiceCharge.Api.Options;
public class UserContext : IUserContext
{
    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        User = httpContextAccessor.HttpContext!.User;
    }

    public ClaimsPrincipal User { get; }
}
