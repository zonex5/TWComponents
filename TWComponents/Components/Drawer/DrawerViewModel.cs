using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DrawerComponent.Components.Drawer
{
    public class DrawerViewModel : INotifyPropertyChanged
    {
        public const int DefaultCollapsedWidth = 200;
        public const int DefaultExpandedWidth = 60;

        private bool _isCollapsed;
        private int _collapsedWidth;
        private int _expandedWidth;

        public DrawerViewModel()
        {
            IsCollapsed = false;
            ExpandedWidth = DefaultExpandedWidth;
            CollapsedWidth = DefaultCollapsedWidth;
        }

        public int CollapsedWidth
        {
            get => _collapsedWidth;
            set
            {
                if (_collapsedWidth == value) return;
                _collapsedWidth = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(MenuWidth));
            }
        }

        public int ExpandedWidth
        {
            get => _expandedWidth;
            set
            {
                if (_expandedWidth == value) return;
                _expandedWidth = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(MenuWidth));
            }
        }

        public bool IsCollapsed
        {
            get => _isCollapsed;
            set
            {
                if (_isCollapsed == value) return;
                _isCollapsed = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(MenuWidth));
            }
        }

        public int MenuWidth => IsCollapsed ? CollapsedWidth : ExpandedWidth;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}