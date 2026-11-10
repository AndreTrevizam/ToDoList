using ToDoList.Shared.DTOs;
using ToDoList.Api.Models;

public interface ITaskService
{
  Task<TodoTask> Create(CreateTaskDto dto, Guid userId);
  Task<List<TodoTask>> ListByUser(Guid userId);
  Task<List<TodoTask>> ListAll();
}
