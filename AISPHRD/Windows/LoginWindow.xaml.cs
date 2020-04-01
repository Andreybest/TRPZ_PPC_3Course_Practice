using AISPHRD.Models;
using AISPHRD.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace AISPHRD.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private IUserRepository _userRepository;
        private IServiceProvider _serviceProvider;

        public LoginWindow(IUserRepository userRepository, IServiceProvider serviceProvider)
        {
            _userRepository = userRepository;
            _serviceProvider = serviceProvider;
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
                PasswordBox.Password = "12345678";
                _serviceProvider.GetService<TabsWindow>().Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Неправильний пароль");
            }
        }
    }
}
