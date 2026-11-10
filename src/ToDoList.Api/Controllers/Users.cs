using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.DTOs;
using ToDoList.Models;

namespace ToDoList.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(Roles = nameof(UserRole.Admin))]
public class UsersController : ControllerBase
{
  private readonly IUserService _userService;

  public UsersController(IUserService userService)
  {
    _userService = userService;
  }

  [HttpGet]
  public async Task<List<UserDto>> Get()
  {
    var users = await _userService.ListAll();

    return users.Select(UserDto.FromEntity).ToList();
  }
}
