using System.Linq;
using System.Windows;
using Assignment_PRN.Models;

namespace Assignment_PRN
{
    public partial class EditProfileWindow : Window
    {
        private int _userId;

        public EditProfileWindow(int userId)
        {
            InitializeComponent();
            _userId = userId;
            LoadUserProfile();
        }

        private void LoadUserProfile()
        {
            using (var context = new TestPrnassContext())
            {
                var user = context.Users.FirstOrDefault(u => u.UserId == _userId);
                if (user != null)
                {
                    txtFullName.Text = user.FullName;
                    txtEmail.Text = user.Email;
                    txtAddress.Text = user.Address;
                }
                else
                {
                    MessageBox.Show("User not found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Close();
                }
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new TestPrnassContext())
            {
                var user = context.Users.FirstOrDefault(u => u.UserId == _userId);
                if (user != null)
                {
                    user.FullName = txtFullName.Text;
                    user.Email = txtEmail.Text;
                    user.Address = txtAddress.Text;

                    context.SaveChanges();
                    MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("Error updating profile!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
