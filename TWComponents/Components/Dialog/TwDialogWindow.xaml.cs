// TwDialogWindow.xaml.cs
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace TwComponents.Components.Dialog
{
    public partial class TwDialogWindow : Window, INotifyPropertyChanged
    {
        public TwDialogWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private string _message;
        public string Message
        {
            get => _message;
            set { _message = value; OnPropertyChanged(); }
        }

        private string _caption;
        public string Caption
        {
            get => _caption;
            set { _caption = value; OnPropertyChanged(); }
        }

        // Новый свойство для символа иконки
        private string _iconGlyph;
        public string IconGlyph
        {
            get => _iconGlyph;
            set { _iconGlyph = value; OnPropertyChanged(); }
        }

        // Новое свойство для цвета иконки (опционально)
        private Brush _iconForeground = Brushes.Black;
        public Brush IconForeground
        {
            get => _iconForeground;
            set { _iconForeground = value; OnPropertyChanged(); }
        }

        public List<DialogButton> Buttons { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.Tag is MessageBoxResult result)
            {
                this.DialogResult = true;
                this.Tag = result;
                this.Close();
            }
        }
    }
}