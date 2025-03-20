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
using Assignment_PRN.Models;

namespace Assignment_PRN
{
    /// <summary>
    /// Interaction logic for ConfirmTransferWindow.xaml
    /// </summary>

    public partial class ConfirmTransferWindow : Window
    {
        private int _requestId;
        private int _policeId; // ID của người xác nhận

        public ConfirmTransferWindow(int requestId, int policeId)
        {
            InitializeComponent();
            _requestId = requestId;
            _policeId = policeId;

            txtRequestId.Text = requestId.ToString();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e) 
        {
            if (cbStatus.SelectedItem == null)
            {
                MessageBox.Show("Please select a status!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string selectedStatus = ((ComboBoxItem)cbStatus.SelectedItem).Content.ToString();

            using (var context = new TestPrnassContext())
            {
                var request = context.Registrations.FirstOrDefault(r => r.RegistrationId == _requestId);
                if (request != null)
                {
                    request.ConfirmedStatus = selectedStatus;
                    request.ConfirmedBy = _policeId;
                    context.SaveChanges();
                    MessageBox.Show("Request has been updated!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            Close();
        }
    }
}
