using AISPHRD.Repositories;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using AISPHRD.Windows;
using AISPHRD.Models;
using AISPHRD.Tabs.Add;
using AISPHRD.Tabs.Edit;

namespace AISPHRD.Tabs
{
    /// <summary>
    /// Логика взаимодействия для ConscriptsUserControl.xaml
    /// </summary>
    public partial class ConscriptsUserControl : TabUserControl
    {

        private IConscriptRepository _conscriptRepository;

        public ConscriptsUserControl()
        {
            _conscriptRepository = App.ServiceProvider.GetService<IConscriptRepository>();
            InitializeComponent();
            UpdateData();
        }

        public void UpdateTable(DataGrid dataGrid, IConscriptRepository conscriptRepository)
        {
            if (SearchTextBox.Text.Length > 0)
            {
                dataGrid.ItemsSource = conscriptRepository.GetAllBySearchString(SearchTextBox.Text);
                return;
            }
            dataGrid.ItemsSource = conscriptRepository.GetAll();
        }

        public override void UpdateData()
        {
            UpdateTable(ConscriptsDataGrid, _conscriptRepository);
            ConscriptsAmountLabel.Content = "Усього в системі зареєстрованно " + _conscriptRepository.GetAll().Count;
        }

        private void EditConscriptButton_Click(object sender, RoutedEventArgs e)
        {
            App.ServiceProvider.GetService<TabsWindow>().CreateNewTab(new EditConscriptUserControl((ConscriptsDataGrid.SelectedItem as Conscript)), "ВІЙСЬКОВОЗОБОВ'ЯЗАНИЙ / РЕДАГУВАННЯ");
        }

        private void DeleteConscriptButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show($"Ви точно хочете видалити {(ConscriptsDataGrid.SelectedItem as Conscript).Student.FullName}?", "Видалити військовозобов'язаного", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                _conscriptRepository.Delete((ConscriptsDataGrid.SelectedItem as Conscript).ConscriptId);
                UpdateData();
            }
        }

        private void AddConscriptButton_Click(object sender, RoutedEventArgs e)
        {
            App.ServiceProvider.GetService<TabsWindow>().CreateNewTab(new AddConscriptUserControl(), "ВІЙСЬКОВОЗОБОВ'ЯЗАНИЙ / ДОДАВАННЯ");
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTable(ConscriptsDataGrid, _conscriptRepository);
        }
    }
}
