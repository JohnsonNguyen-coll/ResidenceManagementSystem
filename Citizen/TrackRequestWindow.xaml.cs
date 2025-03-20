using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Assignment_PRN.Models;

namespace Assignment_PRN
{
    /// <summary>
    /// Interaction logic for TrackRequestWindow.xaml
    /// </summary>
    public partial class TrackRequestWindow : Window
    {
        private int _userId;

        public TrackRequestWindow(int userId)
        {
            InitializeComponent();
            _userId = userId;
            LoadRegistrations();
        }

        private void LoadRegistrations()
        {
            using (var context = new TestPrnassContext())
            {
                var registrations = context.Registrations
                    .Where(r => r.UserId == _userId)
                    .Select(r => new
                    {
                        r.RegistrationType,
                        r.StartDate,
                        r.EndDate,
                        r.Status,
                        r.Comments
                    })
                    .ToList();

                dgRegistrations.ItemsSource = registrations;
            }
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
