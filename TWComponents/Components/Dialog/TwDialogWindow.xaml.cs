using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace TwComponents.Components.Dialog
{
    public partial class TwDialogWindow : Window, INotifyPropertyChanged
    {
        public TwDialogWindow()
        {
            InitializeComponent();
            Loaded += (sender, args) => RemoveWindowRoundedCorners();

            DataContext = this;
        }

        private string _message;

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        private string _caption;

        public string Caption
        {
            get => _caption;
            set
            {
                _caption = value;
                OnPropertyChanged();
            }
        }

        private string _iconGlyph;

        public string IconGlyph
        {
            get => _iconGlyph;
            set
            {
                _iconGlyph = value;
                OnPropertyChanged();
            }
        }

        private Brush _iconForeground = Brushes.Black;

        public Brush IconForeground
        {
            get => _iconForeground;
            set
            {
                _iconForeground = value;
                OnPropertyChanged();
            }
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

        #region Remove window rounded corners

        private enum DWM_WINDOW_CORNER_PREFERENCE
        {
            DWMWCP_DONOTROUND = 1,
        }

        [DllImport("dwmapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr,
            ref DWM_WINDOW_CORNER_PREFERENCE attrValue, int attrSize);

        private void RemoveWindowRoundedCorners()
        {
            IntPtr hwnd = new WindowInteropHelper(this).Handle;
            DWM_WINDOW_CORNER_PREFERENCE preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_DONOTROUND;
            DwmSetWindowAttribute(hwnd, 33, ref preference, sizeof(uint));
        }

        #endregion
    }
}