using CommunityToolkit.Mvvm.ComponentModel;

namespace Armory2
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        bool busy;
    }
}
