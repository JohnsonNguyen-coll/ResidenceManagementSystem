using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ResidenceHub.BLL;
using ResidenceHub.Models;

namespace ResidenceHub.GUI.Police
{
    public partial class AddHouseholdDialog : Window
    {
        private readonly User _currentUser;
        private readonly UserBLL _userBLL;
        private readonly HouseholdBLL _householdBLL;
        private readonly HouseholdMemberBLL _householdMemberBLL;
        private readonly LogBLL _logBLL;

        private List<User> _allResidents;
        private List<ResidentSelectionViewModel> _availableResidents;

        public AddHouseholdDialog(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _userBLL = new UserBLL();
            _householdBLL = new HouseholdBLL();
            _householdMemberBLL = new HouseholdMemberBLL();
            _logBLL = new LogBLL();

            // Fill address with the current managed area
            string area = ExtractAreaFromAddress(_currentUser.Address);
            txtAddress.Text = area;

            // Load residents
            LoadResidents();
        }

        private string ExtractAreaFromAddress(string address)
        {
            string[] parts = address.Split(',');
            if (parts.Length > 1)
            {
                return parts[parts.Length - 2].Trim();
            }
            return address;
        }

        private void LoadResidents()
        {
            try
            {
                // Get all residents in the area
                string area = ExtractAreaFromAddress(_currentUser.Address);
                _allResidents = _userBLL.GetAllUsers();

                // Filter only citizens
                _allResidents = _allResidents.Where(r => r.Role == "Citizen").ToList();

                // Create selection view model
                _availableResidents = _allResidents.Select(r => new ResidentSelectionViewModel
                {
                    UserId = r.UserId,
                    FullName = r.FullName,
                    Email = r.Email,
                    IsSelected = false
                }).ToList();

                // Set data sources
                cmbHeadOfHousehold.ItemsSource = _allResidents;
                lstAvailableResidents.ItemsSource = _availableResidents;

                // Select first item if available
                if (_allResidents.Count > 0)
                {
                    cmbHeadOfHousehold.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading residents: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            lblError.Text = "";

            // Validate input
            if (cmbHeadOfHousehold.SelectedItem == null || string.IsNullOrEmpty(txtAddress.Text))
            {
                lblError.Text = "Please select a head of household and enter an address.";
                return;
            }

            try
            {
                // Get selected head of household
                User headOfHousehold = (User)cmbHeadOfHousehold.SelectedItem;

                // Create household object
                var household = new Household
                {
                    HeadOfHouseholdId = headOfHousehold.UserId,
                    Address = txtAddress.Text.Trim(),
                    CreatedDate = DateOnly.FromDateTime(DateTime.Now)
                };

                // Create the household
                bool result = _householdBLL.CreateHousehold(household, _currentUser.UserId);

                if (result)
                {
                    // Add selected members to the household
                    var selectedMembers = _availableResidents.Where(r => r.IsSelected).ToList();
                    foreach (var member in selectedMembers)
                    {
                        // Skip if the member is the head of household
                        if (member.UserId != headOfHousehold.UserId)
                        {
                            var householdMember = new HouseholdMember
                            {
                                HouseholdId = household.HouseholdId,
                                UserId = member.UserId,
                                Relationship = "Member" // Default relationship
                            };

                            _householdMemberBLL.CreateMember(householdMember, _currentUser.UserId);
                        }
                    }

                    // Log the action
                    _logBLL.CreateLog(_currentUser.UserId, $"Created new household with head {headOfHousehold.FullName}");

                    // Set dialog result
                    DialogResult = true;

                    // Close dialog
                    Close();
                }
                else
                {
                    lblError.Text = "Failed to create household.";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = $"Error: {ex.Message}";
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // Set dialog result
            DialogResult = false;

            // Close dialog
            Close();
        }
    }

    // View model for resident selection
    public class ResidentSelectionViewModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsSelected { get; set; }
    }
}