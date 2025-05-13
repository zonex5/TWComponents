using System;
using System.Timers;
using System.Windows;
using TwComponents.Helpers;
using TwComponents.Models;

namespace TwComponents.Components.Notification
{
    public partial class TwNotification
    {
        public event Action<object> OnCloseInvoke = delegate { };

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string),
            typeof(TwNotification), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type",
            typeof(NotificationTypes), typeof(TwNotification), new PropertyMetadata(NotificationTypes.Info));

        private readonly Timer _timer;

        public string Text
        {
            get => (string)GetValue(TextProperty);
            private set => SetValue(TextProperty, value);
        }

        public NotificationTypes Type
        {
            get { return (NotificationTypes)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public ColorModel NotificationColor
        {
            get
            {
                switch (Type)
                {
                    case NotificationTypes.Error:
                        return ColorHelper.ErrorColor;
                    case NotificationTypes.Warning:
                        return ColorHelper.WarningColor;
                    case NotificationTypes.Success:
                        return ColorHelper.SuccessColor;
                    case NotificationTypes.Info:
                    default:
                        return ColorHelper.InfoColor;
                }
            }
        }

        public string IconSource
        {
            get
            {
                switch (Type)
                {
                    case NotificationTypes.Error:
                        return "\uEB90";
                    case NotificationTypes.Warning:
                        return "\uE7BA";
                    case NotificationTypes.Success:
                        return "\uE783";
                    case NotificationTypes.Info:
                    default:
                        return "\uF167";
                }
            }
        }

        public TwNotification(string msg, NotificationTypes notificationType, int closeAfterSeconds)
        {
            InitializeComponent();

            Text = msg;
            Type = notificationType;
            if (closeAfterSeconds > 0)
            {
                _timer = new Timer(1000 * closeAfterSeconds);
                _timer.Elapsed += (sender, e) => { Dispatcher.Invoke(() => { OnCloseInvoke.Invoke(this); }); };
                _timer.AutoReset = false;
                _timer.Start();
            }
        }

        private void BtClose_Click(object sender, RoutedEventArgs e)
        {
            OnCloseInvoke.Invoke(this);
        }
    }

    public enum NotificationTypes
    {
        Error,
        Warning,
        Info,
        Success
    }
}