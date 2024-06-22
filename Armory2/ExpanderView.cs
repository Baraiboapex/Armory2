//using Armory2;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using System.Text;
//using System.Threading.Tasks;


//public class ExpanderView : Microsoft.Maui.Controls.ViewCell
//{
//    public static event Action ViewCellSizeChangedEvent;

//    public static readonly BindableProperty ExpandedViewProperty =
//        BindableProperty.Create(nameof(ExpandedView), typeof(View), typeof(ExpanderView));

//    public static readonly BindableProperty HeaderViewProperty =
//        BindableProperty.Create(nameof(HeaderView), typeof(View), typeof(ExpanderView));

//    public static readonly BindableProperty IsExpandedProperty =
//        BindableProperty.Create(nameof(IsExpanded), typeof(bool), typeof(ExpanderView), false);

//    public View ExpandedView
//    {
//        get { return (View)GetValue(ExpandedViewProperty); }
//        set { SetValue(ExpandedViewProperty, value); }
//    }

//    public View HeaderView
//    {
//        get { return (View)GetValue(HeaderViewProperty); }
//        set { SetValue(HeaderViewProperty, value); }
//    }


//    public bool IsExpanded
//    {
//        get { return (bool)GetValue(IsExpandedProperty); }
//        set { SetValue(IsExpandedProperty, value); }
//    }

//    public static readonly BindableProperty UpdateExpandedOnTappedProperty = BindableProperty.CreateAttached(
//        propertyName: "UpdateExpandedOnTapped",
//        returnType: typeof(bool),
//        declaringType: typeof(ExpanderView),
//        defaultValue: false,
//        propertyChanged: OnUpdateExpandedOnTappedChanged);

//    public static void OnUpdateExpandedOnTappedChanged(BindableObject bindable, object oldValue, object newValue)
//    {
//        // Once the view is attached to the visual tree, we can add the gesture recognizer
//        if ((bindable is not View view))
//        {
//            return;
//        }

//#if IOS
//        view.Effects.Add(new OnAttachedListenerEffect
//        {
//            OnAttachedToWindow = () =>
//            {
//                var closestExpanderViewCell = view.ClosestAncestor<ExpanderView>();
//                if (closestExpanderViewCell is not null)
//                {
//                    if (newValue is bool updateExpandedOnTapped)
//                    {
//                        if (updateExpandedOnTapped)
//                        {
//                            view.GestureRecognizers.Add(new TapGestureRecognizer
//                            {
//                                Command = new Command(() => closestExpanderViewCell.IsExpanded = !closestExpanderViewCell.IsExpanded)
//                            });
//                        }
//                    }
//                }
//            }
//        }
//        );
//#endif
//    }

//    public static void SetUpdateExpandedOnTapped(BindableObject bindable, bool value) => bindable.SetValue(UpdateExpandedOnTappedProperty, value);

//    public static bool GetUpdateExpandedOnTapped(BindableObject bindable) => (bool)bindable.GetValue(UpdateExpandedOnTappedProperty);

//    private void UpdateExpandedOnTapped(object sender, EventArgs e)
//    {
//        IsExpanded = !IsExpanded;
//    }

//    protected override void OnParentSet()
//    {
//        base.OnParentSet();

//        if (Parent != null)
//        {
//            ConfigureContent();
//        }
//    }

//    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
//    {
//        base.OnPropertyChanged(propertyName);

//        if (propertyName == nameof(IsExpanded))
//        {
//            OnExpandedChanged();
//        }
//    }
//    private void ConfigureContent()
//    {
//        var expanderGrid = new Grid
//        {
//            RowDefinitions =
//            {
//                new RowDefinition( GridLength.Auto ),
//                new RowDefinition( GridLength.Auto ),
//                new RowDefinition( GridLength.Auto ),
//            }
//        };

//        if (HeaderView is not null)
//        {
//            expanderGrid.Add(HeaderView, 0, 0);
//            HeaderView.GestureRecognizers.Add(new TapGestureRecognizer
//            {
//                Command = new Command(() => IsExpanded = !IsExpanded)
//            });
//        }

//        if (ExpandedView is not null)
//        {
//            expanderGrid.Add(ExpandedView, 0, 1);
//            ExpandedView.SetBinding(View.IsVisibleProperty, new Binding(nameof(IsExpanded), source: this));
//        }

//        if (DividerView is not null)
//        {
//            expanderGrid.Add(DividerView, 0, 2);
//        }

//        View = expanderGrid;
//    }

//    private void OnExpandedChanged()
//    {
//        if (Parent is not ListView listView)
//        {
//            return;
//        }

//#if IOS
//        if( listView.Handler is Handlers.ListViewHandler listViewHandler )
//        {
//            listViewHandler.RaiseViewCellSizeChangedEvent();
//        }
//#endif
//    }
//}
