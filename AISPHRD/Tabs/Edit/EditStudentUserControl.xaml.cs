using AISPHRD.Models;
using AISPHRD.Windows;
using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using AISPHRD.Repositories;

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
            SpecialityTextBox.Text = student.Speciality;
            FacultyTextBox.Text = student.Faculty;
            SexTextBox.Text = student.Sex;
            YearTextBox.Text = student.Year.ToString();
            AddressTypeTextBox.Text = student.Address;
            BirthDatePicker.SelectedDate = student.BirthDate;
        }

        private void TabUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillFields(_editedStudent);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _editedStudent.FullName = FullNameTextBox.Text;
                _editedStudent.Speciality = SpecialityTextBox.Text;
                _editedStudent.Faculty = FacultyTextBox.Text;
                _editedStudent.Sex = SexTextBox.Text;
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
    }
}
