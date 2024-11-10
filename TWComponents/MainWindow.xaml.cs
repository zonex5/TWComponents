using System.Windows;

namespace DrawerComponent
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            MyDrawer.Toggle();
        }

        private void TwHamburgerButton_OnClick()
        {
            //MessageBox.Show("Hamburger");
        }
    }
}