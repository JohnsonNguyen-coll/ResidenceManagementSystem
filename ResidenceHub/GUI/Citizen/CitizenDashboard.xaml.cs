using System;
using System.Windows;
using ResidenceHub.BLL;
using ResidenceHub.Models;
using ResidenceHub.GUI.Common;
using System.Windows.Controls;

namespace ResidenceHub.GUI.Citizen
{
    public partial class CitizenDashboard : Window
    {
        private readonly User _currentUser;
        private readonly LogBLL _logBLL;

        public CitizenDashboard(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _logBLL = new LogBLL();

            // Set user name in header
            lblUserName.Text = _currentUser.FullName;

            // Log user access
            _logBLL.CreateLog(_currentUser.UserId, "Citizen accessed dashboard");

            // Load personal information view by default
            btnPersonalInfo_Click(null, null);
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

        private void btnPersonalInfo_Click(object sender, RoutedEventArgs e)
        {
            // Set button visual states
            SetActiveButton(btnPersonalInfo);

            // Load personal information view
            PersonalInfoView personalInfoView = new PersonalInfoView(_currentUser);
            MainFrame.Navigate(personalInfoView);
        }

        private void btnRegistrations_Click(object sender, RoutedEventArgs e)
        {
            // Set button visual states
            SetActiveButton(btnRegistrations);

            // Load registrations view
            RegistrationRequestView registrationsView = new RegistrationRequestView(_currentUser);
            MainFrame.Navigate(registrationsView);
        }

        private void btnNotifications_Click(object sender, RoutedEventArgs e)
        {
            // Set button visual states
            SetActiveButton(btnNotifications);

            // Load notifications view
            NotificationsView notificationsView = new NotificationsView(_currentUser);
            MainFrame.Navigate(notificationsView);
        }

        private void btnHouseholdTransfer_Click(object sender, RoutedEventArgs e)
        {
            // Set button visual states
            SetActiveButton(btnHouseholdTransfer);

            // Load household transfer view
            HouseholdTransferView householdTransferView = new HouseholdTransferView(_currentUser);
            MainFrame.Navigate(householdTransferView);
        }

        private void SetActiveButton(Button activeButton)
        {
            // Reset all buttons
            btnPersonalInfo.Background = System.Windows.Media.Brushes.Transparent;
            btnRegistrations.Background = System.Windows.Media.Brushes.Transparent;
            btnNotifications.Background = System.Windows.Media.Brushes.Transparent;
            btnHouseholdTransfer.Background = System.Windows.Media.Brushes.Transparent;

            // Set active button
            activeButton.Background = System.Windows.Media.Brushes.LightGray;
        }
    }
}