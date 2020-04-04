using AISPHRD.Models;
using AISPHRD.Windows;
using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using AISPHRD.Repositories;
using System.Text.RegularExpressions;

namespace AISPHRD.Tabs.Edit
{
    /// <summary>
    /// Логика взаимодействия для EditStudentUserControl.xaml
    /// </summary>
    public partial class EditStudentUserControl : TabUserControl
    {
        private Student _editedStudent;

        public EditStudentUserControl(Student student)
        {
            _editedStudent = student;
            InitializeComponent();
        }

        public void FillFields(Student student)
        {
            FullNameTextBox.Text = student.FullName;
            SpecialityComboBox.Text = student.Speciality;
            FacultyComboBox.Text = student.Faculty;
            SexComboBox.Text = student.Sex;
            YearTextBox.Text = student.Year.ToString();
            AddressTypeTextBox.Text = student.Address;
            BirthDatePicker.SelectedDate = student.BirthDate;
        }

        private void TabUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SpecialityComboBox.ItemsSource = App.ServiceProvider.GetService<IStudentRepository>().GetUniqueSpecialities();
            FacultyComboBox.ItemsSource = App.ServiceProvider.GetService<IStudentRepository>().GetUniqueFaculties();
            SexComboBox.ItemsSource = App.ServiceProvider.GetService<IStudentRepository>().GetUniqueSexes();
            FillFields(_editedStudent);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _editedStudent.FullName = FullNameTextBox.Text;
                _editedStudent.Speciality = SpecialityComboBox.Text;
                _editedStudent.Faculty = FacultyComboBox.Text;
                _editedStudent.Sex = SexComboBox.Text;
                _editedStudent.Year = int.Parse(YearTextBox.Text);
                _editedStudent.Address = AddressTypeTextBox.Text;
                _editedStudent.BirthDate = BirthDatePicker.DisplayDate;
                App.ServiceProvider.GetService<IStudentRepository>().Update(_editedStudent);
                MessageBox.Show("Інформація про студента успішно оновлена!");
                App.ServiceProvider.GetService<TabsWindow>().CloseTab("СТУДЕНТ / РЕДАГУВАННЯ");
            }
            catch (Exception)
            {
                MessageBox.Show("Виникла помилка при редагуванні студента.");
                App.ServiceProvider.GetService<TabsWindow>().CloseTab("СТУДЕНТ / РЕДАГУВАННЯ");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            App.ServiceProvider.GetService<TabsWindow>().CloseTab("СТУДЕНТ / РЕДАГУВАННЯ");
        }

        private void AddressTypeTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]");
        }
    }
}
