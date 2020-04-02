using AISPHRD.Models;
using AISPHRD.Repositories;
using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using AISPHRD.Windows;

namespace AISPHRD.Tabs.Add
{
    /// <summary>
    /// Логика взаимодействия для AddConscriptUserControl.xaml
    /// </summary>
    public partial class AddConscriptUserControl : TabUserControl
    {
        private Conscript _newConscript;

        public AddConscriptUserControl()
        {
            _newConscript = new Conscript();
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _newConscript.StudentId = (StudentComboBox.SelectedItem as Student).StudentId;
                _newConscript.Commissariat = CommissariatTextBox.Text;
                App.ServiceProvider.GetService<IConscriptRepository>().Insert(_newConscript);
                MessageBox.Show("Новий військовозобов'язаний успішно доданий!");
                App.ServiceProvider.GetService<TabsWindow>().CloseTab("ВІЙСЬКОВОЗОБОВ'ЯЗАНИЙ / ДОДАВАННЯ");
            }
            catch (Exception)
            {
                MessageBox.Show("Виникла помилка при додаванні військовозобов'язаного.");
                App.ServiceProvider.GetService<TabsWindow>().CloseTab("ВІЙСЬКОВОЗОБОВ'ЯЗАНИЙ / ДОДАВАННЯ");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            App.ServiceProvider.GetService<TabsWindow>().CloseTab("ВІЙСЬКОВОЗОБОВ'ЯЗАНИЙ / ДОДАВАННЯ");
        }

        private void TabUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            StudentComboBox.ItemsSource = App.ServiceProvider.GetService<IStudentRepository>().GetAll();
            if (StudentComboBox.Items.Count > 0)
            {
                StudentComboBox.SelectedIndex = 0;
            }
        }
    }
}
