using AISPHRD.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using AISPHRD.Repositories;
using Microsoft.Extensions.DependencyInjection;
using AISPHRD.Windows;

namespace AISPHRD.Tabs.Edit
{
    /// <summary>
    /// Логика взаимодействия для EditUserPasswordUserControl.xaml
    /// </summary>
    public partial class EditUserPasswordUserControl : TabUserControl
    {

        private User _editedUser;

        public EditUserPasswordUserControl()
        {
            _editedUser = App.ServiceProvider.GetService<IUserRepository>().GetUser("Admin");
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password != RepeatPasswordBox.Password)
            {
                MessageBox.Show("Введені паролі не співпадають");
                return;
            }

            try
            {
                _editedUser.Password = PasswordBox.Password;
                App.ServiceProvider.GetService<IUserRepository>().Update(_editedUser);
                MessageBox.Show("Ваш пароль успішно оновлено!");
                App.ServiceProvider.GetService<TabsWindow>().CloseTab("ЗМІНА ПАРОЛЮ");
            }
            catch (Exception)
            {
                MessageBox.Show("Виникла помилка при зміні паролю.");
                App.ServiceProvider.GetService<TabsWindow>().CloseTab("ЗМІНА ПАРОЛЮ");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            App.ServiceProvider.GetService<TabsWindow>().CloseTab("ЗМІНА ПАРОЛЮ");
        }
    }
}
