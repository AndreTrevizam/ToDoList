using ToDoList.DTOs;
using ToDoList.Models;

public interface IUserService
{
  Task<User> Create(CreateUserDto dto);
}