using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ResidenceHub.BLL;
using ResidenceHub.Models;

namespace ResidenceHub.GUI.AreaLeader
{
    public partial class SendNotificationView : Page
    {
        private readonly User _currentUser;
        private readonly UserBLL _userBLL;
        private readonly NotificationBLL _notificationBLL;
        private readonly LogBLL _logBLL;

        private List<User> _residents;

        public SendNotificationView(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _userBLL = new UserBLL();
            _notificationBLL = new NotificationBLL();
            _logBLL = new LogBLL();

            // Load all residents
            LoadResidents();

            // Load recent notifications
            LoadRecentNotifications();
        }

        private void LoadResidents()
        {
            try
            {
                // Get all citizens instead of filtering by area
                _residents = _userBLL.GetUsersByRole("Citizen");

                // Set as data source for the combobox
                cmbResidents.ItemsSource = _residents;

                // Select first item if available
                if (_residents.Count > 0)
                {
                    cmbResidents.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading residents: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadRecentNotifications()
        {
            try
            {
                // Create a mock list of recent notifications
                // In a real app, you would get this from the database
                var recentNotifications = new List<object>
                {
                    new { RecipientType = "All Residents", Message = "Community meeting this Saturday at 10 AM in the community center.", SentDate = "2023-05-15 14:30" },
                    new { RecipientType = "Area Police", Message = "Monthly security report submitted.", SentDate = "2023-05-10 09:15" },
                    new { RecipientType = "Specific Resident: John Smith", Message = "Please update your contact information.", SentDate = "2023-05-05 11:45" }
                };

                lstNotifications.ItemsSource = recentNotifications;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading notifications: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmbRecipientType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbRecipientType?.SelectedItem != null && pnlSpecificResident != null)
                {
                    var selectedItem = (ComboBoxItem)cmbRecipientType.SelectedItem;
                    string selectedType = selectedItem.Content.ToString();

                    // Show/hide specific resident selection based on selection
                    pnlSpecificResident.Visibility = selectedType == "Specific Resident" ?
                                                     Visibility.Visible :
                                                     Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error changing recipient type: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get selected recipient type
                var selectedItem = (ComboBoxItem)cmbRecipientType.SelectedItem;
                string recipientType = selectedItem.Content.ToString();

                // Validate message
                if (string.IsNullOrWhiteSpace(txtMessage.Text))
                {
                    MessageBox.Show("Please enter a message.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string message = txtMessage.Text.Trim();
                bool result = false;

                // Send notification based on recipient type
                switch (recipientType)
                {
                    case "All Residents in Area":
                        // Changed to send to all citizens instead of just those in an area
                        var allCitizens = _userBLL.GetUsersByRole("Citizen");
                        result = true;
                        foreach (var citizen in allCitizens)
                        {
                            bool sent = _notificationBLL.SendNotificationToUser(citizen.UserId, message, _currentUser.UserId);
                            if (!sent)
                            {
                                result = false;
                            }
                        }
                        break;

                    case "Specific Resident":
                        if (cmbResidents.SelectedItem == null)
                        {
                            MessageBox.Show("Please select a resident.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        User selectedResident = (User)cmbResidents.SelectedItem;
                        result = _notificationBLL.SendNotificationToUser(selectedResident.UserId, message, _currentUser.UserId);
                        break;

                    case "Area Police":
                        // Changed to send to all police users instead of just those in an area
                        var policeUsers = _userBLL.GetUsersByRole("Police");
                        result = true;
                        foreach (var police in policeUsers)
                        {
                            bool sent = _notificationBLL.SendNotificationToUser(police.UserId, message, _currentUser.UserId);
                            if (!sent)
                            {
                                result = false;
                            }
                        }
                        break;
                }

                // Show result
                if (result)
                {
                    lblStatus.Text = "Notification sent successfully.";
                    lblStatus.Foreground = System.Windows.Media.Brushes.Green;

                    // Clear message
                    txtMessage.Text = "";

                    // Log the action
                    _logBLL.CreateLog(_currentUser.UserId, $"Sent notification to {recipientType}");

                    // Refresh recent notifications
                    LoadRecentNotifications();
                }
                else
                {
                    lblStatus.Text = "Failed to send notification. Please try again.";
                    lblStatus.Foreground = System.Windows.Media.Brushes.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending notification: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadRecentNotifications();
        }
    }
}