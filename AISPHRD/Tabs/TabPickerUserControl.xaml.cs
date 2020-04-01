using AISPHRD.Windows;
using CommonServiceLocator;
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

namespace AISPHRD.Tabs
{
    /// <summary>
    /// Логика взаимодействия для TabPickerUserControl.xaml
    /// </summary>
    public partial class TabPickerUserControl : TabUserControl
    {

        public TabPickerUserControl()
        {
            InitializeComponent();
        }

        private void WorkersButton_Click(object sender, RoutedEventArgs e)
        {
            App.ServiceProvider.GetService<TabsWindow>().CreateNewTab(new WorkersUserControl(), "ПРАЦІВНИКИ");
        }

        private void StudentsButton_Click(object sender, RoutedEventArgs e)
        {
            App.ServiceProvider.GetService<TabsWindow>().CreateNewTab(new StudentsUserControl(), "СТУДЕНТИ");
        }

        private void ConscriptsButton_Click(object sender, RoutedEventArgs e)
        {
            App.ServiceProvider.GetService<TabsWindow>().CreateNewTab(new ConscriptsUserControl(), "ВІЙСЬКОВОЗОБОВ'ЯЗАНІ");
        }

        private void TabelButton_Click(object sender, RoutedEventArgs e)
        {
            App.ServiceProvider.GetService<TabsWindow>().CreateNewTab(new TabelUserControl(), "ТАБЕЛЬ");
        }
    }
}
