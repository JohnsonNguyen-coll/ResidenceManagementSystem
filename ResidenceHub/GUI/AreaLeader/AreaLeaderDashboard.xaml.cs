using System;
using System.Windows;
using ResidenceHub.BLL;
using ResidenceHub.Models;
using ResidenceHub.GUI.Common;
using System.Windows.Controls;

namespace ResidenceHub.GUI.AreaLeader
{
    public partial class AreaLeaderDashboard : Window
    {
        private readonly User _currentUser;
        private readonly LogBLL _logBLL;
        private readonly RegistrationBLL _registrationBLL;
        private readonly UserBLL _userBLL;
        private readonly HouseholdBLL _householdBLL;

        public AreaLeaderDashboard(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _logBLL = new LogBLL();
            _registrationBLL = new RegistrationBLL();
            _userBLL = new UserBLL();
            _householdBLL = new HouseholdBLL();

            // Set user name in header
            lblUserName.Text = _currentUser.FullName;

            // Log user access
            _logBLL.CreateLog(_currentUser.UserId, "Area Leader accessed dashboard");

            // Load statistics data - now for all records rather than by area
            LoadStatisticsData();

            // Load resident list view by default
            btnResidentList_Click(null, null);
        }

        private void LoadStatisticsData()
        {
            try
            {
                // Get all residents (citizens)
                var residents = _userBLL.GetUsersByRole("Citizen");
                txtResidentsCount.Text = residents.Count.ToString();

                // Get all households
                var households = _householdBLL.GetAllHouseholds();
                txtHouseholdsCount.Text = households.Count.ToString();

                // Get all pending verification requests
                var pendingRequests = _registrationBLL.GetRegistrationsByStatus("Pending");
                txtPendingVerifications.Text = $"{pendingRequests.Count} Pending Verifications";
            }
            catch (Exception ex)
            {
                // If there's an error, set default values
                MessageBox.Show($"Error loading statistics: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                txtResidentsCount.Text = "0";
                txtHouseholdsCount.Text = "0";
                txtPendingVerifications.Text = "0 Pending Verifications";
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

        private void btnResidentList_Click(object sender, RoutedEventArgs e)
        {
            // Set button visual states
            SetActiveButton(btnResidentList);

            // Load resident list view
            ResidentListView residentListView = new ResidentListView(_currentUser);
            MainFrame.Navigate(residentListView);
        }

        private void btnHouseholdList_Click(object sender, RoutedEventArgs e)
        {
            // Set button visual states
            SetActiveButton(btnHouseholdList);

            // Load household list view
            HouseholdListView householdListView = new HouseholdListView(_currentUser);
            MainFrame.Navigate(householdListView);
        }

        private void btnRequestVerification_Click(object sender, RoutedEventArgs e)
        {
            // Set button visual states
            SetActiveButton(btnRequestVerification);

            // Load request verification view
            RequestVerificationView requestVerificationView = new RequestVerificationView(_currentUser);
            MainFrame.Navigate(requestVerificationView);

            // Refresh statistics
            LoadStatisticsData();
        }

        private void btnSendNotification_Click(object sender, RoutedEventArgs e)
        {
            // Set button visual states
            SetActiveButton(btnSendNotification);

            // Load send notification view
            SendNotificationView sendNotificationView = new SendNotificationView(_currentUser);
            MainFrame.Navigate(sendNotificationView);
        }

        private void SetActiveButton(Button activeButton)
        {
            // Reset all buttons
            btnResidentList.Background = System.Windows.Media.Brushes.Transparent;
            btnHouseholdList.Background = System.Windows.Media.Brushes.Transparent;
            btnRequestVerification.Background = System.Windows.Media.Brushes.Transparent;
            btnSendNotification.Background = System.Windows.Media.Brushes.Transparent;

            // Set active button
            activeButton.Background = System.Windows.Media.Brushes.LightGray;
        }
    }
}