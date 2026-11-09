using ToDoList.DTOs;
using ToDoList.Models;

public interface ITaskService
{
  Task<TodoTask> Create(CreateTaskDto dto);
  Task<List<TodoTask>> ListAll();
}