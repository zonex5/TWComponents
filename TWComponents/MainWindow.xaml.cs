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
            RemoveWindowRoundedCorners();

            Loaded += (a, args) =>
            {
                //NotificationBox.ErrorNotification("Huinea! Huiovaia huiota! Huini huiovogo huia, huianaia huianea huiovo.");
                //NotificationBox.WarningNotification("Huinea! Huiovaia huiota! Huini huiovogo huia, huianaia huianea huiovo.");
                //NotificationBox.SusscessNotification("Huinea! Huiovaia huiota! Huini huiovogo huia, huianaia huianea huiovo.");
                //NotificationBox.InfoNotification("Huinea! Huiovaia huiota! Huini huiovogo huia, huianaia huianea huiovo.");

                TwHamburgerButton_OnClick();
            };
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            MyDrawer.Toggle();
        }

        private void TwHamburgerButton_OnClick()
        {
            TwDialog.Show("Huinea! Huiovaia huiota! Huini huiovogo huia, huianaia huianea huiovo. Huiovaia huiota! Huini huiovogo huia, huianaia huianea huiovo. pisea", "qweqwe", MessageBoxButton.OK, MessageBoxImage.Question);
            TwDialog.Show(this,"Ты че, педрила, хочешь удалить что-то?", "Удаление заметки", MessageBoxButton.YesNo, MessageBoxImage.Information);
        }
    }
}