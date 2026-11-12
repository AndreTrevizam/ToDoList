using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Desktop.ViewModels;

namespace ToDoList.Desktop.Services;

public partial class NavigationService : ObservableObject
{
  private readonly IServiceProvider _provider;

  public NavigationService(IServiceProvider provider)
  {
    _provider = provider;
  }

  [ObservableProperty]
  public partial ViewModelBase? CurrentPage { get; set; }

  public void NavigateTo<T>() where T : ViewModelBase
  {
    CurrentPage = _provider.GetRequiredService<T>();
  }
}