using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using ResidenceHub.BLL;
using ResidenceHub.Models;

namespace ResidenceHub.GUI.Admin
{
    public partial class SystemLogsView : Page
    {
        private readonly User _currentUser;
        private readonly LogBLL _logBLL;
        private readonly UserBLL _userBLL;

        private List<Log> _logs;

        public SystemLogsView(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _logBLL = new LogBLL();
            _userBLL = new UserBLL();

            dpEndDate.SelectedDate = DateTime.Now;
            dpStartDate.SelectedDate = DateTime.Now.AddDays(-7);

            LoadUsers();

            LoadLogs();
        }

        private void LoadUsers()
        {
            try
            {
                var users = _userBLL.GetAllUsers();

                var allUsersOption = new User { UserId = 0, FullName = "All Users" };
                var usersList = new List<User> { allUsersOption };
                usersList.AddRange(users);

                cmbUserFilter.ItemsSource = usersList;

                cmbUserFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadLogs()
        {
            try
            {
                DateTime? startDate = dpStartDate.SelectedDate;
                DateTime? endDate = dpEndDate.SelectedDate;

                if (endDate.HasValue)
                {
                    endDate = endDate.Value.AddDays(1).AddSeconds(-1);
                }

                User selectedUser = cmbUserFilter.SelectedItem as User;
                int? userId = (selectedUser != null && selectedUser.UserId != 0) ? selectedUser.UserId : null;

                string actionFilter = txtActionFilter.Text.Trim();

                if (startDate.HasValue && endDate.HasValue)
                {
                    _logs = _logBLL.GetLogsByDateRange(startDate.Value, endDate.Value);
                }
                else
                {
                    _logs = _logBLL.GetAllLogs();
                }

                if (userId.HasValue)
                {
                    _logs = _logs.Where(l => l.UserId == userId.Value).ToList();
                }

                if (!string.IsNullOrEmpty(actionFilter))
                {
                    _logs = _logs.Where(l => l.Action.ToLower().Contains(actionFilter.ToLower())).ToList();
                }

                var logsVM = _logs.Select(l => new
                {
                    l.LogId,
                    UserName = l.User?.FullName ?? "Unknown",
                    l.Action,
                    Timestamp = l.Timestamp?.ToString("yyyy-MM-dd HH:mm:ss") ?? "Unknown"
                }).OrderByDescending(l => l.Timestamp).ToList();

                dgLogs.ItemsSource = logsVM;

                lblStatus.Text = $"Displaying {logsVM.Count} logs";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading logs: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmbUserFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            if (dpStartDate.SelectedDate.HasValue && dpEndDate.SelectedDate.HasValue)
            {
                if (dpStartDate.SelectedDate > dpEndDate.SelectedDate)
                {
                    MessageBox.Show("Start date must be before or equal to end date.", "Invalid Date Range", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            LoadLogs();
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_logs == null || _logs.Count == 0)
                {
                    MessageBox.Show("No logs to export.", "Export", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Set up save file dialog
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV Files (*.csv)|*.csv|All files (*.*)|*.*",
                    DefaultExt = "csv",
                    FileName = $"SystemLogs_{DateTime.Now:yyyyMMdd}"
                };

                // Show save file dialog
                if (saveFileDialog.ShowDialog() == true)
                {
                    // Export to CSV
                    ExportToCsv(saveFileDialog.FileName);

                    // Show success message
                    MessageBox.Show($"Logs exported successfully to {saveFileDialog.FileName}", "Export Successful", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Log the action
                    _logBLL.CreateLog(_currentUser.UserId, $"Exported system logs to CSV");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting logs: {ex.Message}", "Export Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExportToCsv(string filePath)
        {
            // This is a simplified CSV export
            using (var writer = new System.IO.StreamWriter(filePath))
            {
                // Write header
                writer.WriteLine("LogID,User,Action,Timestamp");

                // Write data
                foreach (var log in _logs)
                {
                    string userName = log.User?.FullName ?? "Unknown";
                    string timestamp = log.Timestamp?.ToString("yyyy-MM-dd HH:mm:ss") ?? "Unknown";
                    string action = log.Action.Replace(",", ";"); // Escape commas

                    writer.WriteLine($"{log.LogId},\"{userName}\",\"{action}\",\"{timestamp}\"");
                }
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadLogs();
        }
    }
}