using ToDoList.DTOs;
using ToDoList.Models;

public interface IUserService
{
  Task<User?> Register(CreateUserDto dto);
  Task<User?> Authenticate(LoginDto dto);
  Task<List<User>> ListAll();
}
