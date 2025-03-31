using System;
using System.Windows;
using ResidenceHub.BLL;
using ResidenceHub.Models;
using ResidenceHub.GUI.Common;
using System.Windows.Controls;
namespace ResidenceHub.GUI.Police
{
    public partial class PoliceDashboard : Window
    {
        private readonly User _currentUser;
        private readonly LogBLL _logBLL;
        private readonly RegistrationBLL _registrationBLL;
        private readonly UserBLL _userBLL;

        public PoliceDashboard(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _logBLL = new LogBLL();
            _registrationBLL = new RegistrationBLL();
            _userBLL = new UserBLL();

            // Set user name in header
            lblUserName.Text = _currentUser.FullName;

            // Log user access
            _logBLL.CreateLog(_currentUser.UserId, "Police accessed dashboard");

            // Load district status data
            LoadDistrictStatusData();

            // Load resident management view by default
            btnResidentManagement_Click(null, null);
        }

        private void LoadDistrictStatusData()
        {
            try
            {
                // Get pending requests count (all of them, not just for a specific area)
                var pendingRequests = _registrationBLL.GetRegistrationsByStatus("Pending");
                txtPendingRequests.Text = pendingRequests.Count.ToString();

                // Get active residents count (all citizens)
                var residents = _userBLL.GetUsersByRole("Citizen");
                txtActiveResidents.Text = residents.Count.ToString();

                // Get high priority requests
                var highPriorityRequests = _registrationBLL.GetHighPriorityRequests();
                txtHighPriorityRequests.Text = $"{highPriorityRequests.Count} High-Priority Requests";

                // Set security level based on pending requests
                if (pendingRequests.Count > 10)
                {
                    txtSecurityLevel.Text = "Elevated";
                    txtSecurityLevel.Foreground = System.Windows.Media.Brushes.Orange;
                }
                else if (pendingRequests.Count > 20)
                {
                    txtSecurityLevel.Text = "High";
                    txtSecurityLevel.Foreground = System.Windows.Media.Brushes.Red;
                }
                else
                {
                    txtSecurityLevel.Text = "Normal";
                    txtSecurityLevel.Foreground = System.Windows.Media.Brushes.Green;
                }
            }
            catch (Exception ex)
            {
                // If there's an error, set default values
                MessageBox.Show($"Error loading district status: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                txtPendingRequests.Text = "0";
                txtActiveResidents.Text = "0";
                txtSecurityLevel.Text = "Normal";
                txtHighPriorityRequests.Text = "0 High-Priority Requests";
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

        private void btnResidentManagement_Click(object sender, RoutedEventArgs e)
        {
            // Set button visual states
            SetActiveButton(btnResidentManagement);

            // Load resident management view
            ResidentManagementView residentManagementView = new ResidentManagementView(_currentUser);
            MainFrame.Navigate(residentManagementView);
        }

        private void btnHouseholdManagement_Click(object sender, RoutedEventArgs e)
        {
            // Set button visual states
            SetActiveButton(btnHouseholdManagement);

            // Load household management view
            HouseholdManagementView householdManagementView = new HouseholdManagementView(_currentUser);
            MainFrame.Navigate(householdManagementView);
        }

        private void btnRequestApproval_Click(object sender, RoutedEventArgs e)
        {
            // Set button visual states
            SetActiveButton(btnRequestApproval);

            // Load request approval view
            RequestApprovalView requestApprovalView = new RequestApprovalView(_currentUser);
            MainFrame.Navigate(requestApprovalView);

            // Refresh district status data when viewing requests
            LoadDistrictStatusData();
        }

        private void btnReports_Click(object sender, RoutedEventArgs e)
        {
            // Set button visual states
            SetActiveButton(btnReports);

            // Load reports view
            ReportsView reportsView = new ReportsView(_currentUser);
            MainFrame.Navigate(reportsView);
        }

        private void btnNotifications_Click(object sender, RoutedEventArgs e)
        {
            // Set button visual states
            SetActiveButton(btnNotifications);

            // Load notifications view
            NotificationsView notificationsView = new NotificationsView(_currentUser);
            MainFrame.Navigate(notificationsView);
        }

        private void SetActiveButton(Button activeButton)
        {
            // Reset all buttons
            btnResidentManagement.Background = System.Windows.Media.Brushes.Transparent;
            btnHouseholdManagement.Background = System.Windows.Media.Brushes.Transparent;
            btnRequestApproval.Background = System.Windows.Media.Brushes.Transparent;
            btnReports.Background = System.Windows.Media.Brushes.Transparent;
            btnNotifications.Background = System.Windows.Media.Brushes.Transparent;

            // Set active button
            activeButton.Background = System.Windows.Media.Brushes.LightGray;
        }
    }
}