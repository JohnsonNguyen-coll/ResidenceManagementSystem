using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ResidenceHub.BLL;
using ResidenceHub.Models;

namespace ResidenceHub.GUI.Police
{
    public partial class NotificationsView : Page
    {
        private readonly User _currentUser;
        private readonly UserBLL _userBLL;
        private readonly NotificationBLL _notificationBLL;
        private readonly LogBLL _logBLL;

        private List<User> _residents;
        private bool _isInitialized = false;

        public NotificationsView(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _userBLL = new UserBLL();
            _notificationBLL = new NotificationBLL();
            _logBLL = new LogBLL();

            // Set flag to indicate initialization is complete
            _isInitialized = true;

            // Load all citizens
            LoadResidents();

            // Load recent notifications
            LoadRecentNotifications();

            // Initial setup of pnlSpecificResident visibility based on the default selection
            if (cmbRecipientType.SelectedItem != null && pnlSpecificResident != null)
            {
                var selectedItem = (ComboBoxItem)cmbRecipientType.SelectedItem;
                string selectedType = selectedItem.Content.ToString();

                pnlSpecificResident.Visibility = selectedType == "Specific Resident" ?
                                                 Visibility.Visible :
                                                 Visibility.Collapsed;
            }
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
                // In a real application, we would retrieve this from the database
                // For now, we're using mock data
                var recentNotifications = new List<object>
                {
                    new { RecipientType = "All Residents", Message = "Monthly security report: Crime rate decreased by 15% in our area. Keep up the vigilance!", SentDate = "2023-05-15 14:30" },
                    new { RecipientType = "Area Leaders", Message = "Reminder: Submit monthly area reports by the 25th.", SentDate = "2023-05-10 09:15" },
                    new { RecipientType = "Specific Resident: John Smith", Message = "Please update your household registration information.", SentDate = "2023-05-05 11:45" }
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
            // Skip if the initialization is not complete or UI elements are null
            if (!_isInitialized || cmbRecipientType == null || pnlSpecificResident == null)
                return;

            try
            {
                if (cmbRecipientType.SelectedItem != null)
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
                // Log the error instead of showing a message box to avoid disrupting the UI
                Console.WriteLine($"Error in recipient type selection changed: {ex.Message}");
            }
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Make sure controls are not null
                if (cmbRecipientType == null || txtMessage == null || lblStatus == null)
                {
                    MessageBox.Show("UI controls not properly initialized.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Get selected recipient type
                if (cmbRecipientType.SelectedItem == null)
                {
                    MessageBox.Show("Please select a recipient type.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

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
                        if (pnlSpecificResident == null || cmbResidents == null || cmbResidents.SelectedItem == null)
                        {
                            MessageBox.Show("Please select a resident.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        User selectedResident = (User)cmbResidents.SelectedItem;
                        result = _notificationBLL.SendNotificationToUser(selectedResident.UserId, message, _currentUser.UserId);
                        break;

                    case "Area Leaders":
                        // Changed to send to all area leaders instead of just those in an area
                        var allLeaders = _userBLL.GetUsersByRole("AreaLeader");
                        result = true;
                        foreach (var leader in allLeaders)
                        {
                            bool sent = _notificationBLL.SendNotificationToUser(leader.UserId, message, _currentUser.UserId);
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