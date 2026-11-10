using ToDoList.Api.Models;

public interface ITokenService
{
  string Generate(User user);
}
