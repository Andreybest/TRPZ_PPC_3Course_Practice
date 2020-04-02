using AISPHRD.Models;
using AISPHRD.Repositories;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using AISPHRD.Windows;
using AISPHRD.Tabs.Edit;
using AISPHRD.Tabs.Add;

namespace AISPHRD.Tabs
{
    /// <summary>
    /// Логика взаимодействия для WorkersUserControl.xaml
    /// </summary>
    public partial class WorkersUserControl : TabUserControl
    {

        private IWorkerRepository _workerRepository;

        public WorkersUserControl()
        {
            _workerRepository = App.ServiceProvider.GetService<IWorkerRepository>();
            InitializeComponent();
            UpdateData();
        }

        public void UpdateTable(DataGrid dataGrid, IWorkerRepository workerRepository)
        {
            if (SearchTextBox.Text.Length > 0)
            {
                dataGrid.ItemsSource = workerRepository.GetAllBySearchString(SearchTextBox.Text);
                return;
            }
            dataGrid.ItemsSource = workerRepository.GetAll();
        }

        public override void UpdateData()
        {
            UpdateTable(WorkersDataGrid, _workerRepository);
            WorkersAmountLabel.Content = "Усього в системі зареєстрованно " + _workerRepository.GetAll().Count;
        }

        private void EditWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            App.ServiceProvider.GetService<TabsWindow>().CreateNewTab(new EditWorkerUserControl((WorkersDataGrid.SelectedItem as Worker)), "ПРАЦІВНИК / РЕДАГУВАННЯ");
        }

        private void DeleteWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show($"Ви точно хочете видалити {(WorkersDataGrid.SelectedItem as Worker).FullName}?", "Видалити працівника", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                _workerRepository.Delete((WorkersDataGrid.SelectedItem as Worker).WorkerId);
                UpdateData();
            }
        }

        private void AddWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            App.ServiceProvider.GetService<TabsWindow>().CreateNewTab(new AddWorkerUserControl(), "ПРАЦІВНИК / ДОДАВАННЯ");
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTable(WorkersDataGrid, _workerRepository);
        }
    }
}
