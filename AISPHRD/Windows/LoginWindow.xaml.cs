using AISPHRD.Models;
using AISPHRD.Repositories;
using System.Windows;

namespace AISPHRD.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private IUserRepository _userRepository;

        public LoginWindow(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            InitializeComponent();
        }

        private void PowerButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (LoginTextBox.Text.Length < 1 || PasswordBox.Password.Length < 1)
            {
                MessageBox.Show("Заповніть усі поля");
                return;
            }

            User user = _userRepository.GetUser(LoginTextBox.Text);
            if (user == null)
            {
                MessageBox.Show("Користувач з таким ім'ям, не знайдений");
                return;
            }

            if (user.Password == PasswordBox.Password)
            {
                // TO-DO
            }
            else
            {
                MessageBox.Show("Неправильний пароль");
            }
        }
    }
}
