using System;
using System.Windows;
using ResidenceHub.BLL;
using ResidenceHub.Models;
using ResidenceHub.Helpers;
using System.Windows.Controls;

namespace ResidenceHub.GUI.Admin
{
    public partial class AddUserDialog : Window
    {
        private readonly User _currentUser;
        private readonly UserBLL _userBLL;
        private readonly LogBLL _logBLL;

        public AddUserDialog(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _userBLL = new UserBLL();
            _logBLL = new LogBLL();

            txtFullName.Focus();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            lblError.Text = "";

            if (string.IsNullOrEmpty(txtFullName.Text) ||
                string.IsNullOrEmpty(txtEmail.Text) ||
                string.IsNullOrEmpty(txtAddress.Text) ||
                string.IsNullOrEmpty(txtPassword.Password))
            {
                lblError.Text = "Please fill in all required fields.";
                return;
            }

            if (!ValidationHelper.IsValidEmail(txtEmail.Text))
            {
                lblError.Text = "Please enter a valid email address.";
                return;
            }

            if (!ValidationHelper.IsValidPassword(txtPassword.Password))
            {
                lblError.Text = "Password must be at least 6 characters long.";
                return;
            }

            if (txtPassword.Password != txtConfirmPassword.Password)
            {
                lblError.Text = "Passwords do not match.";
                return;
            }

            try
            {
                ComboBoxItem selectedRole = (ComboBoxItem)cmbRole.SelectedItem;
                string role = selectedRole.Content.ToString();

                var user = new User
                {
                    FullName = txtFullName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    Role = role,
                    Password = txtPassword.Password 
                };

                bool result = _userBLL.CreateUser(user);

                if (result)
                {
                    _logBLL.CreateLog(_currentUser.UserId, $"Added new {role}: {user.FullName}");

                    DialogResult = true;

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
            DialogResult = false;

            Close();
        }
    }
}