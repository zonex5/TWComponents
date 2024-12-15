using System.Windows;
using TwComponents.Components;
using TwComponents.Components.Dialog;

namespace TwComponents
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += (a, args) =>
            {
                //NotificationBox.ErrorNotification("Huinea! Huiovaia huiota! Huini huiovogo huia, huianaia huianea huiovo.");
                //NotificationBox.WarningNotification("Huinea! Huiovaia huiota! Huini huiovogo huia, huianaia huianea huiovo.");
                //NotificationBox.SusscessNotification("Huinea! Huiovaia huiota! Huini huiovogo huia, huianaia huianea huiovo.");
                //NotificationBox.InfoNotification("Huinea! Huiovaia huiota! Huini huiovogo huia, huianaia huianea huiovo.");

                TwHamburgerButton_OnClick();
                
                //TestWindow window = new TestWindow();
                //window.ShowDialog();
            };
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            MyDrawer.Toggle();
        }

        private void TwHamburgerButton_OnClick()
        {
            TwDialog.Show("Huinea! Huiovaia huiota! Huini huiovogo huia, huianaia huianea huiovo. Huiovaia huiota! Huini huiovogo huia, huianaia huianea huiovo.", "qweqwe", MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question);
        }
    }
}