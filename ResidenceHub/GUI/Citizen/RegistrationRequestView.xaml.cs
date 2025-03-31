using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32; 
using ResidenceHub.BLL;
using ResidenceHub.Models;

namespace ResidenceHub.GUI.Citizen
{
    public partial class RegistrationRequestView : Page
    {
        private readonly User _currentUser;
        private readonly RegistrationBLL _registrationBLL;
        private readonly UserBLL _userBLL;
        private readonly LogBLL _logBLL;
        private Registration _selectedRegistration;
        private byte[]? uploadedImageData;
        private string? uploadedFileName;

        public RegistrationRequestView(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _registrationBLL = new RegistrationBLL();
            _userBLL = new UserBLL();
            _logBLL = new LogBLL();

            cmbRegistrationType.SelectedIndex = 0;
            dpStartDate.SelectedDate = DateTime.Now;

            LoadRegistrations();

            btnDelete.IsEnabled = false;

            if (txtNoImage != null) txtNoImage.Visibility = Visibility.Visible;
            if (txtImageRequired != null) txtImageRequired.Visibility = Visibility.Visible;
        }

        private void LoadRegistrations()
        {
            var registrations = _registrationBLL.GetRegistrationsByUser(_currentUser.UserId);

            var registrationsVM = registrations.Select(r => new
            {
                r.RegistrationId,
                r.RegistrationType,
                StartDate = r.StartDate.ToString("yyyy-MM-dd"),
                EndDate = r.EndDate.HasValue ? r.EndDate.Value.ToString("yyyy-MM-dd") : "",
                r.Status,
                ApprovedByName = r.ApprovedByNavigation?.FullName ?? "",
                r.Comments
            }).ToList();

            dgRequests.ItemsSource = registrationsVM;

           
            dgRequests.SelectedItem = null;
            _selectedRegistration = null;
            btnDelete.IsEnabled = false;
            imgPreview.Source = null; 

            if (txtNoImage != null) txtNoImage.Visibility = Visibility.Visible;
            if (txtImageRequired != null) txtImageRequired.Visibility = Visibility.Visible;
            lblFileName.Text = "* No file chosen (required)";
            lblFileName.Foreground = System.Windows.Media.Brushes.Red;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            lblNotification.Text = "";

            if (cmbRegistrationType.SelectedItem == null || dpStartDate.SelectedDate == null)
            {
                lblNotification.Text = "Please fill in all required fields.";
                lblNotification.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }

            if (uploadedImageData == null || uploadedFileName == null)
            {
                lblNotification.Text = "You must upload an identity card image before submitting.";
                lblNotification.Foreground = System.Windows.Media.Brushes.Red;

                lblFileName.Foreground = System.Windows.Media.Brushes.Red;
                lblFileName.Text = "* No file chosen (required)";
                if (txtImageRequired != null) txtImageRequired.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }

            string registrationType = ((ComboBoxItem)cmbRegistrationType.SelectedItem).Content.ToString();

            if ((registrationType == "Temporary" || registrationType == "TemporaryStay") && dpEndDate.SelectedDate == null)
            {
                lblNotification.Text = "End date is required for temporary registrations.";
                lblNotification.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }

            if (dpEndDate.SelectedDate != null && dpStartDate.SelectedDate >= dpEndDate.SelectedDate)
            {
                lblNotification.Text = "End date must be after start date.";
                lblNotification.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }

            var registration = new Registration
            {
                UserId = _currentUser.UserId,
                RegistrationType = registrationType,
                StartDate = DateOnly.FromDateTime(dpStartDate.SelectedDate.Value),
                EndDate = dpEndDate.SelectedDate != null ? DateOnly.FromDateTime(dpEndDate.SelectedDate.Value) : null,
                Status = "Pending",
                Comments = txtComments.Text,
                FileName = uploadedFileName,
                FileData = uploadedImageData
            };

            bool result = _registrationBLL.CreateRegistration(registration);

            if (result)
            {
                lblNotification.Text = "Registration request submitted successfully.";
                lblNotification.Foreground = System.Windows.Media.Brushes.Green;

                cmbRegistrationType.SelectedIndex = 0;
                dpStartDate.SelectedDate = DateTime.Now;
                dpEndDate.SelectedDate = null;
                txtComments.Text = "";
                uploadedImageData = null;
                uploadedFileName = null;
                lblFileName.Text = "* No file chosen (required)";
                lblFileName.Foreground = System.Windows.Media.Brushes.Red;
                imgPreview.Source = null;

                // Show the placeholder text
                if (txtNoImage != null) txtNoImage.Visibility = Visibility.Visible;
                if (txtImageRequired != null)
                {
                    txtImageRequired.Visibility = Visibility.Visible;
                    txtImageRequired.Foreground = System.Windows.Media.Brushes.Red;
                }

                LoadRegistrations();
            }
            else
            {
                lblNotification.Text = "Failed to submit registration request. Please try again.";
                lblNotification.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                uploadedFileName = openFileDialog.FileName;
                lblFileName.Text = Path.GetFileName(uploadedFileName);
                lblFileName.Foreground = System.Windows.Media.Brushes.Black; 
                uploadedImageData = File.ReadAllBytes(uploadedFileName);

                BitmapImage bitmap = new BitmapImage();
                using (var stream = new MemoryStream(uploadedImageData))
                {
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                }
                imgPreview.Source = bitmap;

                if (txtNoImage != null) txtNoImage.Visibility = Visibility.Collapsed;
                if (txtImageRequired != null)
                {
                    txtImageRequired.Visibility = Visibility.Collapsed;
                 
                }
            }
        }

        private void dgRequests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgRequests.SelectedItem != null)
            {
                var selectedItem = dgRequests.SelectedItem;
                int registrationId = (int)selectedItem.GetType().GetProperty("RegistrationId").GetValue(selectedItem);

                _selectedRegistration = _registrationBLL.GetRegistrationById(registrationId);

                btnDelete.IsEnabled = _selectedRegistration.Status == "Pending";

                if (_selectedRegistration.FileData != null)
                {
                    BitmapImage bitmap = new BitmapImage();
                    using (var stream = new MemoryStream(_selectedRegistration.FileData))
                    {
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.StreamSource = stream;
                        bitmap.EndInit();
                    }
                    imgPreview.Source = bitmap;

                    if (txtNoImage != null) txtNoImage.Visibility = Visibility.Collapsed;
                    if (txtImageRequired != null) txtImageRequired.Visibility = Visibility.Collapsed;
                }
                else
                {
                    imgPreview.Source = null;

                    if (txtNoImage != null) txtNoImage.Visibility = Visibility.Visible;
                    if (txtImageRequired != null) txtImageRequired.Visibility = Visibility.Visible;
                }
            }
            else
            {
                _selectedRegistration = null;
                btnDelete.IsEnabled = false;
                imgPreview.Source = null;

                if (txtNoImage != null) txtNoImage.Visibility = Visibility.Visible;
                if (txtImageRequired != null) txtImageRequired.Visibility = Visibility.Visible;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadRegistrations(); 
            lblNotification.Text = "List has been refreshed!";
            lblNotification.Foreground = System.Windows.Media.Brushes.Green;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedRegistration == null)
            {
                MessageBox.Show("Please select a request to delete.", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_selectedRegistration.Status != "Pending")
            {
                MessageBox.Show("You can only delete pending requests.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to delete this request?", "Confirm Deletion",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                bool deleteSuccess = _registrationBLL.DeleteRegistration(_selectedRegistration.RegistrationId, _currentUser.UserId);

                if (deleteSuccess)
                {
                    lblNotification.Text = "Request has been deleted successfully!";
                    lblNotification.Foreground = System.Windows.Media.Brushes.Green;
                    LoadRegistrations(); 
                }
                else
                {
                    lblNotification.Text = "Error deleting request.";
                    lblNotification.Foreground = System.Windows.Media.Brushes.Red;
                }
            }
        }
    }
}