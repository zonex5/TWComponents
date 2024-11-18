namespace DrawerComponent.Components.Notification
{
    public partial class TwNotificationBox
    {
        public TwNotificationBox()
        {
            InitializeComponent();
        }

        public int AddNotification(TwNotification twNotification)
        {
            if (NotificationContainer.Children.Count >= 10) return -1;
            return NotificationContainer.Children.Add(twNotification);
        }

        public void RemoveNotification(TwNotification twNotification)
        {
            NotificationContainer.Children.Remove(twNotification);
        }

        public void ClearNotifications()
        {
            NotificationContainer.Children.Clear();
        }

        public TwNotification WarningNotification(string message, int closeAfterSeconds = 0)
        {
            var note = new TwNotification(message, NotificationTypes.Warning, closeAfterSeconds);
            note.OnCloseInvoke += Note_OnCloseInvoke;
            AddNotification(note);
            return note;
        }

        public TwNotification ErrorNotification(string message, int closeAfterSeconds = 0)
        {
            var note = new TwNotification(message, NotificationTypes.Error, closeAfterSeconds);
            note.OnCloseInvoke += Note_OnCloseInvoke;
            AddNotification(note);
            return note;
        }

        public TwNotification SusscessNotification(string message, int closeAfterSeconds = 0)
        {
            var note = new TwNotification(message, NotificationTypes.Success, closeAfterSeconds);
            note.OnCloseInvoke += Note_OnCloseInvoke;
            AddNotification(note);
            return note;
        }

        public TwNotification InfoNotification(string message, int closeAfterSeconds = 0)
        {
            var note = new TwNotification(message, NotificationTypes.Info, closeAfterSeconds);
            note.OnCloseInvoke += Note_OnCloseInvoke;
            AddNotification(note);
            return note;
        }

        private void Note_OnCloseInvoke(object note)
        {
            RemoveNotification((TwNotification)note);
        }
    }
}