using System;
using System.Windows;

namespace ResidenceHub.GUI.AreaLeader
{
    public partial class ApprovalCommentDialog : Window
    {
        private readonly bool _isRequired;

        public string ResponseText { get; private set; }

        public ApprovalCommentDialog(string prompt, string title, bool isRequired = false)
        {
            InitializeComponent();

            this.Title = title;
            lblPrompt.Text = prompt;
            _isRequired = isRequired;

            if (_isRequired)
            {
                lblPrompt.Text += " (Required)";
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            // Check if input is required
            if (_isRequired && string.IsNullOrWhiteSpace(txtComments.Text))
            {
                MessageBox.Show("Please enter a comment.", "Required Field", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Set the response text
            ResponseText = txtComments.Text.Trim();

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