using System;
using System.Windows;
using System.Windows.Controls;

namespace TwComponents.Components.WindowControls
{
    public partial class WindowControls : UserControl
    {
        public event Action<object, RoutedEventArgs> OnMinimizeClick = delegate { };
        public event Action<object, RoutedEventArgs> OnCloseClick = delegate { };
        public event Action<object, RoutedEventArgs> OnMaximizeClick = delegate { };

        public WindowControls()
        {
            InitializeComponent();
        }

        private void Minimize_OnClick(object sender, RoutedEventArgs e)
        {
            OnMinimizeClick.Invoke(sender, e);
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            OnCloseClick.Invoke(sender, e);
        }

        private void Close_Maximize(object sender, RoutedEventArgs e)
        {
            OnMaximizeClick.Invoke(sender, e);
        }
    }
}