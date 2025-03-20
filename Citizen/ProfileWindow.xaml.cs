using System.Linq;
using System.Windows;
using Assignment_PRN.Models; // Import models để truy vấn DB

namespace Assignment_PRN
{
        public partial class ProfileWindow : Window
        {
            public ProfileWindow(int userId)
            {
                InitializeComponent();
                LoadUserProfile(userId);
            }

            private void LoadUserProfile(int userId)
            {
                using (var context = new TestPrnassContext())
                {
                    var user = context.Users
                        .Where(u => u.UserId == userId)
                        .Select(u => new
                        {
                            u.FullName,
                            u.Email,
                            u.Address,
                            u.Role
                        })
                        .FirstOrDefault();

                    if (user != null)
                    {
                        DataContext = user;
                    }
                    else
                    {
                        MessageBox.Show("No user information found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        Close();
                    }
                }
            }

            private void CloseWindow(object sender, RoutedEventArgs e)
            {
                this.Close();
            }
        }
    }

