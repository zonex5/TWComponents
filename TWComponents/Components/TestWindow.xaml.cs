using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace TwComponents.Components
{
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();

            Loaded += (sender, args) => { RemoveWindowRoundedCorners(); };
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