using System;
using System.Windows;
using System.Windows.Input;
using ResidenceHub.BLL;
using ResidenceHub.Models;
using ResidenceHub.GUI.Citizen;
using ResidenceHub.GUI.AreaLeader;
using ResidenceHub.GUI.Police;
using ResidenceHub.GUI.Admin;

namespace ResidenceHub.GUI.Common
{
    public partial class LoginWindow : Window
    {
        private readonly UserBLL _userBLL;

        public LoginWindow()
        {
            InitializeComponent();
            _userBLL = new UserBLL();
            txtEmail.Focus();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Password;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                lblError.Text = "Please enter both email and password.";
                return;
            }

            var user = _userBLL.Authenticate(email, password);
            if (user != null)
            {
                OpenDashboard(user);
                this.Close();
            }
            else
            {
                lblError.Text = "Invalid email or password.";
                txtPassword.Password = string.Empty;
            }
        }

        private void OpenDashboard(User user)
        {
            switch (user.Role)
            {
                case "Citizen":
                    CitizenDashboard citizenDashboard = new CitizenDashboard(user);
                    citizenDashboard.Show();
                    break;
                case "AreaLeader":
                    AreaLeaderDashboard areaLeaderDashboard = new AreaLeaderDashboard(user);
                    areaLeaderDashboard.Show();
                    break;
                case "Police":
                    PoliceDashboard policeDashboard = new PoliceDashboard(user);
                    policeDashboard.Show();
                    break;
                case "Admin":
                    AdminDashboard adminDashboard = new AdminDashboard(user);
                    adminDashboard.Show();
                    break;
                default:
                    MessageBox.Show("Unknown user role.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }

        private void txtRegister_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RegisterAccountView registerView = new RegisterAccountView();
            registerView.Show();
            this.Close();
        }
    }
}