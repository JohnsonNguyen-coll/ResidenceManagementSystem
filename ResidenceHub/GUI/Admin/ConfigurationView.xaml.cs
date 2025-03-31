using System;
using System.Windows;
using System.Windows.Controls;
using ResidenceHub.BLL;
using ResidenceHub.Models;

namespace ResidenceHub.GUI.Admin
{
    public partial class ConfigurationView : Page
    {
        private readonly User _currentUser;
        private readonly LogBLL _logBLL;


        public ConfigurationView(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _logBLL = new LogBLL();

            LoadSettings();
        }

        private void LoadSettings()
        {

            txtSystemName.Text = "Residence Hub";
            txtAdminEmail.Text = "admin@residencehub.com";
            cmbDateFormat.SelectedIndex = 0; 

            cmbDefaultRegistrationType.SelectedIndex = 0;
            txtRegistrationApprovalDays.Text = "3";
            chkAutoExpireTemporary.IsChecked = true;

            chkSendEmailNotifications.IsChecked = true;
            chkSendSmsNotifications.IsChecked = false;
            txtNotificationTemplate.Text = "Hello {User},\n\n{Message}\n\nRegards,\n{SystemName} Team";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSystemName.Text) || string.IsNullOrEmpty(txtAdminEmail.Text))
                {
                    ShowError("System Name and Admin Email are required.");
                    return;
                }

                if (!IsValidEmail(txtAdminEmail.Text))
                {
                    ShowError("Please enter a valid email address.");
                    return;
                }

                int approvalDays;
                if (!int.TryParse(txtRegistrationApprovalDays.Text, out approvalDays) || approvalDays < 1)
                {
                    ShowError("Registration Approval Days must be a positive number.");
                    return;
                }


                _logBLL.CreateLog(_currentUser.UserId, "Updated system configuration");

                lblStatus.Text = "Configuration saved successfully.";
                lblStatus.Foreground = System.Windows.Media.Brushes.Green;
            }
            catch (Exception ex)
            {
                ShowError($"Error saving configuration: {ex.Message}");
            }
        }

        private void ShowError(string message)
        {
            lblStatus.Text = message;
            lblStatus.Foreground = System.Windows.Media.Brushes.Red;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to reset all configuration settings to default values?",
                "Confirm Reset",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                LoadSettings();

                _logBLL.CreateLog(_currentUser.UserId, "Reset system configuration to defaults");

                lblStatus.Text = "Configuration reset to default values.";
                lblStatus.Foreground = System.Windows.Media.Brushes.Green;
            }
        }
    }
}