using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ResidenceHub.BLL;
using ResidenceHub.Models;

namespace ResidenceHub.GUI.AreaLeader
{
    public partial class HouseholdListView : Page
    {
        private readonly User _currentUser;
        private readonly HouseholdBLL _householdBLL;
        private readonly HouseholdMemberBLL _householdMemberBLL;
        private readonly LogBLL _logBLL;

        private List<Household> _households;
        private Household _selectedHousehold;

        public HouseholdListView(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _householdBLL = new HouseholdBLL();
            _householdMemberBLL = new HouseholdMemberBLL();
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
                        CreatedDate = household.CreatedDate?.ToString("yyyy-MM-dd") ?? "Unknown",
                        MembersCount = membersCount
                    });
                }

                dgHouseholds.ItemsSource = householdsVM;

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

        private void btnViewDetails_Click(object sender, RoutedEventArgs e)
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

                    // Create a dialog to display household details
                    var dialog = new HouseholdDetailsDialog(household, members);
                    dialog.ShowDialog();
                }
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadHouseholds(txtSearch.Text.Trim());
        }
    }
}