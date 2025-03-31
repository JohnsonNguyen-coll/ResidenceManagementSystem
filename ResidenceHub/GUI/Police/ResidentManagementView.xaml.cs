using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ResidenceHub.BLL;
using ResidenceHub.Models;


namespace ResidenceHub.GUI.Police
{
    public partial class ResidentManagementView : Page
    {
        private readonly User _currentUser;
        private readonly UserBLL _userBLL;
        private readonly LogBLL _logBLL;

        private List<User> _residents;
        private User _selectedResident;

        public ResidentManagementView(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _userBLL = new UserBLL();
            _logBLL = new LogBLL();

            // Update label to show it's loading all citizens
            lblArea.Text = "All Areas";

            // Load all citizens
            LoadResidents();
        }

        private void LoadResidents(string searchTerm = "")
        {
            try
            {
                // Get all citizens instead of filtering by area
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

                // Set as data source for the datagrid
                dgResidents.ItemsSource = _residents;

                // Clear selection
                dgResidents.SelectedItem = null;
                _selectedResident = null;

                // Update status
                lblStatus.Text = $"Displaying {_residents.Count} residents";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading residents: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            LoadResidents(searchTerm);
        }

        private void dgResidents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedResident = dgResidents.SelectedItem as User;
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                try
                {
                    int userId = (int)button.Tag;

                    // Find the user in the _residents list
                    var editedUser = _residents.FirstOrDefault(r => r.UserId == userId);

                    if (editedUser != null)
                    {
                        // Update user in the database
                        bool result = _userBLL.UpdateUser(editedUser);

                        if (result)
                        {
                            lblStatus.Text = "User updated successfully.";
                            lblStatus.Foreground = System.Windows.Media.Brushes.Green;

                            // Log the action
                            _logBLL.CreateLog(_currentUser.UserId, $"Updated user {userId}");
                        }
                        else
                        {
                            lblStatus.Text = "Failed to update user.";
                            lblStatus.Foreground = System.Windows.Media.Brushes.Red;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                int userId = (int)button.Tag;

                // Confirm deletion
                MessageBoxResult result = MessageBox.Show(
                    "Are you sure you want to delete this user?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Delete user
                        bool success = _userBLL.DeleteUser(userId);

                        if (success)
                        {
                            lblStatus.Text = "User deleted successfully.";
                            lblStatus.Foreground = System.Windows.Media.Brushes.Green;

                            // Log the action
                            _logBLL.CreateLog(_currentUser.UserId, $"Deleted user {userId}");

                            // Reload residents
                            LoadResidents(txtSearch.Text.Trim());
                        }
                        else
                        {
                            lblStatus.Text = "Failed to delete user.";
                            lblStatus.Foreground = System.Windows.Media.Brushes.Red;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Show dialog to add new user
            var dialog = new AddResidentDialog(_currentUser);
            if (dialog.ShowDialog() == true)
            {
                // Reload residents
                LoadResidents(txtSearch.Text.Trim());

                // Show success message
                lblStatus.Text = "New resident added successfully.";
                lblStatus.Foreground = System.Windows.Media.Brushes.Green;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadResidents(txtSearch.Text.Trim());
        }
    }
}