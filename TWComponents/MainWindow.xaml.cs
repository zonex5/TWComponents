using System.Windows;

namespace DrawerComponent
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            
            Loaded += (a, args) =>
            {
                //todo
                NotificationBox.ErrorNotification("Huinea! Huiovaia huiota! Huini huiovogo huia, huianaia huianea huiovo.");
                NotificationBox.WarningNotification("Huinea! Huiovaia huiota! Huini huiovogo huia, huianaia huianea huiovo.");
                NotificationBox.SusscessNotification("Huinea! Huiovaia huiota! Huini huiovogo huia, huianaia huianea huiovo.");
                NotificationBox.InfoNotification("Huinea! Huiovaia huiota! Huini huiovogo huia, huianaia huianea huiovo.");
            };
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