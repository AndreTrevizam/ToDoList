using ToDoList.Api.Models;
using ToDoList.Shared.DTOs;

namespace ToDoList.Api.Mappings;

public static class DtoMappings
{
  public static TaskDto ToDto(this TodoTask task) => new()
  {
    Id = task.Id,
    UserId = task.UserId,
    Title = task.Title,
    Description = task.Description,
    IsCompleted = task.IsCompleted,
    CreatedAt = task.CreatedAt
  };

  public static UserDto ToDto(this User user) => new()
  {
    Id = user.Id,
    Name = user.Name,
    Email = user.Email,
    Role = user.Role.ToString(),
    CreatedAt = user.CreatedAt
  };
}
