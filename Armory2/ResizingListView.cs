using PropertyChanged;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;

namespace Armory2
{
    public class ResizingListView : ListView
    {
        private INotifyCollectionChanged _oldCollectionChanged;

        public ResizingListView() : base(ListViewCachingStrategy.RecycleElement)
        {
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == "ItemsSource")
            {
                if (_oldCollectionChanged != null)
                {
                    _oldCollectionChanged.CollectionChanged -= OnItemsSourceCollectionChanged;
                    _oldCollectionChanged = null;
                }

                if (ItemsSource is INotifyCollectionChanged collectionChanged)
                {
                    collectionChanged.CollectionChanged += OnItemsSourceCollectionChanged;
                    _oldCollectionChanged = collectionChanged;
                }

            }
        }

        [SuppressPropertyChangedWarnings]
        private void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ResizeList();
        }

        public void ResizeList()
        {
            var numberOfItems = (ItemsSource as IList)?.Count ?? 1;

            numberOfItems++;

            HeightRequest = RowHeight * numberOfItems + 10;

            InvalidateMeasure();
        }
    }
}