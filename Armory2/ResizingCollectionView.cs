using PropertyChanged;
using System.Collections;
using System.Collections.Specialized;
using System.Net.Mail;
using System.Runtime.CompilerServices;

namespace Armory2
{
    public class ResizingCollectionView : CollectionView
    {

        private INotifyCollectionChanged _oldCollectionChanged;

        public static readonly BindableProperty RowHeightProperty =
        BindableProperty.Create(nameof(RowHeight), typeof(double), typeof(ExpandableControl));

        public double RowHeight
        {
            get { return (double)GetValue(RowHeightProperty); }
            set { SetValue(RowHeightProperty, value); }
        }


        public ResizingCollectionView()
        {

        }

        public ResizingCollectionView(ItemSizingStrategy sizingStrategy = ItemSizingStrategy.MeasureFirstItem) : base()
        {
            ItemSizingStrategy = sizingStrategy;
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

                //ResizeList();
            }
        }

        [SuppressPropertyChangedWarnings]
        private void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var numberOfItems = (ItemsSource as IList)?.Count ?? 1;

            numberOfItems++;

            if (numberOfItems > 0)
            {
                ResizeList();
            }
        }

        public void ResizeList()
        {
#if IOS
            var numberOfItems = (ItemsSource as IList)?.Count ?? 1;

            numberOfItems++;

            HeightRequest = RowHeight * numberOfItems + 10;

            InvalidateMeasure();
#endif
        }
    }
}