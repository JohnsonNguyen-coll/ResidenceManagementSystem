using System;
using System.Windows;
using ResidenceHub.BLL;
using ResidenceHub.Models;
using ResidenceHub.Helpers;
using System.Windows.Controls;

namespace ResidenceHub.GUI.Police
{
    public partial class AddResidentDialog : Window
    {
        private readonly User _currentUser;
        private readonly UserBLL _userBLL;
        private readonly LogBLL _logBLL;

        public AddResidentDialog(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _userBLL = new UserBLL();
            _logBLL = new LogBLL();

            // Set initial focus
            txtFullName.Focus();

            // Fill address with the current managed area
            string area = ExtractAreaFromAddress(_currentUser.Address);
            txtAddress.Text = area;
        }

        private string ExtractAreaFromAddress(string address)
        {
            // This is a simplified version - in a real app, you would have a more sophisticated way to determine the managed area
            string[] parts = address.Split(',');
            if (parts.Length > 1)
            {
                return parts[parts.Length - 2].Trim();
            }
            return address;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            lblError.Text = "";

            // Validate input
            if (string.IsNullOrEmpty(txtFullName.Text) ||
                string.IsNullOrEmpty(txtEmail.Text) ||
                string.IsNullOrEmpty(txtAddress.Text) ||
                string.IsNullOrEmpty(txtPassword.Password))
            {
                lblError.Text = "Please fill in all required fields.";
                return;
            }

            // Validate email
            if (!ValidationHelper.IsValidEmail(txtEmail.Text))
            {
                lblError.Text = "Please enter a valid email address.";
                return;
            }

            // Validate password
            if (!ValidationHelper.IsValidPassword(txtPassword.Password))
            {
                lblError.Text = "Password must be at least 6 characters long.";
                return;
            }

            // Validate password confirmation
            if (txtPassword.Password != txtConfirmPassword.Password)
            {
                lblError.Text = "Passwords do not match.";
                return;
            }

            try
            {
                // Get selected role
                ComboBoxItem selectedRole = (ComboBoxItem)cmbRole.SelectedItem;
                string role = selectedRole.Content.ToString();

                // Create user object
                var user = new User
                {
                    FullName = txtFullName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    Role = role,
                    Password = txtPassword.Password // In a real app, hash the password
                };

                // Add user to database
                bool result = _userBLL.CreateUser(user);

                if (result)
                {
                    // Log the action
                    _logBLL.CreateLog(_currentUser.UserId, $"Added new {role}: {user.FullName}");

                    // Set dialog result
                    DialogResult = true;

                    // Close dialog
                    Close();
                }
                else
                {
                    lblError.Text = "Failed to add user. Email may already be in use.";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = $"Error: {ex.Message}";
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // Set dialog result
            DialogResult = false;

            // Close dialog
            Close();
        }
    }
}