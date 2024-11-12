using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace DrawerComponent.Components.Drawer
{
    [ContentProperty(nameof(MenuContent))]
    public class TwDrawer : UserControl
    {
        private DrawerViewModel ViewModel { get; }

        public static readonly DependencyProperty MenuContentProperty =
            DependencyProperty.Register(nameof(MenuContent), typeof(UIElement), typeof(TwDrawer), new PropertyMetadata(null));

        public static readonly DependencyProperty ExpandedWidthProperty =
            DependencyProperty.Register(nameof(ExpandedWidth), typeof(int), typeof(TwDrawer),
                new PropertyMetadata(DrawerViewModel.DefaultExpandedWidth, OnExpandedWidthChanged));

        public static readonly DependencyProperty CollapsedWidthProperty =
            DependencyProperty.Register(nameof(CollapsedWidth), typeof(int), typeof(TwDrawer),
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

        /// <summary>
        /// 
        /// </summary>
        private StackPanel MenuPanelControl;

        public TwDrawer()
        {
            InitializeComponent();
            ViewModel = new DrawerViewModel();
            DataContext = ViewModel;
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void InitializeComponent()
        {
            // Создание стиля для StackPanel
            var stackPanelStyle = new Style(typeof(StackPanel));
            stackPanelStyle.Setters.Add(new Setter(WidthProperty, new Binding("MenuWidth")));

            // Добавление стиля в ресурсы UserControl
            Resources.Add(typeof(StackPanel), stackPanelStyle);

            // Создание Grid, содержащего StackPanel
            var grid = new Grid();

            // Создание StackPanel
            MenuPanelControl = new StackPanel
            {
                Background = Brushes.Transparent
            };

            // Создание ContentPresenter
            var contentPresenter = new ContentPresenter();
            var binding = new Binding("MenuContent")
            {
                RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(UserControl), 1)
            };
            contentPresenter.SetBinding(ContentPresenter.ContentProperty, binding);

            // Добавление ContentPresenter в StackPanel
            MenuPanelControl.Children.Add(contentPresenter);

            // Добавление StackPanel в Grid
            grid.Children.Add(MenuPanelControl);

            // Установка корневого элемента UserControl
            Content = grid;

            // Установка фона UserControl
            Background = new SolidColorBrush(Color.FromRgb(60, 74, 85));
        }

        private static void OnExpandedWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is TwDrawer drawer) || drawer.ViewModel == null) return;

            drawer.ViewModel.ExpandedWidth = (int)e.NewValue;
            if (!drawer.ViewModel.IsCollapsed)
            {
                drawer.AnimateMenuWidth(drawer.ViewModel.ExpandedWidth);
            }
        }

        private static void OnCollapsedWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is TwDrawer drawer) || drawer.ViewModel == null) return;

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