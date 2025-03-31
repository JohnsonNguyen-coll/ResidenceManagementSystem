using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ResidenceHub.BLL;
using ResidenceHub.Models;

namespace ResidenceHub.GUI.Admin
{
    public partial class UserManagementView : Page
    {
        private readonly User _currentUser;
        private readonly UserBLL _userBLL;
        private readonly LogBLL _logBLL;

        private List<User> _users;
        private User _selectedUser;

        public List<string> Roles { get; } = new List<string> { "Citizen", "AreaLeader", "Police", "Admin" };

        public UserManagementView(User user)
        {
            InitializeComponent();
            _currentUser = user;

            try
            {
                _userBLL = new UserBLL();
                _logBLL = new LogBLL();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing business logic: {ex.Message}", "Initialization Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.DataContext = this;

            LoadUsers();
        }

        private void LoadUsers(string roleFilter = "All", string searchTerm = "")
        {
            try
            {
                if (_userBLL == null)
                {
                    //MessageBox.Show("User business logic is not initialized.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                _users = _userBLL.GetAllUsers();

                if (roleFilter != "All")
                {
                    _users = _users.Where(u => u.Role == roleFilter).ToList();
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    searchTerm = searchTerm.ToLower();
                    _users = _users.Where(u =>
                        u.FullName.ToLower().Contains(searchTerm) ||
                        u.Email.ToLower().Contains(searchTerm) ||
                        u.Address.ToLower().Contains(searchTerm)
                    ).ToList();
                }

                // Set as data source for the datagrid
                if (dgUsers != null)
                {
                    dgUsers.ItemsSource = _users;

                    // Clear selection
                    dgUsers.SelectedItem = null;
                    _selectedUser = null;
                }

                // Update status
                if (lblStatus != null)
                {
                    lblStatus.Text = $"Displaying {_users.Count} users";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmbRoleFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbRoleFilter.SelectedItem != null)
            {
                string roleFilter;

                // Kiểm tra xem SelectedItem có phải là ComboBoxItem không
                if (cmbRoleFilter.SelectedItem is ComboBoxItem)
                {
                    roleFilter = ((ComboBoxItem)cmbRoleFilter.SelectedItem).Content?.ToString() ?? "All";
                }
                else
                {
                    // Nếu không, giả sử nó là chuỗi từ Roles list
                    roleFilter = cmbRoleFilter.SelectedItem.ToString();
                }

                string searchTerm = "";
                if (txtSearch != null)
                {
                    searchTerm = txtSearch.Text?.Trim() ?? "";
                }

                LoadUsers(roleFilter, searchTerm);
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string roleFilter;

            if (cmbRoleFilter != null && cmbRoleFilter.SelectedItem != null)
            {
                if (cmbRoleFilter.SelectedItem is ComboBoxItem)
                {
                    roleFilter = ((ComboBoxItem)cmbRoleFilter.SelectedItem).Content?.ToString() ?? "All";
                }
                else
                {
                    roleFilter = cmbRoleFilter.SelectedItem.ToString();
                }
            }
            else
            {
                roleFilter = "All";
            }

            string searchTerm = "";
            if (txtSearch != null)
            {
                searchTerm = txtSearch.Text?.Trim() ?? "";
            }

            LoadUsers(roleFilter, searchTerm);
        }

        private void dgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedUser = dgUsers.SelectedItem as User;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_userBLL == null)
            {
                MessageBox.Show("User business logic is not initialized.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Button button = sender as Button;
            if (button != null)
            {
                try
                {
                    int userId = (int)button.Tag;

                    // Find the user in the _users list
                    var editedUser = _users.FirstOrDefault(u => u.UserId == userId);

                    if (editedUser != null)
                    {
                        // Validate required fields
                        if (string.IsNullOrEmpty(editedUser.FullName) ||
                            string.IsNullOrEmpty(editedUser.Email) ||
                            string.IsNullOrEmpty(editedUser.Address) ||
                            string.IsNullOrEmpty(editedUser.Role))
                        {
                            MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        // Update user in the database
                        bool result = _userBLL.UpdateUser(editedUser);

                        if (result)
                        {
                            if (lblStatus != null)
                            {
                                lblStatus.Text = "User updated successfully.";
                                lblStatus.Foreground = System.Windows.Media.Brushes.Green;
                            }

                            // Log the action
                            if (_logBLL != null && _currentUser != null)
                            {
                                _logBLL.CreateLog(_currentUser.UserId, $"Updated user {userId}");
                            }
                        }
                        else
                        {
                            if (lblStatus != null)
                            {
                                lblStatus.Text = "Failed to update user.";
                                lblStatus.Foreground = System.Windows.Media.Brushes.Red;
                            }
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
            if (_userBLL == null)
            {
                MessageBox.Show("User business logic is not initialized.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Button button = sender as Button;
            if (button != null)
            {
                int userId = (int)button.Tag;

                // Prevent deleting current user
                if (_currentUser != null && userId == _currentUser.UserId)
                {
                    MessageBox.Show("You cannot delete your own account.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

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
                            if (lblStatus != null)
                            {
                                lblStatus.Text = "User deleted successfully.";
                                lblStatus.Foreground = System.Windows.Media.Brushes.Green;
                            }

                            // Log the action
                            if (_logBLL != null && _currentUser != null)
                            {
                                _logBLL.CreateLog(_currentUser.UserId, $"Deleted user {userId}");
                            }

                            // Reload users
                            string roleFilter;
                            if (cmbRoleFilter != null && cmbRoleFilter.SelectedItem != null)
                            {
                                if (cmbRoleFilter.SelectedItem is ComboBoxItem)
                                {
                                    roleFilter = ((ComboBoxItem)cmbRoleFilter.SelectedItem).Content?.ToString() ?? "All";
                                }
                                else
                                {
                                    roleFilter = cmbRoleFilter.SelectedItem.ToString();
                                }
                            }
                            else
                            {
                                roleFilter = "All";
                            }

                            string searchTerm = "";
                            if (txtSearch != null)
                            {
                                searchTerm = txtSearch.Text?.Trim() ?? "";
                            }

                            LoadUsers(roleFilter, searchTerm);
                        }
                        else
                        {
                            if (lblStatus != null)
                            {
                                lblStatus.Text = "Failed to delete user.";
                                lblStatus.Foreground = System.Windows.Media.Brushes.Red;
                            }
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
            if (_currentUser == null)
            {
                MessageBox.Show("Current user information is not available.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Show dialog to add new user
                var dialog = new AddUserDialog(_currentUser);
                if (dialog.ShowDialog() == true)
                {
                    // Reload users
                    string roleFilter;
                    if (cmbRoleFilter != null && cmbRoleFilter.SelectedItem != null)
                    {
                        if (cmbRoleFilter.SelectedItem is ComboBoxItem)
                        {
                            roleFilter = ((ComboBoxItem)cmbRoleFilter.SelectedItem).Content?.ToString() ?? "All";
                        }
                        else
                        {
                            roleFilter = cmbRoleFilter.SelectedItem.ToString();
                        }
                    }
                    else
                    {
                        roleFilter = "All";
                    }

                    string searchTerm = "";
                    if (txtSearch != null)
                    {
                        searchTerm = txtSearch.Text?.Trim() ?? "";
                    }

                    LoadUsers(roleFilter, searchTerm);

                    // Show success message
                    if (lblStatus != null)
                    {
                        lblStatus.Text = "New user added successfully.";
                        lblStatus.Foreground = System.Windows.Media.Brushes.Green;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding new user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            string roleFilter;
            if (cmbRoleFilter != null && cmbRoleFilter.SelectedItem != null)
            {
                if (cmbRoleFilter.SelectedItem is ComboBoxItem)
                {
                    roleFilter = ((ComboBoxItem)cmbRoleFilter.SelectedItem).Content?.ToString() ?? "All";
                }
                else
                {
                    roleFilter = cmbRoleFilter.SelectedItem.ToString();
                }
            }
            else
            {
                roleFilter = "All";
            }

            string searchTerm = "";
            if (txtSearch != null)
            {
                searchTerm = txtSearch.Text?.Trim() ?? "";
            }

            LoadUsers(roleFilter, searchTerm);
        }
    }
}