
using CommunityToolkit.Mvvm.ComponentModel;
using DryIoc;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Platform;
using System.Collections;


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

        MainPageViewModel ViewModel { get; set; }

        public MainPage(
            MainPageViewModel vm
        )
        {
            InitializeComponent();

            BindingContext = vm;

            vm.MainCollectionView = MainCollectionView;
            vm.MonitorViewCellSizeChange = ViewCellSizeChangedEvent;
            
            vm.CurrentPage = this;

            ViewModel = vm;
        }
        
        private void MainCollectionView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.SectionTapped.Execute((ItemObject)e.SelectedItem);
        }
    }

    [AddINotifyPropertyChangedInterface]
    public class ItemObject: ObservableObject
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Expanded { get; set; }
        public ObservableCollection<ItemObject> NestedItems { get; set; }
    }

    public partial class MainPageViewModel: BaseViewModel, INavigationAware
    {

        private bool _timerRunning;

        public List<ItemObject> CurrentList { get; private set; }
        public ObservableCollection<ItemObject> Items { get; set; }

        public ICommand SectionTapped => new DelegateCommand<ItemObject>(OnSectionTapped);

        public ItemObject SelectedObject {  get; set; } 

        public MainPage CurrentPage { get; set; }

        [ObservableProperty]
        public string _collectionViewHeight;

        public ResizingCollectionView MainCollectionView {  get; set; }

        public Action MonitorViewCellSizeChange { get; internal set; }

        public MainPageViewModel()
        {
        }

        

#if IOS
        UICollectionView iosCollectionView { get; set; }
#endif

        private void InitializePage()
        {
            var items = new ObservableCollection<ItemObject> {
                new ItemObject(){
                    Id = 1,
                    Index = 0,
                    Name="Item1",
                    Description="Desc1",
                    NestedItems = new ObservableCollection<ItemObject>()
                    {
                        new ItemObject()
                        {
                            Id = 11,
                            Index = 0,
                            Name="NestedItem1",
                            Description="Nested Desc 1",
                            NestedItems=new ObservableCollection<ItemObject>()
                            {
                                new ItemObject()
                                {
                                    Id = 111,
                                    Index = 0,
                                    Name="NestedItemJOJ",
                                    Description="Nested Desc 1"
                                },
                                new ItemObject()
                                {
                                    Id = 112,
                                    Index = 1,
                                    Name="NestedItemJOJ2",
                                    Description="Nested Desc 2"
                                },
                                new ItemObject()
                                { 
                                    Id = 113,
                                    Index = 2,
                                    Name="NestedItemJOJ3",
                                    Description="Nested Desc 3"
                                },
                                new ItemObject()
                                {
                                    Id = 114,
                                    Index = 3,
                                    Name="NestedItemJOJ4",
                                    Description="Nested Desc 2"
                                },
                                new ItemObject()
                                {
                                    Id = 115,
                                    Index = 4,
                                    Name="NestedItemJOJ5",
                                    Description="Nested Desc 3"
                                },
                                new ItemObject()
                                {
                                    Id = 116,
                                    Index = 5,
                                    Name="NestedItemJOJ6",
                                    Description="Nested Desc 4"
                                },
                                new ItemObject()
                                {
                                    Id = 117,
                                    Index = 6,
                                    Name="NestedItemJOJ7",
                                    Description="Nested Desc 5"
                                }
                            }
                        },
                        new ItemObject()
                        {
                            Id = 12,
                            Index = 1,
                            Name="NestedItem2",
                            Description="Nested Desc 2"
                        },
                        new ItemObject()
                        {
                            Id = 13,
                            Index = 2,
                            Name="NestedItem3",
                            Description="Nested Desc 3"
                        }
                    }
                },
                new ItemObject(){
                    Id=2,
                    Index = 1,
                    Name="Item2",
                    Description="Desc2",
                    NestedItems = new ObservableCollection<ItemObject>()
                    {
                        new ItemObject()
                        {
                            Id = 21,
                            Index = 0,
                            Name="NestedItem1",
                            Description="Nested Desc 1"
                        },
                        new ItemObject()
                        {
                            Id = 22,
                            Index = 1,
                            Name="NestedItem2",
                            Description="Nested Desc 2"
                        },
                        new ItemObject()
                        {
                            Id = 22,
                            Index = 2,
                            Name="NestedItem3",
                            Description="Nested Desc 3"
                        }
                    }
                },
                new ItemObject(){
                    Id = 3,
                    Index = 2,
                    Name="Item3",
                    Description="Desc3",
                    NestedItems = new ObservableCollection<ItemObject>()
                    {
                        new ItemObject()
                        {Id = 31,
                            Index = 0,
                            Name="NestedItem1",
                            Description="Nested Desc 0"
                        },
                        new ItemObject()
                        {Id = 32,
                            Index = 1,
                            Name="NestedItem2",
                            Description="Nested Desc 1"
                        },
                        new ItemObject()
                        {Id = 33,
                            Index = 2,
                            Name="NestedItem3",
                            Description="Nested Desc 2"
                        }
                    }
                },
                new ItemObject(){

                    Id = 4,
                    Index = 3,
                    Name="Item4",
                    Description="Desc4",
                    NestedItems = new ObservableCollection<ItemObject>()
                    {
                        new ItemObject()
                        {Id = 41,
                            Index = 0,
                            Name="NestedItem1",
                            Description="Nested Desc 1"
                        },
                        new ItemObject()
                        {Id = 42,
                            Index= 1,
                            Name="NestedItem2",
                            Description="Nested Desc 2"
                        },
                        new ItemObject()
                        {Id = 43,
                            Index= 2,
                            Name="NestedItem3",
                            Description="Nested Desc 3"
                        }
                    }
                },
                new ItemObject(){
                    Id=5,
                    Index = 4,
                    Name="Item5",
                    Description="Desc5",
                    NestedItems = new ObservableCollection<ItemObject>()
                    {
                        new ItemObject()
                        {Id=51,
                            Index = 0,
                            Name="NestedItem1",
                            Description="Nested Desc 1"
                        },
                        new ItemObject()
                        {Id=52,
                            Index= 1,
                            Name="NestedItem2",
                            Description="Nested Desc 2"
                        },
                        new ItemObject()
                        {Id=53,
                            Index= 2,
                            Name="NestedItem3",
                            Description="Nested Desc 3"
                        }
                    }
                },
            };

            CurrentList = items.ToList();

            Items = new ObservableCollection<ItemObject>(CurrentList);
        }

        public void ItemChanged(ItemObject item)
        {

        }

        public void OnSectionTapped(ItemObject section)
        {
            section.Expanded = !section.Expanded;

#if IOS

            Hashtable table = SearchListRecursively(
                Items,
                (item) => item.Id == section.Id
            );

            var indices = table["IndexPath"] as List<int>;


            //if(MainCollectionView.Handler is ListViewHandler listViewHandler)
            //{
            //    listViewHandler.RaiseViewCellSizeChangedEvent();
            //}
#endif
        }

        private Hashtable? SearchListRecursively(
            ObservableCollection<ItemObject> items, 
            Func<ItemObject,bool> condition,
            List<int> indexArray = null
        )
        {
            Hashtable result = new Hashtable();

            bool foundItem = false;

            foreach (var item in items.Select((value,index) => new { index, value })) {

                if (item.index <= items.Count - 1)
                {
                    indexArray.Add(item.index);

                    if (indexArray == null)
                    {
                        indexArray = new List<int>();
                    }

                    if (item.index >= items.Count - 1)
                    {
                        break;
                    }

                    if (condition(item.value))
                    {
                        result.Add("Item", item);
                        result.Add("IndexPath", indexArray);

                        foundItem = true;

                        break;
                    }

                    if (item.value.NestedItems != null)
                    {
                        if (indexArray != null)
                            SearchListRecursively(item.value.NestedItems, condition, indexArray);
                    }
                }
            }

            if(foundItem)
            {
                return result;
            }

            return null;
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
