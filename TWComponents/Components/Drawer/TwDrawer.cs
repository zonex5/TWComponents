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
    [ContentProperty(nameof(DrawerContent))]
    public class TwDrawer : UserControl
    {
        private DrawerViewModel ViewModel { get; }

        public static readonly DependencyProperty DrawerContentProperty =
            DependencyProperty.Register(nameof(DrawerContent), typeof(UIElement), typeof(TwDrawer), new PropertyMetadata(null));

        public static readonly DependencyProperty ExpandedWidthProperty = DependencyProperty.Register(nameof(ExpandedWidth), typeof(int), typeof(TwDrawer),
            new PropertyMetadata(DrawerViewModel.DefaultExpandedWidth, OnExpandedWidthChanged));

        public static readonly DependencyProperty CollapsedWidthProperty = DependencyProperty.Register(nameof(CollapsedWidth), typeof(int), typeof(TwDrawer),
            new PropertyMetadata(DrawerViewModel.DefaultCollapsedWidth, OnCollapsedWidthChanged));

        public UIElement DrawerContent
        {
            get => (UIElement)GetValue(DrawerContentProperty);
            set => SetValue(DrawerContentProperty, value);
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

        private StackPanel _menuPanelControl;

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
            stackPanelStyle.Setters.Add(new Setter(WidthProperty, new Binding(nameof(ViewModel.DrawerWidth))));

            // Добавление стиля в ресурсы UserControl
            Resources.Add(typeof(StackPanel), stackPanelStyle);

            // Создание Grid, содержащего StackPanel
            var grid = new Grid();

            // Создание StackPanel
            _menuPanelControl = new StackPanel
            {
                Background = Brushes.Transparent
            };

            // Создание ContentPresenter
            var contentPresenter = new ContentPresenter();
            var binding = new Binding(nameof(DrawerContent))
            {
                RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(UserControl), 1)
            };
            contentPresenter.SetBinding(ContentPresenter.ContentProperty, binding);

            // Добавление ContentPresenter в StackPanel
            _menuPanelControl.Children.Add(contentPresenter);

            // Добавление StackPanel в Grid
            grid.Children.Add(_menuPanelControl);

            // Установка корневого элемента UserControl
            Content = grid;

            // Установка фона UserControl
            Background = new SolidColorBrush(Color.FromRgb(60, 74, 85));
        }

        private static void OnExpandedWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TwDrawer drawer && drawer.ViewModel != null)
            {
                drawer.ViewModel.ExpandedWidth = (int)e.NewValue;
                if (!drawer.ViewModel.IsCollapsed)
                {
                    drawer.AnimateMenuWidth(drawer.ViewModel.ExpandedWidth);
                }
            }
        }

        private static void OnCollapsedWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TwDrawer drawer && drawer.ViewModel != null)
            {
                drawer.ViewModel.CollapsedWidth = (int)e.NewValue;
                if (drawer.ViewModel.IsCollapsed)
                {
                    drawer.AnimateMenuWidth(drawer.ViewModel.CollapsedWidth);
                }
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
            double fromWidth = _menuPanelControl.ActualWidth;

            // Создание анимации ширины
            var widthAnimation = new DoubleAnimation
            {
                From = fromWidth,
                To = toWidth,
                Duration = TimeSpan.FromSeconds(1),
                EasingFunction = new QuadraticEase() // Используем плавное изменение
            };

            // Создание Storyboard
            var storyboard = new Storyboard();
            storyboard.Children.Add(widthAnimation);

            // Установка цели анимации
            Storyboard.SetTarget(widthAnimation, _menuPanelControl);
            Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(WidthProperty));

            // Запуск анимации
            storyboard.Begin();
        }

        public void Toggle()
        {
            ViewModel.IsCollapsed = !ViewModel.IsCollapsed;
        }
    }
}