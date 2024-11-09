using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Animation;

namespace DrawerComponent.Components
{
    [ContentProperty(nameof(MenuContent))]
    public partial class Drawer : UserControl
    {
        private DrawerViewModel ViewModel { get; }

        public static readonly DependencyProperty MenuContentProperty =
            DependencyProperty.Register(nameof(MenuContent), typeof(UIElement), typeof(Drawer), new PropertyMetadata(null));

        public static readonly DependencyProperty ExpandedWidthProperty =
            DependencyProperty.Register(nameof(ExpandedWidth), typeof(int), typeof(Drawer),
                new PropertyMetadata(DrawerViewModel.DefaultExpandedWidth, OnExpandedWidthChanged));

        public static readonly DependencyProperty CollapsedWidthProperty =
            DependencyProperty.Register(nameof(CollapsedWidth), typeof(int), typeof(Drawer),
                new PropertyMetadata(DrawerViewModel.DefaultCollapsedWidth, OnCollapsedWidthChanged));

        public UIElement MenuContent
        {
            get => (UIElement)GetValue(MenuContentProperty);
            set => SetValue(MenuContentProperty, value);
        }

        public int CollapsedWidth
        {
            get => (int)GetValue(CollapsedWidthProperty);
            set => SetValue(CollapsedWidthProperty, value);
        }

        public int ExpandedWidth
        {
            get => (int)GetValue(ExpandedWidthProperty);
            set => SetValue(ExpandedWidthProperty, value);
        }

        public Drawer()
        {
            InitializeComponent();
            ViewModel = new DrawerViewModel();
            DataContext = ViewModel;
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private static void OnExpandedWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is Drawer drawer) || drawer.ViewModel == null) return;

            drawer.ViewModel.ExpandedWidth = (int)e.NewValue;
            if (!drawer.ViewModel.IsCollapsed)
            {
                drawer.AnimateMenuWidth(drawer.ViewModel.ExpandedWidth);
            }
        }

        private static void OnCollapsedWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is Drawer drawer) || drawer.ViewModel == null) return;

            drawer.ViewModel.CollapsedWidth = (int)e.NewValue;
            if (drawer.ViewModel.IsCollapsed)
            {
                drawer.AnimateMenuWidth(drawer.ViewModel.CollapsedWidth);
            }
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DrawerViewModel.IsCollapsed))
            {
                AnimateMenuWidth(ViewModel.IsCollapsed ? ViewModel.CollapsedWidth : ViewModel.ExpandedWidth);
            }
        }

        private void AnimateMenuWidth(int toWidth)
        {
            int fromWidth = (int)MenuPanelControl.ActualWidth;
            var animation = new DoubleAnimation
            {
                From = fromWidth,
                To = toWidth,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new QuadraticEase()
            };
            MenuPanelControl.BeginAnimation(WidthProperty, animation);
        }

        public void Toggle()
        {
            ViewModel.IsCollapsed = !ViewModel.IsCollapsed;
        }
    }
}