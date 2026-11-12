using ToDoList.Desktop.Services;
using ToDoList.Desktop.ViewModels.Auth;
namespace ToDoList.Desktop.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public NavigationService Navigation { get; }

    public MainViewModel(NavigationService navigation)
    {
        Navigation = navigation;
        Navigation.NavigateTo<LoginViewModel>();
    }
}
