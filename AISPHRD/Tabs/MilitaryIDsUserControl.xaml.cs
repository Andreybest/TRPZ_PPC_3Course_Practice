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
    /// Логика взаимодействия для MilitaryIDsUserControl.xaml
    /// </summary>
    public partial class MilitaryIDsUserControl : TabUserControl
    {

        private IMilitaryIDRepository _militaryIDRepository;

        public MilitaryIDsUserControl()
        {
            _militaryIDRepository = App.ServiceProvider.GetService<IMilitaryIDRepository>();
            InitializeComponent();
            UpdateData();
        }

        public void UpdateTable(DataGrid dataGrid, IMilitaryIDRepository militaryIDRepository)
        {
            if (SearchTextBox.Text.Length > 0)
            {
                dataGrid.ItemsSource = militaryIDRepository.GetAllBySearchString(SearchTextBox.Text);
                return;
            }
            dataGrid.ItemsSource = militaryIDRepository.GetAll();
        }

        public override void UpdateData()
        {
            UpdateTable(MilitaryIDsDataGrid, _militaryIDRepository);
            MilitaryIDsAmountLabel.Content = "Усього в системі зареєстрованно " + _militaryIDRepository.GetAll().Count;
        }

        private void OpenMilitaryIDButton_Click(object sender, RoutedEventArgs e)
        {
            App.ServiceProvider.GetService<TabsWindow>().CreateNewTab(new ViewMilitaryIDUserControl((MilitaryIDsDataGrid.SelectedItem as MilitaryID).MilitaryIDPhoto), "ПРАЦІВНИК / ПЕРЕГЛЯД ВІЙСЬКОВОГО БІЛЕТУ");
        }

        private void EditMilitaryIDButton_Click(object sender, RoutedEventArgs e)
        {
            App.ServiceProvider.GetService<TabsWindow>().CreateNewTab(new EditMilitaryIDUserControl((MilitaryIDsDataGrid.SelectedItem as MilitaryID)), "ПРАЦІВНИК / РЕДАГУВАННЯ ВІЙСЬКОВОГО БІЛЕТУ");
        }

        private void DeleteMilitaryIDButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show($"Ви точно хочете видалити військовий білет {(MilitaryIDsDataGrid.SelectedItem as MilitaryID).Worker.FullName}?", "Видалити військовий білет працівника", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                _militaryIDRepository.Delete((MilitaryIDsDataGrid.SelectedItem as MilitaryID).MilitaryIDId);
                UpdateData();
            }
        }

        private void AddMilitaryIDButton_Click(object sender, RoutedEventArgs e)
        {
            App.ServiceProvider.GetService<TabsWindow>().CreateNewTab(new AddMilitaryIDUserControl(), "ПРАЦІВНИК / ДОДАВАННЯ ВІЙСЬКОВОГО БІЛЕТУ");
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTable(MilitaryIDsDataGrid, _militaryIDRepository);
        }
    }
}
