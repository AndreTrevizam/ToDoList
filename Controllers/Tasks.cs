using Microsoft.AspNetCore.Mvc;
using ToDoList.DTOs;
using ToDoList.Models;

namespace ToDoList.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TasksController : ControllerBase
{
  private readonly ITaskService _taskService;

  public TasksController(ITaskService taskService)
  {
    _taskService = taskService;
  }

  [HttpGet]
  public async Task<List<TodoTask>> Get()
  {
    return await _taskService.ListAll();
  }

  [HttpPost]
  public async Task<IActionResult> Create(CreateTaskDto dto)
  {
    var createdTask = await _taskService.Create(dto);

    return Ok(dto);
  }
}