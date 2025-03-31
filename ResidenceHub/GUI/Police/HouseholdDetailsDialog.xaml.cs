using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ResidenceHub.Models;

namespace ResidenceHub.GUI.Police
{
    public partial class HouseholdDetailsDialog : Window
    {
        public HouseholdDetailsDialog(Household household, List<HouseholdMember> members)
        {
            InitializeComponent();

            // Display household information
            DisplayHouseholdInfo(household);

            // Display members
            DisplayMembers(members);
        }

        private void DisplayHouseholdInfo(Household household)
        {
            if (household != null)
            {
                lblHouseholdId.Text = household.HouseholdId.ToString();
                lblCreatedDate.Text = household.CreatedDate?.ToString("yyyy-MM-dd") ?? "Unknown";
                lblHeadOfHousehold.Text = household.HeadOfHousehold?.FullName ?? "Unknown";
                lblAddress.Text = household.Address;
            }
        }

        private void DisplayMembers(List<HouseholdMember> members)
        {
            if (members != null)
            {
                var membersVM = members.Select(m => new
                {
                    FullName = m.User?.FullName ?? "Unknown",
                    m.Relationship,
                    Email = m.User?.Email ?? "Unknown",
                    Address = m.User?.Address ?? "Unknown"
                }).ToList();

                dgMembers.ItemsSource = membersVM;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}