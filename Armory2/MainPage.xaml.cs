
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Armory2
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage(
            MainPageViewModel vm
        )
        {
            InitializeComponent();

            BindingContext = vm;
        }

    }

    public class ItemObject: ObservableObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public partial class MainPageViewModel: BaseViewModel, INavigationAware
    {
        [ObservableProperty]
        public ItemObject item;

        public ObservableCollection<ItemObject> Items { get; set; } = new();

        public ICommand ItemChangedCommand => new DelegateCommand<ItemObject>(ItemChanged);

        public MainPageViewModel()
        {
            InitializePage();
        }

        private void InitializePage()
        {
            var items = new ObservableCollection<ItemObject> {
                new ItemObject(){
                    Name="Item1",
                    Description="Desc1"
                },
                new ItemObject(){
                    Name="Item2",
                    Description="Desc2"
                },
                new ItemObject(){
                    Name="Item3",
                    Description="Desc3"
                },
                new ItemObject(){
                    Name="Item4",
                    Description="Desc4"
                },
                new ItemObject(){
                    Name="Item4",
                    Description="Desc5"
                },
            };

            Items = items;
        }

        public void ItemChanged(ItemObject item)
        {

        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            InitializePage();
        }
    }
}
