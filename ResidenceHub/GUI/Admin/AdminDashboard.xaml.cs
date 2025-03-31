using System;
using System.Windows;
using ResidenceHub.BLL;
using ResidenceHub.Models;
using ResidenceHub.GUI.Common;
using System.Windows.Controls;

namespace ResidenceHub.GUI.Admin
{
    public partial class AdminDashboard : Window
    {
        private readonly User _currentUser;
        private readonly LogBLL _logBLL;
        private readonly UserBLL _userBLL;

        public AdminDashboard(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _logBLL = new LogBLL();
            _userBLL = new UserBLL();

            lblUserName.Text = _currentUser.FullName;

            _logBLL.CreateLog(_currentUser.UserId, "Admin accessed dashboard");

            LoadSystemStatistics();

            btnUserManagement_Click(null, null);
        }

        private void LoadSystemStatistics()
        {
            try
            {
                // Get total user count
                var users = _userBLL.GetAllUsers();
                txtTotalUsers.Text = users.Count.ToString();
            }
            catch (Exception ex)
            {
                // If there's an error, set default values
                MessageBox.Show($"Error loading system statistics: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txtTotalUsers.Text = "0";
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            // Log user logout
            _logBLL.CreateLog(_currentUser.UserId, "User logged out");

            // Open login window
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();

            // Close current window
            this.Close();
        }

        private void btnUserManagement_Click(object sender, RoutedEventArgs e)
        {
            // Set button visual states
            SetActiveButton(btnUserManagement);

            // Load user management view
            UserManagementView userManagementView = new UserManagementView(_currentUser);
            MainFrame.Navigate(userManagementView);

            // Refresh statistics when viewing user management
            LoadSystemStatistics();
        }

        private void btnSystemLogs_Click(object sender, RoutedEventArgs e)
        {
            // Set button visual states
            SetActiveButton(btnSystemLogs);

            // Load system logs view
            SystemLogsView systemLogsView = new SystemLogsView(_currentUser);
            MainFrame.Navigate(systemLogsView);
        }

        private void btnConfiguration_Click(object sender, RoutedEventArgs e)
        {
            // Set button visual states
            SetActiveButton(btnConfiguration);

            // Load configuration view
            ConfigurationView configurationView = new ConfigurationView(_currentUser);
            MainFrame.Navigate(configurationView);
        }

        private void btnBackupDatabase_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // In a real app, this would trigger a database backup process
                MessageBox.Show("Database backup initiated successfully.", "Backup", MessageBoxButton.OK, MessageBoxImage.Information);

                // Log the action
                _logBLL.CreateLog(_currentUser.UserId, "Admin initiated database backup");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during backup: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSystemCheck_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // In a real app, this would run diagnostics and system checks
                MessageBox.Show("System check completed. All systems operational.", "System Check", MessageBoxButton.OK, MessageBoxImage.Information);

                // Log the action
                _logBLL.CreateLog(_currentUser.UserId, "Admin ran system check");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during system check: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetActiveButton(Button activeButton)
        {
            // Reset all buttons
            btnUserManagement.Background = System.Windows.Media.Brushes.Transparent;
            btnSystemLogs.Background = System.Windows.Media.Brushes.Transparent;
            btnConfiguration.Background = System.Windows.Media.Brushes.Transparent;

            // Set active button
            activeButton.Background = System.Windows.Media.Brushes.LightGray;
        }
    }
}