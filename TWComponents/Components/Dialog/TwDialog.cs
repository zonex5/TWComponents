using System;
using System.Collections.Generic;
using System.Windows;

namespace TwComponents.Components.Dialog
{
    public static class TwDialog
    {
        public static MessageBoxResult Show(string messageBoxText)
        {
            return ShowInternal(null, messageBoxText, "Сообщение", MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.OK);
        }

        public static MessageBoxResult Show(string messageBoxText, string caption)
        {
            return ShowInternal(null, messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.OK);
        }

        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button)
        {
            return ShowInternal(null, messageBoxText, caption, button, MessageBoxImage.None, GetDefaultResult(button));
        }

        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            return ShowInternal(null, messageBoxText, caption, button, icon, GetDefaultResult(button));
        }

        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
        {
            return ShowInternal(null, messageBoxText, caption, button, icon, defaultResult);
        }

        public static MessageBoxResult Show(Window owner, string messageBoxText)
        {
            return ShowInternal(owner, messageBoxText, "Сообщение", MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.OK);
        }

        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption)
        {
            return ShowInternal(owner, messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.OK);
        }

        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button)
        {
            return ShowInternal(owner, messageBoxText, caption, button, MessageBoxImage.None, GetDefaultResult(button));
        }

        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            return ShowInternal(owner, messageBoxText, caption, button, icon, GetDefaultResult(button));
        }

        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
        {
            return ShowInternal(owner, messageBoxText, caption, button, icon, defaultResult);
        }

        // Внутренний метод для отображения диалога
        private static MessageBoxResult ShowInternal(Window owner, string message, string caption, MessageBoxButton buttons, MessageBoxImage icon, MessageBoxResult defaultResult)
        {
            TwDialogWindow dialog = new TwDialogWindow
            {
                Owner = owner,
                Message = message,
                Caption = caption,
                IconGlyph = GetIconGlyph(icon),
                Buttons = GetButtons(buttons, defaultResult)
            };

            bool? result = dialog.ShowDialog();

            if (result == true && dialog.Tag is MessageBoxResult mbr)
            {
                return mbr;
            }

            return MessageBoxResult.None;
        }

        // Метод для получения символа-иконки
        private static string GetIconGlyph(MessageBoxImage icon)
        {
            switch (icon)
            {
                case MessageBoxImage.Asterisk:
                    return "\uE946"; // Information icon
                case MessageBoxImage.Error:
                    return "\uE783"; // Error icon
                case MessageBoxImage.Exclamation:
                    return "\uE7BA"; // Warning icon
                case MessageBoxImage.Question:
                    return "\uE946"; // Question icon (можно выбрать другой код, если необходимо)
                default:
                    return string.Empty;
            }
        }

        // Метод для получения списка кнопок в диалоге
        private static List<DialogButton> GetButtons(MessageBoxButton buttons, MessageBoxResult defaultResult)
        {
            var btnList = new List<DialogButton>();

            switch (buttons)
            {
                case MessageBoxButton.OK:
                    btnList.Add(new DialogButton { Content = "OK", Result = MessageBoxResult.OK, IsDefault = true, IsCancel = true });
                    break;
                case MessageBoxButton.OKCancel:
                    btnList.Add(new DialogButton { Content = "OK", Result = MessageBoxResult.OK, IsDefault = (defaultResult == MessageBoxResult.OK) });
                    btnList.Add(new DialogButton { Content = "Cancel", Result = MessageBoxResult.Cancel, IsCancel = true });
                    break;
                case MessageBoxButton.YesNo:
                    btnList.Add(new DialogButton { Content = "Yes", Result = MessageBoxResult.Yes, IsDefault = (defaultResult == MessageBoxResult.Yes) });
                    btnList.Add(new DialogButton { Content = "No", Result = MessageBoxResult.No, IsCancel = true });
                    break;
                case MessageBoxButton.YesNoCancel:
                    btnList.Add(new DialogButton { Content = "Yes", Result = MessageBoxResult.Yes, IsDefault = (defaultResult == MessageBoxResult.Yes) });
                    btnList.Add(new DialogButton { Content = "No", Result = MessageBoxResult.No });
                    btnList.Add(new DialogButton { Content = "Cancel", Result = MessageBoxResult.Cancel, IsCancel = true });
                    break;
                default:
                    btnList.Add(new DialogButton { Content = "OK", Result = MessageBoxResult.OK, IsDefault = true, IsCancel = true });
                    break;
            }

            return btnList;
        }

        // Метод для определения результата по умолчанию
        private static MessageBoxResult GetDefaultResult(MessageBoxButton buttons)
        {
            switch (buttons)
            {
                case MessageBoxButton.OK:
                    return MessageBoxResult.OK;
                case MessageBoxButton.OKCancel:
                    return MessageBoxResult.OK;
                case MessageBoxButton.YesNo:
                    return MessageBoxResult.Yes;
                case MessageBoxButton.YesNoCancel:
                    return MessageBoxResult.Yes;
                default:
                    return MessageBoxResult.None;
            }
        }
    }

    // Класс для представления кнопки в диалоге
    public class DialogButton
    {
        public string Content { get; set; }
        public MessageBoxResult Result { get; set; }
        public bool IsDefault { get; set; }
        public bool IsCancel { get; set; }
    }
}
