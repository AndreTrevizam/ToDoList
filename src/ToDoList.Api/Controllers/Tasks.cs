using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Api.Mappings;
using ToDoList.Shared.DTOs;
using ToDoList.Api.Extensions;
using ToDoList.Api.Models;

namespace ToDoList.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class TasksController : ControllerBase
{
  private readonly ITaskService _taskService;

  public TasksController(ITaskService taskService)
  {
    _taskService = taskService;
  }

  [HttpGet]
  public async Task<List<TaskDto>> Get()
  {
    var tasks = await _taskService.ListByUser(User.GetUserId());

    return tasks.Select(t => t.ToDto()).ToList();
  }

  [HttpGet("all")]
  [Authorize(Roles = nameof(UserRole.Admin))]
  public async Task<List<TaskDto>> GetAll()
  {
    var tasks = await _taskService.ListAll();

    return tasks.Select(t => t.ToDto()).ToList();
  }

  [HttpPost]
  public async Task<IActionResult> Create(CreateTaskDto dto)
  {
    var createdTask = await _taskService.Create(dto, User.GetUserId());

    return Ok(createdTask.ToDto());
  }
}
