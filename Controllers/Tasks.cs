using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.DTOs;
using ToDoList.Extensions;

namespace ToDoList.Controllers;

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

    return tasks.Select(TaskDto.FromEntity).ToList();
  }

  [HttpPost]
  public async Task<IActionResult> Create(CreateTaskDto dto)
  {
    var createdTask = await _taskService.Create(dto, User.GetUserId());

    return Ok(TaskDto.FromEntity(createdTask));
  }
}
