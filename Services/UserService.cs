using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.DTOs;
using ToDoList.Models;

public class UserService : IUserService
{
  private readonly AppDbContext _context;
  private readonly IPasswordHasher<User> _hasher;

  public UserService(AppDbContext context, IPasswordHasher<User> hasher)
  {
    _context = context;
    _hasher = hasher;
  }

  public async Task<User?> Register(CreateUserDto dto)
  {
    var email = NormalizeEmail(dto.Email);

    if (await _context.Users.AnyAsync(u => u.Email == email))
      return null;

    var now = DateTime.UtcNow;
    var user = new User
    {
      Id = Guid.NewGuid(),
      Name = dto.Name,
      Email = email,
      CreatedAt = now,
      UpdatedAt = now
    };
    user.Password = _hasher.HashPassword(user, dto.Password);

    _context.Users.Add(user);

    await _context.SaveChangesAsync();

    return user;
  }

  public async Task<User?> Authenticate(LoginDto dto)
  {
    var email = NormalizeEmail(dto.Email);
    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    if (user is null)
      return null;

    var result = _hasher.VerifyHashedPassword(user, user.Password, dto.Password);

    if (result == PasswordVerificationResult.Failed)
      return null;

    if (result == PasswordVerificationResult.SuccessRehashNeeded)
    {
      user.Password = _hasher.HashPassword(user, dto.Password);
      user.UpdatedAt = DateTime.UtcNow;
      await _context.SaveChangesAsync();
    }

    return user;
  }

  public async Task<List<User>> ListAll()
  {
    return await _context.Users
      .OrderBy(u => u.CreatedAt)
      .ToListAsync();
  }

  private static string NormalizeEmail(string email) => email.Trim().ToLowerInvariant();
}
