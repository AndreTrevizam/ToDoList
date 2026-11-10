using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ToDoList.Api.Extensions;

public static class ClaimsPrincipalExtensions
{
  public static Guid GetUserId(this ClaimsPrincipal principal)
  {
    var sub = principal.FindFirstValue(JwtRegisteredClaimNames.Sub);

    if (!Guid.TryParse(sub, out var userId))
      throw new UnauthorizedAccessException("Token sem identificação de usuário");

    return userId;
  }
}
