using AISPHRD.Models;
using AISPHRD.Repositories;
using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using AISPHRD.Windows;

namespace AISPHRD.Tabs.Add
{
    /// <summary>
    /// Логика взаимодействия для AddWorkerUserControl.xaml
    /// </summary>
    public partial class AddWorkerUserControl : TabUserControl
    {
        private Worker _newWorker;

        public AddWorkerUserControl()
        {
            _newWorker = new Worker();
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _newWorker.FullName = FullNameTextBox.Text;
                _newWorker.Department = DepartmentComboBox.Text;
                _newWorker.Sex = SexComboBox.Text;
                _newWorker.BirthDate = BirthDatePicker.SelectedDate.GetValueOrDefault();
                _newWorker.WorkerType = WorkerTypeComboBox.Text;
                App.ServiceProvider.GetService<IWorkerRepository>().Insert(_newWorker);
                MessageBox.Show("Новий працівник успішно доданий!");
                App.ServiceProvider.GetService<TabsWindow>().CloseTab("ПРАЦІВНИК / ДОДАВАННЯ");
            }
            catch (Exception)
            {
                MessageBox.Show("Виникла помилка при додаванні працівника.");
                App.ServiceProvider.GetService<TabsWindow>().CloseTab("ПРАЦІВНИК / ДОДАВАННЯ");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            App.ServiceProvider.GetService<TabsWindow>().CloseTab("ПРАЦІВНИК / ДОДАВАННЯ");
        }

        private void TabUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DepartmentComboBox.ItemsSource = App.ServiceProvider.GetService<IWorkerRepository>().GetUniqueDepartments();
            SexComboBox.ItemsSource = App.ServiceProvider.GetService<IWorkerRepository>().GetUniqueSexes();
            WorkerTypeComboBox.ItemsSource = App.ServiceProvider.GetService<IWorkerRepository>().GetUniqueWorkerTypes();
        }
    }
}
