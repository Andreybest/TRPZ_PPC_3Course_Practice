using AISPHRD.Models;
using AISPHRD.Windows;
using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using AISPHRD.Repositories;
using System.Text.RegularExpressions;

namespace AISPHRD.Tabs.Add
{
    /// <summary>
    /// Логика взаимодействия для AddStudentUserControl.xaml
    /// </summary>
    public partial class AddStudentUserControl : TabUserControl
    {
        private Student _newStudent;

        public AddStudentUserControl()
        {
            _newStudent = new Student();
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _newStudent.FullName = FullNameTextBox.Text;
                _newStudent.Speciality = SpecialityComboBox.Text;
                _newStudent.Faculty = FacultyComboBox.Text;
                _newStudent.Sex = SexComboBox.Text;
                _newStudent.Year = int.Parse(YearTextBox.Text);
                _newStudent.Address = AddressTypeTextBox.Text;
                _newStudent.BirthDate = BirthDatePicker.DisplayDate;
                App.ServiceProvider.GetService<IStudentRepository>().Insert(_newStudent);
                MessageBox.Show("Новий студент успішно доданий!");
                App.ServiceProvider.GetService<TabsWindow>().CloseTab("СТУДЕНТ / ДОДАВАННЯ");
            }
            catch (Exception)
            {
                MessageBox.Show("Виникла помилка при додаванні студента.");
                App.ServiceProvider.GetService<TabsWindow>().CloseTab("СТУДЕНТ / ДОДАВАННЯ");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            App.ServiceProvider.GetService<TabsWindow>().CloseTab("СТУДЕНТ / ДОДАВАННЯ");
        }

        private void TabUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SpecialityComboBox.ItemsSource = App.ServiceProvider.GetService<IStudentRepository>().GetUniqueSpecialities();
            FacultyComboBox.ItemsSource = App.ServiceProvider.GetService<IStudentRepository>().GetUniqueFaculties();
            SexComboBox.ItemsSource = App.ServiceProvider.GetService<IStudentRepository>().GetUniqueSexes();
        }

        private void YearTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]");
        }
    }
}
