using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using Assignment_PRN.Models;

namespace Assignment_PRN
{
    /// <summary>
    /// Interaction logic for SubmitHouseholdTransferWindow.xaml
    /// </summary>
    public partial class SubmitHouseholdTransferWindow : Window
    {
        private int _userId;

        public SubmitHouseholdTransferWindow(int userId, string userName, string userEmail)
        {
            InitializeComponent();
            _userId = userId;

            // Load user information
            txtFullName.Text = userName;
            txtEmail.Text = userEmail;
        }



        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SubmitTransfer_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCurrentAddress.Text) || string.IsNullOrWhiteSpace(txtNewAddress.Text))
            {
                MessageBox.Show("Please fill in all required fields!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var context = new TestPrnassContext())
            {
                var newRegistration = new Registration
                {
                    UserId = _userId,
                    RegistrationType = "Permanent Transfer",
                    StartDate = DateOnly.FromDateTime(DateTime.Now),
                    Status = "Pending",
                    Comments = $"Current Address: {txtCurrentAddress.Text}, New Address: {txtNewAddress.Text}",
                    ConfirmedStatus = "Pending"
                };

                context.Registrations.Add(newRegistration);
                context.SaveChanges();

                MessageBox.Show("Household transfer request submitted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            Close();
        }
    }
}
