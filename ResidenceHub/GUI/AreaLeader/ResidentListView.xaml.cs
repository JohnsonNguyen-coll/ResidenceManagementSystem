using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ResidenceHub.BLL;
using ResidenceHub.Models;

namespace ResidenceHub.GUI.AreaLeader
{
    public partial class ResidentListView : Page
    {
        private readonly User _currentUser;
        private readonly UserBLL _userBLL;
        private readonly NotificationBLL _notificationBLL;
        private readonly LogBLL _logBLL;

        private List<User> _residents;
        private User _selectedResident;

        // Pagination
        private int _currentPage = 1;
        private int _pageSize = 20;
        private int _totalPages = 1;

        public ResidentListView(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _userBLL = new UserBLL();
            _notificationBLL = new NotificationBLL();
            _logBLL = new LogBLL();

            // Update label to show it's loading all residents
            lblArea.Text = "All Areas";

            // Load all residents
            LoadResidents();
        }

        private void LoadResidents(string searchTerm = "")
        {
            try
            {
                // Get all residents (citizens) instead of filtering by area
                _residents = _userBLL.GetUsersByRole("Citizen");

                // Apply search filter if provided
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    searchTerm = searchTerm.ToLower();
                    _residents = _residents.Where(r =>
                        r.FullName.ToLower().Contains(searchTerm) ||
                        r.Email.ToLower().Contains(searchTerm) ||
                        r.Address.ToLower().Contains(searchTerm)
                    ).ToList();
                }

                // Calculate pagination
                _totalPages = (_residents.Count + _pageSize - 1) / _pageSize;
                if (_currentPage > _totalPages && _totalPages > 0)
                {
                    _currentPage = _totalPages;
                }

                // Get current page of data
                var pagedResidents = _residents
                    .Skip((_currentPage - 1) * _pageSize)
                    .Take(_pageSize)
                    .ToList();

                // Update UI
                dgResidents.ItemsSource = pagedResidents;
                lblStatus.Text = $"Displaying {pagedResidents.Count} of {_residents.Count} residents";
                lblPage.Text = $"Page {_currentPage} of {_totalPages}";

                // Enable/disable navigation buttons
                btnPrevious.IsEnabled = _currentPage > 1;
                btnNext.IsEnabled = _currentPage < _totalPages;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading residents: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            _currentPage = 1; // Reset to first page on new search
            LoadResidents(searchTerm);
        }

        private void dgResidents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedResident = dgResidents.SelectedItem as User;
        }

        private void btnViewDetails_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                int userId = (int)button.Tag;
                var user = _userBLL.GetUserById(userId);

                if (user != null)
                {
                    // Display user details in a dialog
                    string message = $"ID: {user.UserId}\n" +
                                    $"Name: {user.FullName}\n" +
                                    $"Email: {user.Email}\n" +
                                    $"Address: {user.Address}\n" +
                                    $"Role: {user.Role}";

                    MessageBox.Show(message, "Resident Details", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void btnSendNotification_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                int userId = (int)button.Tag;
                var user = _userBLL.GetUserById(userId);

                if (user != null)
                {
                    // Show dialog to enter notification message
                    SendNotificationDialog dialog = new SendNotificationDialog(user);
                    if (dialog.ShowDialog() == true)
                    {
                        string message = dialog.NotificationMessage;

                        // Send notification
                        bool result = _notificationBLL.SendNotificationToUser(userId, message, _currentUser.UserId);

                        if (result)
                        {
                            MessageBox.Show("Notification sent successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                            // Log the action
                            _logBLL.CreateLog(_currentUser.UserId, $"Sent notification to user {userId}");
                        }
                        else
                        {
                            MessageBox.Show("Failed to send notification.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                LoadResidents(txtSearch.Text.Trim());
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage < _totalPages)
            {
                _currentPage++;
                LoadResidents(txtSearch.Text.Trim());
            }
        }
    }
}