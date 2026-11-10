using ToDoList.Shared.DTOs;
using ToDoList.Api.Models;

public interface IUserService
{
  Task<User?> Register(CreateUserDto dto);
  Task<User?> Authenticate(LoginDto dto);
  Task<List<User>> ListAll();
}
