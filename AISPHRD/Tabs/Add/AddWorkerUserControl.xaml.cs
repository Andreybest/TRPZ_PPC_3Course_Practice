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
                _newWorker.Department = DepartmentTextBox.Text;
                _newWorker.Sex = SexTextBox.Text;
                _newWorker.BirthDate = BirthDatePicker.DisplayDate;
                _newWorker.WorkerType = WorkerTypeTextBox.Text;
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
    }
}
