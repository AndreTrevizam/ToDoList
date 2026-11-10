using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Data;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options) { }

  public DbSet<TodoTask> Tasks { get; set; }
  public DbSet<User> Users { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<User>()
      .HasIndex(u => u.Email)
      .IsUnique();

    modelBuilder.Entity<User>()
      .Property(u => u.Role)
      .HasConversion<string>()
      .HasDefaultValue(UserRole.User)
      .HasSentinel(UserRole.User);

    modelBuilder.Entity<TodoTask>()
      .HasOne(t => t.User)
      .WithMany(u => u.Tasks)
      .HasForeignKey(t => t.UserId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}