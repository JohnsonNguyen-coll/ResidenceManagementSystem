using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Assignment_PRN.Models;

namespace Assignment_PRN
{
    public partial class CreateRegistrationWindow : Window
    {
        private int _userId;

        public CreateRegistrationWindow(int userId)
        {
            InitializeComponent();
            _userId = userId;
            LoadRegistrationTypes();
        }

        private void LoadRegistrationTypes()
        {
            RegistrationDAO registrationsDAO = new RegistrationDAO();
            var registrations = registrationsDAO.GetRegistration();
            this.cmbRegistrationType.ItemsSource = registrations;
            this.cmbRegistrationType.DisplayMemberPath = "RegistrationType";
            this.cmbRegistrationType.SelectedValuePath = "RegistrationID";
            this.cmbRegistrationType.SelectedIndex = 0;
        }


        private void SubmitRegistration_Click(object sender, RoutedEventArgs e)
        {
            if (cmbRegistrationType.SelectedItem == null || dpStartDate.SelectedDate == null)
            {
                MessageBox.Show("Please fill in all required fields!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newRegistration = new Registration
            {
                UserId = _userId,
                RegistrationType = (cmbRegistrationType.SelectedItem as ComboBoxItem)?.Content.ToString(),
                StartDate = DateOnly.FromDateTime(dpStartDate.SelectedDate.Value),
                EndDate = dpEndDate.SelectedDate.HasValue ? DateOnly.FromDateTime(dpEndDate.SelectedDate.Value) : null,
                Status = "Pending",
                Comments = uploadedFilePath // Lưu đường dẫn file vào Comments
            };

            using (var context = new TestPrnassContext())
            {
                context.Registrations.Add(newRegistration);
                context.SaveChanges();
            }


            MessageBox.Show("Registration request submitted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }


        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private string uploadedFilePath = "";

        private void UploadDocuments_Click(object sender, RoutedEventArgs e)
        {
            UploadDocumentsWindow uploadWindow = new UploadDocumentsWindow(_userId);
            if (uploadWindow.ShowDialog() == true)
            {
                uploadedFilePath = uploadWindow.FilePath;
            }
        }

        private void TrackRequest_Click(object sender, RoutedEventArgs e)
        {
            TrackRequestWindow trackWindow = new TrackRequestWindow(_userId);
            trackWindow.ShowDialog();
        }


    }
}
