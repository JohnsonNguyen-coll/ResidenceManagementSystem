using System;
using System.Windows;
using System.Windows.Controls;
using ResidenceHub.BLL;
using ResidenceHub.Models;
using ResidenceHub.Helpers;

namespace ResidenceHub.GUI.Citizen
{
    public partial class PersonalInfoView : Page
    {
        private readonly User _currentUser;
        private readonly UserBLL _userBLL;
        private readonly LogBLL _logBLL;

        public PersonalInfoView(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _userBLL = new UserBLL();
            _logBLL = new LogBLL();

            // Load user information
            LoadUserInfo();
        }

        private void LoadUserInfo()
        {
            txtFullName.Text = _currentUser.FullName;
            txtEmail.Text = _currentUser.Email;
            txtAddress.Text = _currentUser.Address;
            txtRole.Text = _currentUser.Role;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Clear previous notification
            lblNotification.Text = "";

            // Validate input
            if (string.IsNullOrEmpty(txtFullName.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtAddress.Text))
            {
                lblNotification.Text = "Please fill in all required fields.";
                lblNotification.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }

            if (!ValidationHelper.IsValidEmail(txtEmail.Text))
            {
                lblNotification.Text = "Please enter a valid email address.";
                lblNotification.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }

            // Create updated user
            var updatedUser = new User
            {
                UserId = _currentUser.UserId,
                FullName = txtFullName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                Role = _currentUser.Role,
                Password = _currentUser.Password // Will be updated below if password is changed
            };

            // Check if password is being changed
            bool isPasswordChanged = !string.IsNullOrEmpty(txtCurrentPassword.Password) ||
                                     !string.IsNullOrEmpty(txtNewPassword.Password) ||
                                     !string.IsNullOrEmpty(txtConfirmPassword.Password);

            if (isPasswordChanged)
            {
                // Validate current password
                if (txtCurrentPassword.Password != _currentUser.Password) // In real app, use PasswordHasher.VerifyPassword
                {
                    lblNotification.Text = "Current password is incorrect.";
                    lblNotification.Foreground = System.Windows.Media.Brushes.Red;
                    return;
                }

                // Validate new password
                if (string.IsNullOrEmpty(txtNewPassword.Password) || txtNewPassword.Password.Length < 6)
                {
                    lblNotification.Text = "New password must be at least 6 characters.";
                    lblNotification.Foreground = System.Windows.Media.Brushes.Red;
                    return;
                }

                // Validate password confirmation
                if (txtNewPassword.Password != txtConfirmPassword.Password)
                {
                    lblNotification.Text = "Passwords do not match.";
                    lblNotification.Foreground = System.Windows.Media.Brushes.Red;
                    return;
                }

                // Set new password (in real app, use PasswordHasher.HashPassword)
                updatedUser.Password = txtNewPassword.Password;
            }

            // Update user
            bool result = _userBLL.UpdateUserProfile(updatedUser);

            if (result)
            {
                // Log the action
                _logBLL.CreateLog(_currentUser.UserId, "Updated personal information");

                // Update current user object
                _currentUser.FullName = updatedUser.FullName;
                _currentUser.Email = updatedUser.Email;
                _currentUser.Address = updatedUser.Address;
                _currentUser.Password = updatedUser.Password;

                // Show success message
                lblNotification.Text = "Profile updated successfully.";
                lblNotification.Foreground = System.Windows.Media.Brushes.Green;

                // Clear password fields
                txtCurrentPassword.Password = "";
                txtNewPassword.Password = "";
                txtConfirmPassword.Password = "";
            }
            else
            {
                lblNotification.Text = "Failed to update profile. Please try again.";
                lblNotification.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // Reset fields to original values
            LoadUserInfo();

            // Clear password fields
            txtCurrentPassword.Password = "";
            txtNewPassword.Password = "";
            txtConfirmPassword.Password = "";

            // Clear notification
            lblNotification.Text = "";
        }
    }
}