using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using ToDoList.Models;

public class TokenService : ITokenService
{
  private readonly IConfiguration _config;

  public TokenService(IConfiguration config)
  {
    _config = config;
  }

  public string Generate(User user)
  {
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

    var descriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(new[]
      {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(JwtRegisteredClaimNames.Name, user.Name),
        new Claim(TokenClaims.Role, user.Role.ToString())
      }),
      Issuer = _config["Jwt:Issuer"],
      Audience = _config["Jwt:Audience"],
      Expires = DateTime.UtcNow.AddMinutes(_config.GetValue<int>("Jwt:ExpiresMinutes")),
      SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
    };

    return new JsonWebTokenHandler().CreateToken(descriptor);
  }
}
