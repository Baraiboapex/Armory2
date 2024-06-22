using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Armory2
{
    internal class ExpandableControl: Microsoft.Maui.Controls.ViewCell
    {
        public static readonly BindableProperty ExpandedViewProperty =
        BindableProperty.Create( nameof( ExpandedView ), typeof( View ), typeof(ExpandableControl) );

    public static readonly BindableProperty HeaderViewProperty =
        BindableProperty.Create( nameof( HeaderView ), typeof( View ), typeof(ExpandableControl) );

        public static readonly BindableProperty IsExpandedProperty =
        BindableProperty.Create(nameof(IsExpanded), typeof(bool), typeof(ExpandableControl));

        public View ExpandedView
        {
            get { return (View)GetValue(ExpandedViewProperty); }
            set { SetValue(ExpandedViewProperty, value); }
        }

        public View HeaderView
        {
            get { return (View)GetValue(HeaderViewProperty); }
            set { SetValue(HeaderViewProperty, value); }
        }

        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if(propertyName == nameof(IsExpanded))
            {
                if (Parent is not ListView listView)
                {
                    return;
                }
#if IOS
                if (listView.Handler is ListViewHandler listViewHandler)
                {
                    listViewHandler.RaiseViewCellSizeChangedEvent();
                }
#endif
            }
        }

    }
}
