using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace DrawerComponent.Components.Hamburger
{
    public partial class TwHamburger : UserControl
    {
        public event Action Click = delegate { };

        public static readonly DependencyProperty IconCharProperty = DependencyProperty.Register(nameof(IconChar), typeof(string), typeof(TwHamburger),
            new PropertyMetadata("\ue700", OnIconCharChanged));

        public string IconChar
        {
            get => (string)GetValue(IconCharProperty);
            set => SetValue(IconCharProperty, value);
        }

        public TwHamburger()
        {
            InitializeComponent();
        }

        private static void OnIconCharChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Storyboard rotateStoryboard = (Storyboard)Resources["RotateStoryboard"];
            rotateStoryboard.Begin();
            Click?.Invoke();
        }
    }
}