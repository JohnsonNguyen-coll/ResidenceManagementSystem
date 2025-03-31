using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ResidenceHub.BLL;
using ResidenceHub.Models;
using ResidenceHub.GUI.AreaLeader;
using System.IO;
using System.Windows.Media.Imaging;

namespace ResidenceHub.GUI.Police
{
    public partial class RequestApprovalView : Page
    {
        private readonly User _currentUser;
        private readonly RegistrationBLL _registrationBLL;
        private readonly UserBLL _userBLL;
        private readonly NotificationBLL _notificationBLL;
        private readonly LogBLL _logBLL;

        private Registration _selectedRegistration;
        private User _selectedUser;
        private bool _isInitialized = false;

        public RequestApprovalView(User user)
        {
            InitializeComponent();

            _currentUser = user;
            _registrationBLL = new RegistrationBLL();
            _userBLL = new UserBLL();
            _notificationBLL = new NotificationBLL();
            _logBLL = new LogBLL();

            // Set up the Loaded event to ensure UI is fully initialized
            this.Loaded += (s, e) =>
            {
                _isInitialized = true;
                // Update label to show it's loading all requests
                lblArea.Text = "All Areas";

                // Disable approval/rejection buttons initially
                btnApproveSelected.IsEnabled = false;
                btnRejectSelected.IsEnabled = false;

                // Load requests after UI is initialized
                LoadRequests();
            };
        }

        private void LoadRequests()
        {
            try
            {
                // Ensure component initialization
                if (!_isInitialized || _registrationBLL == null)
                {
                    return;
                }

                // Get status filter
                string statusFilter = "All";
                if (cmbStatus != null && cmbStatus.SelectedItem != null)
                {
                    statusFilter = ((ComboBoxItem)cmbStatus.SelectedItem).Content.ToString();
                }

                // Get type filter
                string typeFilter = "All";
                if (cmbType != null && cmbType.SelectedItem != null)
                {
                    typeFilter = ((ComboBoxItem)cmbType.SelectedItem).Content.ToString();
                }

                // Get all registration requests instead of filtering by area
                var requests = _registrationBLL.GetAllRegistrations();

                // Apply status filter
                if (statusFilter != "All")
                {
                    requests = requests.Where(r => r.Status == statusFilter).ToList();
                }

                // Apply type filter
                if (typeFilter != "All")
                {
                    requests = requests.Where(r => r.RegistrationType == typeFilter).ToList();
                }

                // Create view model with additional properties for display
                var requestsVM = new List<object>();

                foreach (var request in requests)
                {
                    // Get user name for display
                    string userName = "Unknown";
                    if (request.UserId.HasValue)
                    {
                        var user = _userBLL.GetUserById(request.UserId.Value);
                        if (user != null)
                        {
                            userName = user.FullName;
                        }
                    }

                    // Format dates safely
                    string startDate = request.StartDate != null ? request.StartDate.ToString() : "N/A";
                    string endDate = request.EndDate != null ? request.EndDate.ToString() : "N/A";

                    // Add to view model
                    requestsVM.Add(new
                    {
                        request.RegistrationId,
                        UserName = userName,
                        request.RegistrationType,
                        StartDate = startDate,
                        EndDate = endDate,
                        request.Status,
                        Comments = TruncateComments(request.Comments, 50),
                        ApproveButtonVisibility = request.Status == "Pending" ? Visibility.Visible : Visibility.Collapsed,
                        RejectButtonVisibility = request.Status == "Pending" ? Visibility.Visible : Visibility.Collapsed
                    });
                }

                // Bind the filtered results to the DataGrid
                dgRequests.ItemsSource = requestsVM;

                // Clear selection
                dgRequests.SelectedItem = null;
                _selectedRegistration = null;
                _selectedUser = null;

                // Clear details panel
                ClearDetailsPanel();
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                MessageBox.Show($"Error loading requests: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string TruncateComments(string comments, int maxLength)
        {
            if (string.IsNullOrEmpty(comments))
                return "";

            if (comments.Length <= maxLength)
                return comments;

            return comments.Substring(0, maxLength) + "...";
        }

        private void ClearDetailsPanel()
        {
            if (!_isInitialized)
                return;

            lblRequestId.Text = "--";
            lblRequestType.Text = "--";
            lblRequestDates.Text = "--";
            lblRequestStatus.Text = "--";
            lblResidentName.Text = "--";
            lblResidentEmail.Text = "--";
            lblResidentAddress.Text = "--";
        }

        private void cmbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isInitialized)
                LoadRequests();
        }

        private void cmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isInitialized)
                LoadRequests();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadRequests();
        }

        private void dgRequests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_isInitialized || dgRequests == null)
                return;

            if (dgRequests.SelectedItem != null)
            {
                try
                {
                    // Get the selected registration ID
                    var selectedItem = dgRequests.SelectedItem;
                    int registrationId = (int)selectedItem.GetType().GetProperty("RegistrationId").GetValue(selectedItem);

                    // Get the full registration object
                    _selectedRegistration = _registrationBLL.GetRegistrationById(registrationId);

                    if (_selectedRegistration != null && _selectedRegistration.UserId != null)
                    {
                        // Get the user
                        _selectedUser = _userBLL.GetUserById(_selectedRegistration.UserId.Value);

                        // Update details panel
                        UpdateDetailsPanel();

                        // Enable/disable approval/rejection buttons based on status
                        bool isPending = _selectedRegistration.Status == "Pending";
                        btnApproveSelected.IsEnabled = isPending;
                        btnRejectSelected.IsEnabled = isPending;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error selecting registration: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    ClearSelection();
                }
            }
            else
            {
                ClearSelection();
            }
        }

        private void ClearSelection()
        {
            _selectedRegistration = null;
            _selectedUser = null;

            // Disable approval/rejection buttons
            btnApproveSelected.IsEnabled = false;
            btnRejectSelected.IsEnabled = false;

            // Clear details panel
            ClearDetailsPanel();
        }

        private void UpdateDetailsPanel()
        {
            if (!_isInitialized || _selectedRegistration == null)
                return;

            try
            {
                lblRequestId.Text = _selectedRegistration.RegistrationId.ToString();
                lblRequestType.Text = _selectedRegistration.RegistrationType;
                lblRequestStatus.Text = _selectedRegistration.Status;

                string dates = "From: ";
                if (_selectedRegistration.StartDate != null)
                {
                    dates += _selectedRegistration.StartDate.ToString();
                }
                else
                {
                    dates += "N/A";
                }

                if (_selectedRegistration.EndDate != null)
                {
                    dates += $" To: {_selectedRegistration.EndDate.ToString()}";
                }
                lblRequestDates.Text = dates;

                lblRequestStatus.Text = _selectedRegistration.Status;

                if (_selectedUser != null)
                {
                    lblResidentName.Text = _selectedUser.FullName;
                    lblResidentEmail.Text = _selectedUser.Email;
                    lblResidentAddress.Text = _selectedUser.Address;
                }
                else
                {
                    lblResidentName.Text = "Unknown";
                    lblResidentEmail.Text = "Unknown";
                    lblResidentAddress.Text = "Unknown";
                }

                // 🖼 Load ảnh từ FileData
                if (_selectedRegistration.FileData != null && _selectedRegistration.FileData.Length > 0)
                {
                    imgCitizenID.Source = LoadImage(_selectedRegistration.FileData);
                }
                else
                {
                    imgCitizenID.Source = null; // Xóa ảnh nếu không có
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating details panel: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void btnApprove_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                int registrationId = (int)button.Tag;
                ApproveRegistration(registrationId);
            }
        }

        private void btnReject_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                int registrationId = (int)button.Tag;
                RejectRegistration(registrationId);
            }
        }

        private void btnApproveSelected_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedRegistration != null)
            {
                ApproveRegistration(_selectedRegistration.RegistrationId);
            }
        }

        private void btnRejectSelected_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedRegistration != null)
            {
                RejectRegistration(_selectedRegistration.RegistrationId);
            }
        }

        private void ApproveRegistration(int registrationId)
        {
            // Show dialog to enter comments
            var inputDialog = new ApprovalCommentDialog("Enter approval comments (optional):", "Approve Registration");
            if (inputDialog.ShowDialog() == true)
            {
                string comments = inputDialog.ResponseText;

                // Approve the registration
                bool result = _registrationBLL.ApproveRegistration(registrationId, _currentUser.UserId, comments);

                if (result)
                {
                    MessageBox.Show("Registration approved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Log the action
                    _logBLL.CreateLog(_currentUser.UserId, $"Approved registration request {registrationId}");

                    // Reload requests
                    LoadRequests();
                }
                else
                {
                    MessageBox.Show("Failed to approve registration.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void RejectRegistration(int registrationId)
        {
            // Show dialog to enter comments (required for rejection)
            var inputDialog = new ApprovalCommentDialog("Enter rejection reason:", "Reject Registration", true);
            if (inputDialog.ShowDialog() == true)
            {
                string comments = inputDialog.ResponseText;

                // Reject the registration
                bool result = _registrationBLL.RejectRegistration(registrationId, _currentUser.UserId, comments);

                if (result)
                {
                    MessageBox.Show("Registration rejected successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Log the action
                    _logBLL.CreateLog(_currentUser.UserId, $"Rejected registration request {registrationId}");

                    // Reload requests
                    LoadRequests();
                }
                else
                {
                    MessageBox.Show("Failed to reject registration.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
                return null;

            BitmapImage image = new BitmapImage();
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
            }
            return image;
        }


    }
}

