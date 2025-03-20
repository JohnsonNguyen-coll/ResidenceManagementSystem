using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ResidenceHub.DataAccess;
using ResidenceHub.Models;

namespace ResidenceHub
{
    public partial class MainWindow : Window
    {
        private readonly AsmPrnContext _context;
        private readonly UserDAO _userDAO;
        private readonly HouseholdDAO _householdDAO;
        private readonly RegistrationDAO _registrationDAO;
        private readonly NotificationDAO _notificationDAO;
        private readonly ReportDAO _reportDAO;
        private int _currentPoliceId = 3; 
        public MainWindow()
        {
            InitializeComponent();
            _context = new AsmPrnContext();
            _userDAO = new UserDAO(_context);
            _householdDAO = new HouseholdDAO(_context);
            _registrationDAO = new RegistrationDAO(_context);
            _notificationDAO = new NotificationDAO(_context);
            _reportDAO = new ReportDAO(_context);
            LoadData();
        }

        private void LoadData()
        {
            ResidentsDataGrid.ItemsSource = _userDAO.GetAllUsers();
            HouseholdsDataGrid.ItemsSource = _householdDAO.GetAllHouseholds();
            RegistrationsDataGrid.ItemsSource = _registrationDAO.GetPendingRegistrations();
            NotificationUserComboBox.ItemsSource = _userDAO.GetAllUsers();
        }

        // Resident Management
        private void AddResident_Click(object sender, RoutedEventArgs e)
        {
            var user = new User
            {
                FullName = FullNameTextBox.Text,
                Email = EmailTextBox.Text,
                Password = PasswordTextBox.Text, // Nên hash trong thực tế
                Role = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                Address = AddressTextBox.Text
            };
            _userDAO.AddUser(user);
            LoadData();
        }

        private void UpdateResident_Click(object sender, RoutedEventArgs e)
        {
            if (ResidentsDataGrid.SelectedItem is User selectedUser)
            {
                selectedUser.FullName = FullNameTextBox.Text;
                selectedUser.Email = EmailTextBox.Text;
                selectedUser.Password = PasswordTextBox.Text;
                selectedUser.Role = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                selectedUser.Address = AddressTextBox.Text;
                _userDAO.UpdateUser(selectedUser);
                LoadData();
            }
        }

        private void DeleteResident_Click(object sender, RoutedEventArgs e)
        {
            if (ResidentsDataGrid.SelectedItem is User selectedUser)
            {
                _userDAO.DeleteUser(selectedUser.UserId);
                LoadData();
            }
        }

        private void AddHousehold_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(HeadIdTextBox.Text, out int headId))
            {
                var household = new Household
                {
                    HeadOfHouseholdId = headId,
                    Address = HouseholdAddressTextBox.Text,
                    CreatedDate = DateOnly.FromDateTime(DateTime.Now)
                };
                _householdDAO.AddHousehold(household);
                LoadData();
            }
        }

        private void UpdateHousehold_Click(object sender, RoutedEventArgs e)
        {
            if (HouseholdsDataGrid.SelectedItem is Household selectedHousehold && int.TryParse(HeadIdTextBox.Text, out int headId))
            {
                selectedHousehold.HeadOfHouseholdId = headId;
                selectedHousehold.Address = HouseholdAddressTextBox.Text;
                _householdDAO.UpdateHousehold(selectedHousehold);
                LoadData();
            }
        }

        private void DeleteHousehold_Click(object sender, RoutedEventArgs e)
        {
            if (HouseholdsDataGrid.SelectedItem is Household selectedHousehold)
            {
                _householdDAO.DeleteHousehold(selectedHousehold.HouseholdId);
                LoadData();
            }
        }

        private void ResidentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ResidentsDataGrid.SelectedItem is User selectedUser)
            {
                FullNameTextBox.Text = selectedUser.FullName;
                EmailTextBox.Text = selectedUser.Email;
                PasswordTextBox.Text = selectedUser.Password;
                RoleComboBox.SelectedItem = RoleComboBox.Items.Cast<ComboBoxItem>().FirstOrDefault(i => i.Content.ToString() == selectedUser.Role);
                AddressTextBox.Text = selectedUser.Address;
            }
        }

        private void HouseholdsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HouseholdsDataGrid.SelectedItem is Household selectedHousehold)
            {
                HeadIdTextBox.Text = selectedHousehold.HeadOfHouseholdId.ToString();
                HouseholdAddressTextBox.Text = selectedHousehold.Address;
            }
        }

        // Request Approval
        private void ApproveRequest_Click(object sender, RoutedEventArgs e)
        {
            if (RegistrationsDataGrid.SelectedItem is Registration selectedReg)
            {
                _registrationDAO.ApproveRegistration(selectedReg, _currentPoliceId, CommentsTextBox.Text);
                _notificationDAO.SendNotification(selectedReg.UserId.Value, $"Your {selectedReg.RegistrationType} request has been approved.");
                LoadData();
            }
        }

        private void RejectRequest_Click(object sender, RoutedEventArgs e)
        {
            if (RegistrationsDataGrid.SelectedItem is Registration selectedReg)
            {
                _registrationDAO.RejectRegistration(selectedReg, _currentPoliceId, CommentsTextBox.Text);
                _notificationDAO.SendNotification(selectedReg.UserId.Value, $"Your {selectedReg.RegistrationType} request has been rejected.");
                LoadData();
            }
        }

        private void RegistrationsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RegistrationsDataGrid.SelectedItem is Registration selectedReg)
            {
                CommentsTextBox.Text = selectedReg.Comments ?? "";
            }
        }

        private void ApproveTransfer_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Household transfer approved (logic placeholder).");
            // Logic chuyển hộ có thể được thêm vào HouseholdDAO nếu cần
        }

        private void ApproveSeparation_Click(object sender, RoutedEventArgs e)
        {
            if (HouseholdsDataGrid.SelectedItem is Household selectedHousehold)
            {
                _householdDAO.CreateSeparation(selectedHousehold, selectedHousehold.HeadOfHouseholdId.Value, "New Address");
                LoadData();
                MessageBox.Show("Household separation approved.");
            }
        }

        // Reports
        private void GenerateResidentReport_Click(object sender, RoutedEventArgs e)
        {
            var stats = _registrationDAO.GetResidentStats();
            ReportTextBox.Text = "Resident Report:\n" + string.Join("\n", stats.Select(kv => $"{kv.Key}: {kv.Value} residents"));
        }

        private void GenerateHouseholdReport_Click(object sender, RoutedEventArgs e)
        {
            var (householdCount, memberCount) = _reportDAO.GetHouseholdStats();
            ReportTextBox.Text = $"Household Report:\nTotal Households: {householdCount}\nTotal Members: {memberCount}";
        }

        private void ExportToPdf_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Export to PDF functionality not implemented yet.");
        }

        // Notifications
        private void SendNotification_Click(object sender, RoutedEventArgs e)
        {
            if (NotificationUserComboBox.SelectedItem is User selectedUser)
            {
                _notificationDAO.SendNotification(selectedUser.UserId, NotificationMessageTextBox.Text);
                MessageBox.Show("Notification sent!");
            }
        }
    }
}