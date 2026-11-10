using Microsoft.EntityFrameworkCore;
using ToDoList.Api.Data;
using ToDoList.Shared.DTOs;
using ToDoList.Api.Models;

public class TaskService : ITaskService
{
  private readonly AppDbContext _context;

  public TaskService(AppDbContext context)
  {
    _context = context;
  }

  public async Task<TodoTask> Create(CreateTaskDto dto, Guid userId)
  {
    var task = new TodoTask
    {
      Id = Guid.NewGuid(),
      Title = dto.Title,
      Description = dto.Description,
      IsCompleted = false,
      CreatedAt = DateTime.UtcNow,
      UserId = userId
    };

    _context.Tasks.Add(task);

    await _context.SaveChangesAsync();

    return task;
  }

  public async Task<List<TodoTask>> ListByUser(Guid userId)
  {
    return await _context.Tasks
      .Where(t => t.UserId == userId)
      .OrderByDescending(t => t.CreatedAt)
      .ToListAsync();
  }

  public async Task<List<TodoTask>> ListAll()
  {
    return await _context.Tasks
      .OrderByDescending(t => t.CreatedAt)
      .ToListAsync();
  }
}
