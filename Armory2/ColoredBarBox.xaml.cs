namespace Armory2
{
    public partial class ColoredBarBox
    {
        public static readonly BindableProperty BarColorProperty = BindableProperty.Create(nameof(BarColor),
                                                                                        typeof(Color),
                                                                                        typeof(ColoredBarBox),
                                                                                        default(Color));

        public Color BarColor
        {
            get => (Color)GetValue(BarColorProperty);
            set => SetValue(BarColorProperty, value);
        }

        public ColoredBarBox()
        {
            InitializeComponent();
        }
    }
}