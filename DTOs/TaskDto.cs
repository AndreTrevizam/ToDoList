using ToDoList.Models;

namespace ToDoList.DTOs;

public class TaskDto
{
  public Guid Id { get; set; }

  public string Title { get; set; } = string.Empty;

  public string? Description { get; set; }

  public bool IsCompleted { get; set; }

  public DateTime CreatedAt { get; set; }

  public static TaskDto FromEntity(TodoTask task) => new()
  {
    Id = task.Id,
    Title = task.Title,
    Description = task.Description,
    IsCompleted = task.IsCompleted,
    CreatedAt = task.CreatedAt
  };
}
