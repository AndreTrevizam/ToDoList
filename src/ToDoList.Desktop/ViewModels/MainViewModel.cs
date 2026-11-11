using CommunityToolkit.Mvvm.ComponentModel;
using ToDoList.Desktop.ViewModels.Auth;

namespace ToDoList.Desktop.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial ViewModelBase CurrentPage { get; set; } = new LoginViewModel();
}
