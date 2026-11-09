using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.DTOs;
using ToDoList.Models;

public class UserService : IUserService
{
  private readonly AppDbContext _context;

  public UserService(AppDbContext context)
  {
    _context = context;
  }
  public async Task<User> Create(CreateUserDto dto)
  {
    var user = new User
    {
      Id = Guid.NewGuid(),
      Name = dto.Name,
      Password = dto.Password,
      Email = dto.Email,
      CreatedAt = DateTime.UtcNow
    };

    _context.Users.Add(user);

    await _context.SaveChangesAsync();

    return user;
  }
}