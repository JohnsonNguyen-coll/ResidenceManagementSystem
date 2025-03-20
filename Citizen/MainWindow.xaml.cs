using System.Windows;
using Assignment_PRN.DAO;
using Assignment_PRN.Models;
using Assignment_PRN; // Import CitizenHome

namespace Assignment_PRN
{
    public partial class MainWindow : Window
    {
        private readonly UserDAO _userDao;

        public MainWindow()
        {
            InitializeComponent();
            _userDao = new UserDAO();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Password;

            var user = _userDao.AuthenticateUser(email, password);

            if (user != null)
            {
                MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                if (user.Role == "Citizen")
                {
                    CitizenHome citizenHome = new CitizenHome();
                    citizenHome.Show();  // Mở màn hình CitizenHome
                    this.Close();  // Đóng cửa sổ đăng nhập
                }
            }
            else
            {
                MessageBox.Show("Invalid email or password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
