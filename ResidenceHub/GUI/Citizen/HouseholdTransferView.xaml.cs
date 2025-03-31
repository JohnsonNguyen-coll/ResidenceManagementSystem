using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ResidenceHub.BLL;
using ResidenceHub.Models;

namespace ResidenceHub.GUI.Citizen
{
    public partial class HouseholdTransferView : Page
    {
        private readonly User _currentUser;
        private readonly HouseholdBLL _householdBLL;
        private readonly HouseholdMemberBLL _householdMemberBLL;
        private readonly RegistrationBLL _registrationBLL;
        private readonly LogBLL _logBLL;

        private Household _currentHousehold;
        private HouseholdMember _currentMembership;

        public HouseholdTransferView(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _householdBLL = new HouseholdBLL();
            _householdMemberBLL = new HouseholdMemberBLL();
            _registrationBLL = new RegistrationBLL();
            _logBLL = new LogBLL();

            cmbTransferType.SelectedIndex = 0;
            dpStartDate.SelectedDate = DateTime.Now;

            LoadHouseholdInfo();
        }

        private void LoadHouseholdInfo()
        {
            try
            {
                var memberships = _householdMemberBLL.GetHouseholdsByMember(_currentUser.UserId);

                if (memberships != null && memberships.Count > 0)
                {
                    _currentMembership = memberships[0];

                    if (_currentMembership != null && _currentMembership.HouseholdId.HasValue)
                    {
                        _currentHousehold = _householdBLL.GetHouseholdById(_currentMembership.HouseholdId.Value);

                        if (_currentHousehold != null)
                        {
                            // Display household information
                            lblHouseholdId.Text = _currentHousehold.HouseholdId.ToString();
                            lblAddress.Text = _currentHousehold.Address;
                            lblHeadOfHousehold.Text = _currentHousehold.HeadOfHousehold?.FullName ?? "Unknown";
                            lblRelationship.Text = _currentMembership.Relationship;

                            // Get and display household members
                            var members = _householdMemberBLL.GetMembersByHousehold(_currentHousehold.HouseholdId);

                            if (members != null)
                            {
                                var membersVM = members.Select(m => new
                                {
                                    FullName = m.User?.FullName ?? "Unknown",
                                    m.Relationship
                                }).ToList();

                                lvMembers.ItemsSource = membersVM;
                            }

                            // Enable form
                            EnableForm(true);
                            return;
                        }
                    }
                }

                // If we get here, no valid household was found
                ShowNoHouseholdInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading household information: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ShowNoHouseholdInfo();
            }
        }

        private void ShowNoHouseholdInfo()
        {
            // No household found
            lblHouseholdId.Text = "N/A";
            lblAddress.Text = "You are not currently registered to any household.";
            lblHeadOfHousehold.Text = "N/A";
            lblRelationship.Text = "N/A";

            lvMembers.ItemsSource = null;

            // Disable form
            EnableForm(false);

            // Show notification
            lblNotification.Text = "You must be registered to a household before requesting a transfer.";
            lblNotification.Foreground = System.Windows.Media.Brushes.Red;
        }

        private void EnableForm(bool enable)
        {
            txtNewAddress.IsEnabled = enable;
            dpStartDate.IsEnabled = enable;
            cmbTransferType.IsEnabled = enable;
            dpEndDate.IsEnabled = enable;
            txtReason.IsEnabled = enable;
            txtDocuments.IsEnabled = enable;
            btnSubmit.IsEnabled = enable;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            // Clear previous notification
            lblNotification.Text = "";

            // Validate input
            if (string.IsNullOrEmpty(txtNewAddress.Text) || dpStartDate.SelectedDate == null || cmbTransferType.SelectedItem == null)
            {
                lblNotification.Text = "Please fill in all required fields.";
                lblNotification.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }

            // Get transfer type
            string transferType = ((ComboBoxItem)cmbTransferType.SelectedItem).Content.ToString();

            // Validate end date for temporary transfer
            if (transferType == "Temporary" && dpEndDate.SelectedDate == null)
            {
                lblNotification.Text = "End date is required for temporary transfers.";
                lblNotification.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }

            // Validate date range
            if (dpEndDate.SelectedDate != null && dpStartDate.SelectedDate >= dpEndDate.SelectedDate)
            {
                lblNotification.Text = "End date must be after start date.";
                lblNotification.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }

            try
            {
                // Create registration object for the transfer request
                var registration = new Registration
                {
                    UserId = _currentUser.UserId,
                    RegistrationType = transferType,
                    StartDate = DateOnly.FromDateTime(dpStartDate.SelectedDate.Value),
                    EndDate = dpEndDate.SelectedDate != null ? DateOnly.FromDateTime(dpEndDate.SelectedDate.Value) : null,
                    Status = "Pending",
                    Comments = $"Household Transfer Request\n" +
                               $"From: {_currentHousehold?.Address ?? "Current address"}\n" +
                               $"To: {txtNewAddress.Text}\n" +
                               $"Reason: {txtReason.Text}\n" +
                               $"Additional Info: {txtDocuments.Text}"
                };

                // Submit registration
                bool result = _registrationBLL.CreateRegistration(registration);

                if (result)
                {
                    // Show success message
                    lblNotification.Text = "Transfer request submitted successfully. Please wait for approval.";
                    lblNotification.Foreground = System.Windows.Media.Brushes.Green;

                    // Clear form
                    txtNewAddress.Text = "";
                    dpStartDate.SelectedDate = DateTime.Now;
                    cmbTransferType.SelectedIndex = 0;
                    dpEndDate.SelectedDate = null;
                    txtReason.Text = "";
                    txtDocuments.Text = "";
                }
                else
                {
                    lblNotification.Text = "Failed to submit transfer request. Please try again.";
                    lblNotification.Foreground = System.Windows.Media.Brushes.Red;
                }
            }
            catch (Exception ex)
            {
                lblNotification.Text = "Error submitting request: " + ex.Message;
                lblNotification.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // Clear form
            txtNewAddress.Text = "";
            dpStartDate.SelectedDate = DateTime.Now;
            cmbTransferType.SelectedIndex = 0;
            dpEndDate.SelectedDate = null;
            txtReason.Text = "";
            txtDocuments.Text = "";

            // Clear notification
            lblNotification.Text = "";
        }
    }
}