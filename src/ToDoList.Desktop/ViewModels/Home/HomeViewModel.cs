using ToDoList.Desktop.Services;

namespace ToDoList.Desktop.ViewModels.Home;

public partial class HomeViewModel : ViewModelBase
{
  public HomeViewModel(SessionService sessionService)
  {
    UserName = sessionService.Current!.User.Name;
  }

  public string UserName { get; }
}