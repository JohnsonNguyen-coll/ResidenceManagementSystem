using System.Configuration;
using System.Data;
using System.Windows;

namespace ResidenceHub
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Tạo và hiển thị cửa sổ đăng nhập làm cửa sổ chính
            ResidenceHub.GUI.Common.LoginWindow loginWindow = new ResidenceHub.GUI.Common.LoginWindow();
            loginWindow.Show();
        }

    }
}
    // App.xaml.cs


