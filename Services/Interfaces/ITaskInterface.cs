using ToDoList.DTOs;
using ToDoList.Models;

public interface ITaskService
{
  Task<TodoTask> Create(CreateTaskDto dto, Guid userId);
  Task<List<TodoTask>> ListByUser(Guid userId);
}
