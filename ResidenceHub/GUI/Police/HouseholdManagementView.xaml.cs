using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ResidenceHub.BLL;
using ResidenceHub.GUI.AreaLeader;
using ResidenceHub.Models;

namespace ResidenceHub.GUI.Police
{
    public partial class HouseholdManagementView : Page
    {
        private readonly User _currentUser;
        private readonly HouseholdBLL _householdBLL;
        private readonly HouseholdMemberBLL _householdMemberBLL;
        private readonly UserBLL _userBLL;
        private readonly LogBLL _logBLL;

        private List<Household> _households;
        private Household _selectedHousehold;

        public HouseholdManagementView(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _householdBLL = new HouseholdBLL();
            _householdMemberBLL = new HouseholdMemberBLL();
            _userBLL = new UserBLL();
            _logBLL = new LogBLL();

            // Update label to show it's loading all households
            lblArea.Text = "All Areas";

            // Load all households
            LoadHouseholds();
        }

        private void LoadHouseholds(string searchTerm = "")
        {
            try
            {
                // Get all households instead of filtering by area
                _households = _householdBLL.GetAllHouseholds();

                // Apply search filter if provided
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    searchTerm = searchTerm.ToLower();
                    _households = _households.Where(h =>
                        h.Address.ToLower().Contains(searchTerm) ||
                        (h.HeadOfHousehold != null && h.HeadOfHousehold.FullName.ToLower().Contains(searchTerm))
                    ).ToList();
                }

                // Create view model with additional properties
                var householdsVM = new List<object>();

                foreach (var household in _households)
                {
                    // Get members count
                    var members = _householdMemberBLL.GetMembersByHousehold(household.HouseholdId);
                    int membersCount = members != null ? members.Count : 0;

                    householdsVM.Add(new
                    {
                        household.HouseholdId,
                        HeadOfHouseholdName = household.HeadOfHousehold?.FullName ?? "Unknown",
                        household.Address,
                        CreatedDate = household.CreatedDate?.ToString() ?? "Unknown",
                        MembersCount = membersCount
                    });
                }

                dgHouseholds.ItemsSource = householdsVM;

                // Clear selection
                dgHouseholds.SelectedItem = null;
                _selectedHousehold = null;

                // Update status
                lblStatus.Text = $"Displaying {householdsVM.Count} households";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading households: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            LoadHouseholds(searchTerm);
        }

        private void dgHouseholds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgHouseholds.SelectedItem != null)
            {
                // Get the selected household ID
                var selectedItem = dgHouseholds.SelectedItem;
                int householdId = (int)selectedItem.GetType().GetProperty("HouseholdId").GetValue(selectedItem);

                // Get the full household object
                _selectedHousehold = _householdBLL.GetHouseholdById(householdId);
            }
            else
            {
                _selectedHousehold = null;
            }
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                int householdId = (int)button.Tag;
                var household = _householdBLL.GetHouseholdById(householdId);

                if (household != null)
                {
                    // Get members
                    var members = _householdMemberBLL.GetMembersByHousehold(householdId);

                    // Show household details dialog
                    var dialog = new HouseholdDetailsDialog(household, members);
                    dialog.ShowDialog();
                }
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                int householdId = (int)button.Tag;
                var household = _householdBLL.GetHouseholdById(householdId);

                if (household != null)
                {
                    // Get members
                    var members = _householdMemberBLL.GetMembersByHousehold(householdId);

                    // Show edit household dialog
                    var dialog = new EditHouseholdDialog(household, members, _currentUser);
                    if (dialog.ShowDialog() == true)
                    {
                        // Refresh households
                        LoadHouseholds(txtSearch.Text.Trim());

                        // Show success message
                        lblStatus.Text = "Household updated successfully.";
                        lblStatus.Foreground = System.Windows.Media.Brushes.Green;
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                int householdId = (int)button.Tag;

                // Confirm deletion
                MessageBoxResult result = MessageBox.Show(
                    "Are you sure you want to delete this household? This action will also remove all members from the household.",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Delete household
                        bool success = _householdBLL.DeleteHousehold(householdId, _currentUser.UserId);

                        if (success)
                        {
                            lblStatus.Text = "Household deleted successfully.";
                            lblStatus.Foreground = System.Windows.Media.Brushes.Green;

                            // Reload households
                            LoadHouseholds(txtSearch.Text.Trim());
                        }
                        else
                        {
                            lblStatus.Text = "Failed to delete household.";
                            lblStatus.Foreground = System.Windows.Media.Brushes.Red;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting household: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Show dialog to add new household
            var dialog = new AddHouseholdDialog(_currentUser);
            if (dialog.ShowDialog() == true)
            {
                // Reload households
                LoadHouseholds(txtSearch.Text.Trim());

                // Show success message
                lblStatus.Text = "New household added successfully.";
                lblStatus.Foreground = System.Windows.Media.Brushes.Green;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadHouseholds(txtSearch.Text.Trim());
        }
    }
}