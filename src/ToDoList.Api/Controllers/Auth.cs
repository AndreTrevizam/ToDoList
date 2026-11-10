using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Api.Mappings;
using ToDoList.Shared.DTOs;

namespace ToDoList.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
  private readonly IUserService _userService;
  private readonly ITokenService _tokenService;

  public AuthController(IUserService userService, ITokenService tokenService)
  {
    _userService = userService;
    _tokenService = tokenService;
  }

  [HttpPost("register")]
  public async Task<IActionResult> Register(CreateUserDto dto)
  {
    var user = await _userService.Register(dto);

    if (user is null)
      return Conflict(new { message = "Email já cadastrado" });

    return Ok(new AuthResponseDto
    {
      Token = _tokenService.Generate(user),
      User = user.ToDto()
    });
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login(LoginDto dto)
  {
    var user = await _userService.Authenticate(dto);

    if (user is null)
      return Unauthorized(new { message = "Email ou senha inválidos" });

    return Ok(new AuthResponseDto
    {
      Token = _tokenService.Generate(user),
      User = user.ToDto()
    });
  }
}
