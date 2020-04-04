using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Navigation;

namespace AISPHRD.Tabs
{
    /// <summary>
    /// Логика взаимодействия для AboutUserControl.xaml
    /// </summary>
    public partial class AboutUserControl : TabUserControl
    {
        public AboutUserControl()
        {
            InitializeComponent();
        }

        private void OnNavigate(object sender, RequestNavigateEventArgs e)
        {
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {e.Uri.AbsoluteUri}"));
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", e.Uri.AbsoluteUri);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", e.Uri.AbsoluteUri);
                }
                else
                {
                    MessageBox.Show("Не вдалося відкрити посилання");
                }
            }
            catch (System.Exception)
            {
                MessageBox.Show("Не вдалося відкрити посилання");
            }
            e.Handled = true;
        }
    }
}
