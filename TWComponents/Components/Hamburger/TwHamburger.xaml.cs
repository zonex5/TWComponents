using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace DrawerComponent.Components.Hamburger
{
    public partial class TwHamburger : UserControl
    {
        public event Action Click = delegate { };

        public TwHamburger()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Storyboard rotateStoryboard = (Storyboard)Resources["RotateStoryboard"];
            rotateStoryboard.Begin();
            Click?.Invoke();
        }
    }
}