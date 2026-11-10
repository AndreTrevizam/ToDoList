namespace ToDoList.Api.Models;

public class User
{
  public Guid Id { get; set; }

  public string Name { get; set; } = string.Empty;

  public string Password { get; set; } = string.Empty;

  public string Email { get; set; } = string.Empty;

  public UserRole Role { get; set; } = UserRole.User;

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public List<TodoTask> Tasks { get; set; } = new();
}