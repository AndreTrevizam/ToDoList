using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.DTOs;
using ToDoList.Models;

public class TaskService : ITaskService
{
  private readonly AppDbContext _context;

  public TaskService(AppDbContext context)
  {
    _context = context;
  }
  public async Task<TodoTask> Create(CreateTaskDto dto)
  {
    var task = new TodoTask
    {
      Id = Guid.NewGuid(),
      Title = dto.Title,
      Description = dto.Description,
      IsCompleted = false,
      CreatedAt = DateTime.UtcNow
    };

    _context.Tasks.Add(task);

    await _context.SaveChangesAsync();

    return task;
  }

  public async Task<List<TodoTask>> ListAll()
  {
    return await _context.Tasks.ToListAsync();
  }
}