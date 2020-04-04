using AISPHRD.Models;
using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using AISPHRD.Windows;
using AISPHRD.Repositories;

namespace AISPHRD.Tabs.Edit
{
    /// <summary>
    /// Логика взаимодействия для EditWorkerUserControl.xaml
    /// </summary>
    public partial class EditWorkerUserControl : TabUserControl
    {

        private Worker _editedWorker;

        public EditWorkerUserControl(Worker worker)
        {
            _editedWorker = worker;
            InitializeComponent();
        }

        public void FillFields(Worker worker)
        {
            FullNameTextBox.Text = worker.FullName;
            DepartmentComboBox.Text = worker.Department;
            SexComboBox.Text = worker.Sex;
            BirthDatePicker.SelectedDate = worker.BirthDate;
            WorkerTypeComboBox.Text = worker.WorkerType;
        }

        private void TabUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DepartmentComboBox.ItemsSource = App.ServiceProvider.GetService<IWorkerRepository>().GetUniqueDepartments();
            SexComboBox.ItemsSource = App.ServiceProvider.GetService<IWorkerRepository>().GetUniqueSexes();
            WorkerTypeComboBox.ItemsSource = App.ServiceProvider.GetService<IWorkerRepository>().GetUniqueWorkerTypes();
            FillFields(_editedWorker);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _editedWorker.FullName = FullNameTextBox.Text;
                _editedWorker.Department = DepartmentComboBox.Text;
                _editedWorker.Sex = SexComboBox.Text;
                _editedWorker.BirthDate = BirthDatePicker.DisplayDate;
                _editedWorker.WorkerType = WorkerTypeComboBox.Text;
                App.ServiceProvider.GetService<IWorkerRepository>().Update(_editedWorker);
                MessageBox.Show("Інформація про працівника успішно оновлена!");
                App.ServiceProvider.GetService<TabsWindow>().CloseTab("ПРАЦІВНИК / РЕДАГУВАННЯ");
            }
            catch (Exception)
            {
                MessageBox.Show("Виникла помилка при редагуванні працівника.");
                App.ServiceProvider.GetService<TabsWindow>().CloseTab("ПРАЦІВНИК / РЕДАГУВАННЯ");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            App.ServiceProvider.GetService<TabsWindow>().CloseTab("ПРАЦІВНИК / РЕДАГУВАННЯ");
        }
    }
}
