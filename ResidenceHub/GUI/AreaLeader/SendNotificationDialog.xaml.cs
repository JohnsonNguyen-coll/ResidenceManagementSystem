using System;
using System.Windows;
using ResidenceHub.Models;

namespace ResidenceHub.GUI.AreaLeader
{
    public partial class SendNotificationDialog : Window
    {
        private readonly User _recipient;

        public string NotificationMessage { get; private set; }

        public SendNotificationDialog(User recipient)
        {
            InitializeComponent();
            _recipient = recipient;

            // Display recipient info
            lblRecipient.Text = $"{_recipient.FullName} - {_recipient.Email}";
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            // Validate message
            if (string.IsNullOrWhiteSpace(txtMessage.Text))
            {
                MessageBox.Show("Please enter a message.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Set the notification message
            NotificationMessage = txtMessage.Text.Trim();

            // Set dialog result
            DialogResult = true;

            // Close the dialog
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // Set dialog result
            DialogResult = false;

            // Close the dialog
            Close();
        }
    }
}