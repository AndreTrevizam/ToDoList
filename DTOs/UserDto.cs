using ToDoList.Models;

namespace ToDoList.DTOs;

public class UserDto
{
  public Guid Id { get; set; }

  public string Name { get; set; } = string.Empty;

  public string Email { get; set; } = string.Empty;

  public DateTime CreatedAt { get; set; }

  public static UserDto FromEntity(User user) => new()
  {
    Id = user.Id,
    Name = user.Name,
    Email = user.Email,
    CreatedAt = user.CreatedAt
  };
}
