using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ToDoList.Desktop.Services;
using ToDoList.Desktop.ViewModels.Home;
using ToDoList.Shared.DTOs;

namespace ToDoList.Desktop.ViewModels.Auth;

public partial class LoginViewModel : ViewModelBase
{
  private readonly AuthService _authService;
  private readonly SessionService _sessionService;
  private readonly NavigationService _navigationService;
  private readonly Action<AuthResponseDto> _onLoggedIn;

  public LoginViewModel(AuthService authService, SessionService sessionService, NavigationService navigationService)
  {
    _authService = authService;
    _sessionService = sessionService;
    _navigationService = navigationService;
  }

  [ObservableProperty]
  public partial string Email { get; set; } = string.Empty;

  [ObservableProperty]
  public partial string Password { get; set; } = string.Empty;

  [ObservableProperty]
  public partial string? ErrorMessage { get; set; }

  [ObservableProperty]
  public partial bool IsBusy { get; set; }

  [RelayCommand]
  private async Task LoginAsync()
  {
    IsBusy = true;
    ErrorMessage = null;

    try
    {
      var result = await _authService.LoginAsync(new LoginDto
      {
        Email = Email,
        Password = Password
      });

      if (result is null)
      {
        ErrorMessage = "Email ou senha inválidos";
        return;
      }

      _sessionService.Start(result);
      _navigationService.NavigateTo<HomeViewModel>();
    }
    finally
    {
      IsBusy = false;
    }
  }
}