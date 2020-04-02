using AISPHRD.Repositories;
using System;
using System.Collections.Generic;
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
using Microsoft.Extensions.DependencyInjection;
using AISPHRD.Windows;
using AISPHRD.Models;
using AISPHRD.Tabs.Add;
using AISPHRD.Tabs.Edit;

namespace AISPHRD.Tabs
{
    /// <summary>
    /// Логика взаимодействия для StudentsUserControl.xaml
    /// </summary>
    public partial class StudentsUserControl : TabUserControl
    {
        private IStudentRepository _studentRepository;

        public StudentsUserControl()
        {
            _studentRepository = App.ServiceProvider.GetService<IStudentRepository>();
            InitializeComponent();
            UpdateData();
        }

        public void UpdateTable(DataGrid dataGrid, IStudentRepository studentRepository)
        {
            if (SearchTextBox.Text.Length > 0)
            {
                dataGrid.ItemsSource = studentRepository.GetAllBySearchString(SearchTextBox.Text);
                return;
            }
            dataGrid.ItemsSource = studentRepository.GetAll();
        }

        public override void UpdateData()
        {
            UpdateTable(StudentsDataGrid, _studentRepository);
            StudentsAmountLabel.Content = "Усього в системі зареєстрованно " + _studentRepository.GetAll().Count;
        }

        private void EditStudentButton_Click(object sender, RoutedEventArgs e)
        {
            App.ServiceProvider.GetService<TabsWindow>().CreateNewTab(new EditStudentUserControl((StudentsDataGrid.SelectedItem as Student)), "СТУДЕНТ / РЕДАГУВАННЯ");
        }

        private void DeleteStudentButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show($"Ви точно хочете видалити {(StudentsDataGrid.SelectedItem as Student).FullName}?", "Видалити студента", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                try
                {
                    _studentRepository.Delete((StudentsDataGrid.SelectedItem as Student).StudentId);
                    UpdateData();
                }
                catch (Exception)
                {
                    MessageBox.Show("Сталася помилка при видаленні студента, можливо у студент прив'язаний до військомату, видаліть його прописку спочатку");
                    UpdateData();
                }
            }
        }

        private void AddStudentButton_Click(object sender, RoutedEventArgs e)
        {
            App.ServiceProvider.GetService<TabsWindow>().CreateNewTab(new AddStudentUserControl(), "СТУДЕНТ / ДОДАВАННЯ");
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTable(StudentsDataGrid, _studentRepository);
        }
    }
}
