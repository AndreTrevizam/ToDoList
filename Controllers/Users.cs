using Microsoft.AspNetCore.Mvc;
using ToDoList.DTOs;
using ToDoList.Models;

namespace ToDoList.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsersController : ControllerBase
{
  private readonly IUserService _userService;

  public UsersController(IUserService userService)
  {
    _userService = userService;
  }

  [HttpPost]
  public async Task<IActionResult> Create(CreateUserDto dto)
  {
    var createdTask = await _userService.Create(dto);

    return Ok(dto);
  }
}