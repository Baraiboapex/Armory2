
using CommunityToolkit.Mvvm.ComponentModel;
using DryIoc;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Windows.Input;
#if IOS
using UIKit;
#endif

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

            vm.MainCollectionView = MainCollectionView;
        }

    }

    [AddINotifyPropertyChangedInterface]
    public class ItemObject: ObservableObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Expanded { get; set; }
        public ObservableCollection<NestedItemObject> NestedItems { get; set; }
    }

    [AddINotifyPropertyChangedInterface]
    public class NestedItemObject : ObservableObject
    {
        public string Name { set; get; }
        public string Description { get; set; }

    }


    public partial class MainPageViewModel: BaseViewModel, INavigationAware
    {
        [ObservableProperty]
        public ItemObject item;

        private bool _timerRunning;

        public ObservableCollection<ItemObject> Items { get; set; } = new();

        public ICommand ItemChangedCommand => new DelegateCommand<ItemObject>(ItemChanged);

        public ICommand SectionTapped => new DelegateCommand<ItemObject>(OnSectionTapped);

        public bool CurrentPage { get; set; }

        public CollectionView MainCollectionView {  get; set; } 

        public MainPageViewModel()
        {
            InitializePage();
        }

        private void InitializePage()
        {
            var items = new ObservableCollection<ItemObject> {
                new ItemObject(){
                    Name="Item1",
                    Description="Desc1",
                    NestedItems = new ObservableCollection<NestedItemObject>()
                    {
                        new NestedItemObject()
                        { 
                            Name="NestedItem1",
                            Description="Nested Desc 1"
                        },
                        new NestedItemObject()
                        {
                            Name="NestedItem2",
                            Description="Nested Desc 1"
                        },
                        new NestedItemObject()
                        {
                            Name="NestedItem3",
                            Description="Nested Desc 1"
                        }
                    }
                },
                new ItemObject(){
                    Name="Item2",
                    Description="Desc2",
                    NestedItems = new ObservableCollection<NestedItemObject>()
                    {
                        new NestedItemObject()
                        {
                            Name="NestedItem1",
                            Description="Nested Desc 1"
                        },
                        new NestedItemObject()
                        {
                            Name="NestedItem2",
                            Description="Nested Desc 1"
                        },
                        new NestedItemObject()
                        {
                            Name="NestedItem3",
                            Description="Nested Desc 1"
                        }
                    }
                },
                new ItemObject(){
                    Name="Item3",
                    Description="Desc3",
                    NestedItems = new ObservableCollection<NestedItemObject>()
                    {
                        new NestedItemObject()
                        {
                            Name="NestedItem1",
                            Description="Nested Desc 1"
                        },
                        new NestedItemObject()
                        {
                            Name="NestedItem2",
                            Description="Nested Desc 1"
                        },
                        new NestedItemObject()
                        {
                            Name="NestedItem3",
                            Description="Nested Desc 1"
                        }
                    }
                },
                new ItemObject(){
                    Name="Item4",
                    Description="Desc4",
                    NestedItems = new ObservableCollection<NestedItemObject>()
                    {
                        new NestedItemObject()
                        {
                            Name="NestedItem1",
                            Description="Nested Desc 1"
                        },
                        new NestedItemObject()
                        {
                            Name="NestedItem2",
                            Description="Nested Desc 1"
                        },
                        new NestedItemObject()
                        {
                            Name="NestedItem3",
                            Description="Nested Desc 1"
                        }
                    }
                },
                new ItemObject(){
                    Name="Item4",
                    Description="Desc5",
                    NestedItems = new ObservableCollection<NestedItemObject>()
                    {
                        new NestedItemObject()
                        {
                            Name="NestedItem1",
                            Description="Nested Desc 1"
                        },
                        new NestedItemObject()
                        {
                            Name="NestedItem2",
                            Description="Nested Desc 1"
                        },
                        new NestedItemObject()
                        {
                            Name="NestedItem3",
                            Description="Nested Desc 1"
                        }
                    }
                },
            };
            Items = items;
        }

        public void ItemChanged(ItemObject item)
        {

        }

        public void OnSectionTapped(ItemObject section)
        {
            section.Expanded = !section.Expanded;
#if IOS
            var baseView = MainCollectionView.Handler.PlatformView;

            if(baseView != null)
            {
                var view = baseView as UIView;

                if(view != null)
                {
                    var subViewItemIsCollecitonView = view.Subviews[0] is UICollectionView;

                    if(subViewItemIsCollecitonView)
                    {
                        var iosCollectionView = (view.Subviews[0] as UICollectionView);
                        iosCollectionView.ReloadData();
                    }
                }
            }
#endif
        }

        private void MainCollectionView_Refreshing(object? sender, EventArgs e)
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
