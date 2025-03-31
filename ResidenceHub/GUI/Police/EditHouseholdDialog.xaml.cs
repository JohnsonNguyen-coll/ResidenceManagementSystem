using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ResidenceHub.BLL;
using ResidenceHub.Models;

namespace ResidenceHub.GUI.Police
{
    public partial class EditHouseholdDialog : Window
    {
        private readonly User _currentUser;
        private readonly UserBLL _userBLL;
        private readonly HouseholdBLL _householdBLL;
        private readonly HouseholdMemberBLL _householdMemberBLL;
        private readonly LogBLL _logBLL;

        private Household _household;
        private List<HouseholdMember> _members;
        private List<User> _allResidents;
        private List<ResidentSelectionViewModel> _availableResidents;
        private List<MemberViewModel> _currentMembers;

        public EditHouseholdDialog(Household household, List<HouseholdMember> members, User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _household = household;
            _members = members ?? new List<HouseholdMember>();

            _userBLL = new UserBLL();
            _householdBLL = new HouseholdBLL();
            _householdMemberBLL = new HouseholdMemberBLL();
            _logBLL = new LogBLL();

            LoadResidents();

            LoadHouseholdInfo();
        }

        private void LoadHouseholdInfo()
        {
            txtHouseholdId.Text = _household.HouseholdId.ToString();
            txtAddress.Text = _household.Address;
            txtCreatedDate.Text = _household.CreatedDate?.ToString("yyyy-MM-dd") ?? "Unknown";

            if (_household.HeadOfHouseholdId != null)
            {
                foreach (var user in _allResidents)
                {
                    if (user.UserId == _household.HeadOfHouseholdId)
                    {
                        cmbHeadOfHousehold.SelectedItem = user;
                        break;
                    }
                }
            }

            LoadCurrentMembers();
        }

        private void LoadCurrentMembers()
        {
            _currentMembers = new List<MemberViewModel>();

            foreach (var member in _members)
            {
                if (member.User != null)
                {
                    _currentMembers.Add(new MemberViewModel
                    {
                        MemberId = member.MemberId,
                        UserId = member.UserId ?? 0,
                        FullName = member.User.FullName,
                        Relationship = member.Relationship
                    });
                }
            }

            dgCurrentMembers.ItemsSource = _currentMembers;
        }

        private void LoadResidents()
        {
            try
            {
                string area = ExtractAreaFromAddress(_currentUser.Address);
                _allResidents = _userBLL.GetAllUsers();

                _allResidents = _allResidents.Where(r => r.Role == "Citizen").ToList();

                cmbHeadOfHousehold.ItemsSource = _allResidents;

                UpdateAvailableResidents();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading residents: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateAvailableResidents()
        {
            var currentMemberIds = _members.Select(m => m.UserId).ToList();

            var availableUsers = _allResidents.Where(r => !currentMemberIds.Contains(r.UserId)).ToList();

            _availableResidents = availableUsers.Select(r => new ResidentSelectionViewModel
            {
                UserId = r.UserId,
                FullName = r.FullName,
                Email = r.Email,
                IsSelected = false
            }).ToList();

            lstAvailableResidents.ItemsSource = _availableResidents;
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            lblError.Text = "";

            if (cmbHeadOfHousehold.SelectedItem == null || string.IsNullOrEmpty(txtAddress.Text))
            {
                lblError.Text = "Please select a head of household and enter an address.";
                return;
            }

            try
            {
                User headOfHousehold = (User)cmbHeadOfHousehold.SelectedItem;

                _household.HeadOfHouseholdId = headOfHousehold.UserId;
                _household.Address = txtAddress.Text.Trim();

                bool result = _householdBLL.UpdateHousehold(_household, _currentUser.UserId);

                if (result)
                {
                    _logBLL.CreateLog(_currentUser.UserId, $"Updated household {_household.HouseholdId}");

                    DialogResult = true;

                    Close();
                }
                else
                {
                    lblError.Text = "Failed to update household.";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = $"Error: {ex.Message}";
            }
        }

        private void btnRemoveMember_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                int memberId = (int)button.Tag;

                try
                {
                    var member = _members.FirstOrDefault(m => m.MemberId == memberId);
                    if (member != null && member.UserId == _household.HeadOfHouseholdId)
                    {
                        MessageBox.Show("Cannot remove the head of household. Please change the head of household first.",
                                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    MessageBoxResult result = MessageBox.Show(
                        "Are you sure you want to remove this member from the household?",
                        "Confirm Remove",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        // Remove member
                        bool success = _householdMemberBLL.DeleteMember(memberId, _currentUser.UserId);

                        if (success)
                        {
                            // Remove from members list
                            _members.RemoveAll(m => m.MemberId == memberId);

                            // Reload members display
                            LoadCurrentMembers();

                            // Update available residents
                            UpdateAvailableResidents();
                        }
                        else
                        {
                            MessageBox.Show("Failed to remove member.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error removing member: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnAddMembers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get selected residents
                var selectedResidents = _availableResidents.Where(r => r.IsSelected).ToList();

                if (selectedResidents.Count == 0)
                {
                    MessageBox.Show("Please select at least one resident to add.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                int addedCount = 0;

                // Add each selected resident to the household
                foreach (var resident in selectedResidents)
                {
                    var householdMember = new HouseholdMember
                    {
                        HouseholdId = _household.HouseholdId,
                        UserId = resident.UserId,
                        Relationship = "Member" // Default relationship
                    };

                    bool success = _householdMemberBLL.CreateMember(householdMember, _currentUser.UserId);
                    if (success)
                    {
                        addedCount++;
                    }
                }

                if (addedCount > 0)
                {
                    // Reload members from database
                    _members = _householdMemberBLL.GetMembersByHousehold(_household.HouseholdId);

                    // Reload members display
                    LoadCurrentMembers();

                    // Update available residents
                    UpdateAvailableResidents();

                    MessageBox.Show($"Successfully added {addedCount} member(s) to the household.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Failed to add members to the household.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding members: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

    // View model for household members
    public class MemberViewModel
    {
        public int MemberId { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Relationship { get; set; }
    }
}