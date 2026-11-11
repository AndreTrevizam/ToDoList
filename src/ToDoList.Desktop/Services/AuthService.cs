using System.Net.Http.Json;
using ToDoList.Shared.DTOs;

namespace ToDoList.Desktop.Services;

public class AuthService
{
  private static readonly HttpClient _httpClient = new HttpClient()
  {
    BaseAddress = new Uri("http://localhost:5002/api/v1/auth/")
  };

  public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
  {
    var response = await _httpClient.PostAsJsonAsync("login", dto);

    if (!response.IsSuccessStatusCode)
      return null;

    return await response.Content.ReadFromJsonAsync<AuthResponseDto>();
  }

}