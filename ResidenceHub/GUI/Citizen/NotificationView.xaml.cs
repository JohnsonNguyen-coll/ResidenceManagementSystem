using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ResidenceHub.BLL;
using ResidenceHub.Models;

namespace ResidenceHub.GUI.Citizen
{
    public partial class NotificationsView : Page
    {
        private readonly User _currentUser;
        private readonly NotificationBLL _notificationBLL;
        private readonly LogBLL _logBLL;

        public NotificationsView(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _notificationBLL = new NotificationBLL();
            _logBLL = new LogBLL();

            // Load notifications
            LoadNotifications();
        }

        private void LoadNotifications()
        {
            // Get notifications
            var notifications = _notificationBLL.GetNotificationsByUser(_currentUser.UserId);

            // Count unread notifications
            int unreadCount = notifications.Count(n => n.IsRead == false);
            lblUnreadCount.Text = $"({unreadCount} unread)";

            // Create view model for notifications
            var notificationsVM = notifications.Select(n => new
            {
                n.NotificationId,
                n.Message,
                SentDate = n.SentDate?.ToString("yyyy-MM-dd HH:mm"),
                Status = n.IsRead == true ? "Read" : "Unread",
                BackgroundColor = n.IsRead == true ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Color.FromRgb(232, 245, 233)),
                MarkButtonVisibility = n.IsRead == true ? Visibility.Collapsed : Visibility.Visible
            }).OrderByDescending(n => n.SentDate).ToList();

            lvNotifications.ItemsSource = notificationsVM;
        }

        private void lvNotifications_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Not used in this implementation
        }

        private void btnMarkAsRead_Click(object sender, RoutedEventArgs e)
        {
            // Get notification ID from button tag
            Button button = (Button)sender;
            int notificationId = (int)button.Tag;

            // Mark notification as read
            bool result = _notificationBLL.MarkNotificationAsRead(notificationId, _currentUser.UserId);

            if (result)
            {
                // Reload notifications
                LoadNotifications();
            }
        }

        private void btnMarkAllAsRead_Click(object sender, RoutedEventArgs e)
        {
            // Mark all notifications as read
            bool result = _notificationBLL.MarkAllNotificationsAsRead(_currentUser.UserId);

            if (result)
            {
                // Reload notifications
                LoadNotifications();
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            // Reload notifications
            LoadNotifications();
        }
    }
}