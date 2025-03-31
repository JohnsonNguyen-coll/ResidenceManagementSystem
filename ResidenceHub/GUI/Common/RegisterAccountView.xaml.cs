using System;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;
using ResidenceHub.BLL;
using ResidenceHub.Models;
using System.Windows.Controls;

namespace ResidenceHub.GUI.Common
{
    public partial class RegisterAccountView : Window
    {
        private readonly UserBLL _userBLL;

        public RegisterAccountView()
        {
            InitializeComponent();
            _userBLL = new UserBLL();
            txtFullName.Focus();


            if (!IsAdminRegistration())
            {
                cmbRole.Items.RemoveAt(3); // Admin
                cmbRole.Items.RemoveAt(2); // Police
                cmbRole.Items.RemoveAt(1); // AreaLeader
            }
        }

        private bool IsAdminRegistration()
        {

            return false;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            lblError.Text = string.Empty;

            if (!ValidateInput())
                return;

            User newUser = new User
            {
                FullName = txtFullName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Password = txtPassword.Password,
                Role = ((ComboBoxItem)cmbRole.SelectedItem).Content.ToString(),
                Address = txtAddress.Text.Trim()
            };

            bool success = _userBLL.CreateUser(newUser);

            if (success)
            {
                MessageBox.Show("Account created successfully! You can now login.", "Registration Complete",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
            else
            {
                lblError.Text = "Unable to create account. Email may already be registered.";
            }
        }

        private bool ValidateInput()
        {
            // Check for empty fields
            if (string.IsNullOrEmpty(txtFullName.Text.Trim()))
            {
                lblError.Text = "Please enter your full name.";
                txtFullName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                lblError.Text = "Please enter your email address.";
                txtEmail.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtPassword.Password))
            {
                lblError.Text = "Please enter a password.";
                txtPassword.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtConfirmPassword.Password))
            {
                lblError.Text = "Please confirm your password.";
                txtConfirmPassword.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtAddress.Text.Trim()))
            {
                lblError.Text = "Please enter your address.";
                txtAddress.Focus();
                return false;
            }

            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(txtEmail.Text.Trim(), emailPattern))
            {
                lblError.Text = "Please enter a valid email address.";
                txtEmail.Focus();
                return false;
            }

            if (txtPassword.Password != txtConfirmPassword.Password)
            {
                lblError.Text = "Passwords do not match.";
                txtConfirmPassword.Focus();
                return false;
            }

            string passwordPattern = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";
            if (!Regex.IsMatch(txtPassword.Password, passwordPattern))
            {
                lblError.Text = "Password must be at least 8 characters and include both letters and numbers.";
                txtPassword.Focus();
                return false;
            }

            if (chkTerms.IsChecked != true)
            {
                lblError.Text = "You must agree to the terms and conditions.";
                chkTerms.Focus();
                return false;
            }

            return true;
        }

        private void txtLoginLink_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}