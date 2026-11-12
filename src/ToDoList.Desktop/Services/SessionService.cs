using ToDoList.Shared.DTOs;

namespace ToDoList.Desktop.Services;

public class SessionService
{
  public AuthResponseDto? Current { get; set; }

  public void Start(AuthResponseDto auth) => Current = auth;
  public void End() => Current = null;
}