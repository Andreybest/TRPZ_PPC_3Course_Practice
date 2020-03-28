using AISPHRD.Repositories;
using System.Windows;

namespace AISPHRD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IUserRepository _userRepository;

        public MainWindow(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_userRepository.GetUser("Admin").Password);
        }
    }
}
