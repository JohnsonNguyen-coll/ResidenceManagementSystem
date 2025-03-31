using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Win32;
using ResidenceHub.BLL;
using ResidenceHub.Models;

namespace ResidenceHub.GUI.Police
{
    public partial class ReportsView : Page
    {
        private readonly User _currentUser;
        private readonly UserBLL _userBLL;
        private readonly HouseholdBLL _householdBLL;
        private readonly HouseholdMemberBLL _householdMemberBLL;
        private readonly RegistrationBLL _registrationBLL;
        private readonly LogBLL _logBLL;

        private string _currentReportType;
        private bool _isInitialized = false;

        public ReportsView(User user)
        {
            InitializeComponent();

            // This event handler will be called after the UI is initialized
            this.Loaded += (s, e) =>
            {
                _isInitialized = true;
                // Now it's safe to load the report
                LoadReport();
            };

            _currentUser = user;
            _userBLL = new UserBLL();
            _householdBLL = new HouseholdBLL();
            _householdMemberBLL = new HouseholdMemberBLL();
            _registrationBLL = new RegistrationBLL();
            _logBLL = new LogBLL();

            // Set default report type
            _currentReportType = "Resident Statistics";

            // We'll load the report in the Loaded event, not here
        }

        private void LoadReport()
        {
            // Make sure the UI is initialized
            if (!_isInitialized || dgReportData == null)
            {
                return;
            }

            try
            {
                // Clear grid - safely
                dgReportData.Columns.Clear();
                dgReportData.ItemsSource = null;

                // Load report based on selected type
                switch (_currentReportType)
                {
                    case "Resident Statistics":
                        LoadResidentStatistics();
                        break;
                    case "Household Statistics":
                        LoadHouseholdStatistics();
                        break;
                    case "Registration Statistics":
                        LoadRegistrationStatistics();
                        break;
                }

                // Log the action
                _logBLL.CreateLog(_currentUser.UserId, $"Viewed {_currentReportType} report");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading report: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadResidentStatistics()
        {
            try
            {
                var residents = _userBLL.GetAllUsers();

                var registrations = _registrationBLL.GetAllRegistrations();

                int totalResidents = residents.Count;
                int permanentResidents = registrations.Count(r => r.RegistrationType == "Permanent" && r.Status == "Approved");
                int temporaryResidents = registrations.Count(r => (r.RegistrationType == "Temporary" || r.RegistrationType == "TemporaryStay") && r.Status == "Approved");

                lblSummary1Title.Text = "Total Residents";
                lblSummary1Value.Text = totalResidents.ToString();

                lblSummary2Title.Text = "Permanent Residents";
                lblSummary2Value.Text = permanentResidents.ToString();

                lblSummary3Title.Text = "Temporary Residents";
                lblSummary3Value.Text = temporaryResidents.ToString();

                var reportData = residents.GroupBy(r => r.Role).Select(g => new
                {
                    Role = g.Key,
                    Count = g.Count(),
                    Percentage = totalResidents > 0 ? Math.Round((double)g.Count() / totalResidents * 100, 2) : 0
                }).ToList();

                dgReportData.Columns.Add(new DataGridTextColumn { Header = "Role", Binding = new Binding("Role") });
                dgReportData.Columns.Add(new DataGridTextColumn { Header = "Count", Binding = new Binding("Count") });
                dgReportData.Columns.Add(new DataGridTextColumn { Header = "Percentage (%)", Binding = new Binding("Percentage") });

                // Set data
                dgReportData.ItemsSource = reportData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading resident statistics: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadHouseholdStatistics()
        {
            try
            {
                var households = _householdBLL.GetAllHouseholds();

                int totalHouseholds = households.Count;
                int totalMembers = 0;

                var reportData = new List<object>();

                foreach (var household in households)
                {
                    var members = _householdMemberBLL.GetMembersByHousehold(household.HouseholdId);
                    int memberCount = members?.Count ?? 0;
                    totalMembers += memberCount;

                    reportData.Add(new
                    {
                        HouseholdId = household.HouseholdId,
                        Address = household.Address,
                        HeadOfHousehold = household.HeadOfHousehold?.FullName ?? "Unknown",
                        MemberCount = memberCount,
                        CreatedDate = household.CreatedDate?.ToString() ?? "Unknown"
                    });
                }

                // Update summary
                lblSummary1Title.Text = "Total Households";
                lblSummary1Value.Text = totalHouseholds.ToString();

                lblSummary2Title.Text = "Total Members";
                lblSummary2Value.Text = totalMembers.ToString();

                lblSummary3Title.Text = "Avg. Members/Household";
                lblSummary3Value.Text = totalHouseholds > 0 ? Math.Round((double)totalMembers / totalHouseholds, 2).ToString() : "0";

                // Configure data grid
                dgReportData.Columns.Add(new DataGridTextColumn { Header = "Household ID", Binding = new Binding("HouseholdId") });
                dgReportData.Columns.Add(new DataGridTextColumn { Header = "Address", Binding = new Binding("Address"), Width = new DataGridLength(150) });
                dgReportData.Columns.Add(new DataGridTextColumn { Header = "Head of Household", Binding = new Binding("HeadOfHousehold") });
                dgReportData.Columns.Add(new DataGridTextColumn { Header = "Member Count", Binding = new Binding("MemberCount") });
                dgReportData.Columns.Add(new DataGridTextColumn { Header = "Created Date", Binding = new Binding("CreatedDate") });

                // Set data
                dgReportData.ItemsSource = reportData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading household statistics: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadRegistrationStatistics()
        {
            try
            {
                // Get all registrations instead of filtering by area
                var registrations = _registrationBLL.GetAllRegistrations();

                // Calculate statistics
                int totalRegistrations = registrations.Count;
                int pendingRegistrations = registrations.Count(r => r.Status == "Pending");
                int approvedRegistrations = registrations.Count(r => r.Status == "Approved");
                int rejectedRegistrations = registrations.Count(r => r.Status == "Rejected");

                // Update summary
                lblSummary1Title.Text = "Total Registrations";
                lblSummary1Value.Text = totalRegistrations.ToString();

                lblSummary2Title.Text = "Approved";
                lblSummary2Value.Text = approvedRegistrations.ToString();

                lblSummary3Title.Text = "Pending";
                lblSummary3Value.Text = pendingRegistrations.ToString();

                // Create detailed report data by type and status
                var reportData = registrations
                    .GroupBy(r => new { r.RegistrationType, r.Status })
                    .Select(g => new
                    {
                        Type = g.Key.RegistrationType,
                        Status = g.Key.Status,
                        Count = g.Count(),
                        Percentage = totalRegistrations > 0 ? Math.Round((double)g.Count() / totalRegistrations * 100, 2) : 0
                    })
                    .OrderBy(r => r.Type)
                    .ThenBy(r => r.Status)
                    .ToList();

                // Configure data grid
                dgReportData.Columns.Add(new DataGridTextColumn { Header = "Registration Type", Binding = new Binding("Type") });
                dgReportData.Columns.Add(new DataGridTextColumn { Header = "Status", Binding = new Binding("Status") });
                dgReportData.Columns.Add(new DataGridTextColumn { Header = "Count", Binding = new Binding("Count") });
                dgReportData.Columns.Add(new DataGridTextColumn { Header = "Percentage (%)", Binding = new Binding("Percentage") });

                // Set data
                dgReportData.ItemsSource = reportData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading registration statistics: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmbReportType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbReportType?.SelectedItem != null)
            {
                _currentReportType = ((ComboBoxItem)cmbReportType.SelectedItem).Content.ToString();
                LoadReport();
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadReport();
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Make sure the data grid is initialized and has data
                if (dgReportData == null || dgReportData.Items.Count == 0)
                {
                    MessageBox.Show("No data available to export.", "Export Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Set up save file dialog
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV Files (*.csv)|*.csv|All files (*.*)|*.*",
                    DefaultExt = "csv",
                    FileName = $"{_currentReportType.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd}"
                };

                // Show save file dialog
                if (saveFileDialog.ShowDialog() == true)
                {
                    // Export to CSV
                    ExportToCsv(saveFileDialog.FileName);

                    // Show success message
                    MessageBox.Show($"Report exported successfully to {saveFileDialog.FileName}", "Export Successful", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Log the action
                    _logBLL.CreateLog(_currentUser.UserId, $"Exported {_currentReportType} report to CSV");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting report: {ex.Message}", "Export Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExportToCsv(string filePath)
        {
            // This is a simplified CSV export
            using (var writer = new System.IO.StreamWriter(filePath))
            {
                // Write header
                var headerRow = new List<string>();
                foreach (DataGridColumn column in dgReportData.Columns)
                {
                    headerRow.Add(column.Header.ToString());
                }
                writer.WriteLine(string.Join(",", headerRow));

                // Write data
                foreach (var item in dgReportData.Items)
                {
                    var dataRow = new List<string>();
                    foreach (DataGridColumn column in dgReportData.Columns)
                    {
                        var binding = (column as DataGridTextColumn)?.Binding as Binding;
                        if (binding != null)
                        {
                            var propertyValue = item.GetType().GetProperty(binding.Path.Path)?.GetValue(item)?.ToString() ?? "";
                            dataRow.Add($"\"{propertyValue}\"");
                        }
                    }
                    writer.WriteLine(string.Join(",", dataRow));
                }
            }
        }
    }
}