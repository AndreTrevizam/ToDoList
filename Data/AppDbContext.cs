using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Data;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options) { }

  public DbSet<TodoTask> Tasks { get; set; }
  public DbSet<User> Users { get; set; }
}