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
using Assignment_PRN;
using Assignment_PRN.Models;

namespace Assignment_PRN
{
    /// <summary>
    /// Interaction logic for CitizenHome.xaml
    /// </summary>
    public partial class CitizenHome : Window
    {
        public CitizenHome()
        {
            InitializeComponent();
            LoadRequests();
        }

        private void LoadRequests()
        {
            using (var context = new TestPrnassContext())
            {
                dataGridRequests.ItemsSource = context.Registrations
    .           Select(r => new { r.RegistrationId, r.RegistrationType, r.Status })
    .           ToList();

            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Notifications_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HouseholdSeparation_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HouseholdTransfer_Click(object sender, RoutedEventArgs e)
        {
            int userId = GetCurrentUserId(); // Lấy ID người dùng hiện tại
            string userName = GetCurrentUserName(); // Lấy tên người dùng (cần thêm hàm này nếu chưa có)
            string userEmail = GetCurrentUserEmail(); // Lấy email người dùng (cần thêm hàm này nếu chưa có)

            SubmitHouseholdTransferWindow transferWindow = new SubmitHouseholdTransferWindow(userId, userName, userEmail);
            transferWindow.ShowDialog();
        }

        private void UploadDocuments_Click(object sender, RoutedEventArgs e)
        {
            int userId = GetCurrentUserId();
            UploadDocumentsWindow uploadWindow = new UploadDocumentsWindow(userId);
            uploadWindow.ShowDialog();
        }


        private void CreateRegistration_Click(object sender, RoutedEventArgs e)
        {
            int userId = GetCurrentUserId(); // Get the logged-in user's ID
            CreateRegistrationWindow createRegistrationWindow = new CreateRegistrationWindow(userId);
            createRegistrationWindow.ShowDialog();
        }


        private void EditProfile_Click(object sender, RoutedEventArgs e)
        {
            int userId = GetCurrentUserId(); // Get the logged-in user's ID
            EditProfileWindow editProfileWindow = new EditProfileWindow(userId);
            editProfileWindow.ShowDialog();
        }



        private void ViewProfile_Click(object sender, RoutedEventArgs e)
        {
            int userId = GetCurrentUserId(); // Hàm lấy ID người dùng hiện tại
            ProfileWindow profileWindow = new ProfileWindow(userId);
            profileWindow.ShowDialog();
        }

        private int GetCurrentUserId()
        {
            // Ví dụ: Lấy userId từ session hoặc lưu trong biến toàn cục
            return 1; // Thay bằng cách lấy userId thực tế
        }

        private void TrackRequests_Click(object sender, RoutedEventArgs e)
        {
            int userId = GetCurrentUserId();
            TrackRequestWindow trackWindow = new TrackRequestWindow(userId);
            trackWindow.ShowDialog();
        }

        private string GetCurrentUserName()
        {
            using (var context = new TestPrnassContext())
            {
                var user = context.Users.FirstOrDefault(u => u.UserId == GetCurrentUserId());
                return user?.FullName ?? "Unknown"; // Nếu không tìm thấy user, trả về "Unknown"
            }
        }

        private string GetCurrentUserEmail()
        {
            using (var context = new TestPrnassContext())
            {
                var user = context.Users.FirstOrDefault(u => u.UserId == GetCurrentUserId());
                return user?.Email ?? "Unknown";
            }
        }

        private void ConfirmTransfer_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridRequests.SelectedItem is Registration selectedRequest)
            {
                int requestId = selectedRequest.RegistrationId;
                int policeId = GetCurrentUserId(); // Giả sử user đang đăng nhập là cảnh sát

                ConfirmTransferWindow confirmWindow = new ConfirmTransferWindow(requestId, policeId);
                confirmWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a request first!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


    }
}
