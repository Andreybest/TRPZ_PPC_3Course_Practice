using AISPHRD.Models;
using AISPHRD.Windows;
using Microsoft.Win32;
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
using AISPHRD.Repositories;

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
                _newStudent.Speciality = SpecialityTextBox.Text;
                _newStudent.Faculty = FacultyTextBox.Text;
                _newStudent.Sex = SexTextBox.Text;
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
    }
}
