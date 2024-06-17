
using CommunityToolkit.Mvvm.ComponentModel;
using DryIoc;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls.Handlers.Compatibility;

#if IOS
using UIKit;
using Foundation;
#endif

namespace Armory2
{
    public partial class MainPage :  ContentPage, IHasExpandableList
    {
        int count = 0;

        public static event Action ViewCellSizeChangedEvent;

        public MainPage(
            MainPageViewModel vm
        )
        {
            InitializeComponent();

            BindingContext = vm;

            vm.MainCollectionView = MainCollectionView;
            vm.MonitorViewCellSizeChange = ViewCellSizeChangedEvent;
        }

    }

    [AddINotifyPropertyChangedInterface]
    public class ItemObject: ObservableObject
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Expanded { get; set; }
        public ObservableCollection<NestedItemObject> NestedItems { get; set; }
    }

    [AddINotifyPropertyChangedInterface]
    public class NestedItemObject : ObservableObject
    {
        public int Index { get; set; }
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

        public static event Action ViewCellSizeChangedEvent;

        public ContentView View {  get; set; }
        public Action MonitorViewCellSizeChange { get; internal set; }

        public MainPageViewModel()
        {
            InitializePage();
        }

        private void InitializePage()
        {
            var items = new ObservableCollection<ItemObject> {
                new ItemObject(){
                    Index = 0,
                    Name="Item1",
                    Description="Desc1",
                    NestedItems = new ObservableCollection<NestedItemObject>()
                    {
                        new NestedItemObject()
                        { 
                            Index = 0,
                            Name="NestedItem1",
                            Description="Nested Desc 1"
                        },
                        new NestedItemObject()
                        {
                            Index = 1,
                            Name="NestedItem2",
                            Description="Nested Desc 2"
                        },
                        new NestedItemObject()
                        {
                            Index = 2,
                            Name="NestedItem3",
                            Description="Nested Desc 3"
                        }
                    }
                },
                new ItemObject(){
                    Index = 1,
                    Name="Item2",
                    Description="Desc2",
                    NestedItems = new ObservableCollection<NestedItemObject>()
                    {
                        new NestedItemObject()
                        {

                            Index = 0,
                            Name="NestedItem1",
                            Description="Nested Desc 1"
                        },
                        new NestedItemObject()
                        {

                            Index = 1,
                            Name="NestedItem2",
                            Description="Nested Desc 2"
                        },
                        new NestedItemObject()
                        {
                            Index = 2,
                            Name="NestedItem3",
                            Description="Nested Desc 3"
                        }
                    }
                },
                new ItemObject(){
                    Index = 2,
                    Name="Item3",
                    Description="Desc3",
                    NestedItems = new ObservableCollection<NestedItemObject>()
                    {
                        new NestedItemObject()
                        {
                            Index = 0,
                            Name="NestedItem1",
                            Description="Nested Desc 0"
                        },
                        new NestedItemObject()
                        {
                            Index = 1,
                            Name="NestedItem2",
                            Description="Nested Desc 1"
                        },
                        new NestedItemObject()
                        {
                            Index = 2,
                            Name="NestedItem3",
                            Description="Nested Desc 2"
                        }
                    }
                },
                new ItemObject(){
                    Index = 3,
                    Name="Item4",
                    Description="Desc4",
                    NestedItems = new ObservableCollection<NestedItemObject>()
                    {
                        new NestedItemObject()
                        {
                            Index = 0,
                            Name="NestedItem1",
                            Description="Nested Desc 1"
                        },
                        new NestedItemObject()
                        {
                            Index= 1,
                            Name="NestedItem2",
                            Description="Nested Desc 2"
                        },
                        new NestedItemObject()
                        {
                            Index= 2,
                            Name="NestedItem3",
                            Description="Nested Desc 3"
                        }
                    }
                },
                new ItemObject(){
                     Index = 4,
                    Name="Item5",
                    Description="Desc5",
                    NestedItems = new ObservableCollection<NestedItemObject>()
                    {
                        new NestedItemObject()
                        {
                            Index = 0,
                            Name="NestedItem1",
                            Description="Nested Desc 1"
                        },
                        new NestedItemObject()
                        {
                            Index= 1,
                            Name="NestedItem2",
                            Description="Nested Desc 2"
                        },
                        new NestedItemObject()
                        {
                            Index= 2,
                            Name="NestedItem3",
                            Description="Nested Desc 3"
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

            MonitorViewCellSizeChange.Invoke();

            //if(baseView != null)
            //{
            //    var view = baseView as UIView;

            //    if(view != null)
            //    {
            //        var subViewItemIsCollecitonView = view.Subviews[0] is UICollectionView;

            //        if(subViewItemIsCollecitonView)
            //        {
            //            var iosCollectionView = (view.Subviews[0] as UICollectionView);

            //            nint index = Convert.ToUInt16(section.Index);
            //            nint sect = Convert.ToUInt16(0);
            //            NSIndexPath iosIndex = NSIndexPath.FromItemSection(index, sect);
            //            NSIndexPath[] iosIndexSet = { iosIndex };

            //            iosCollectionView.ReloadItems(iosIndexSet);
            //        }

            //    }
            //}


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
