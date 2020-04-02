using AISPHRD.Windows;
using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Diagnostics;

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

        private void MilitaryIDsButton_Click(object sender, RoutedEventArgs e)
        {
            App.ServiceProvider.GetService<TabsWindow>().CreateNewTab(new MilitaryIDsUserControl(), "ПРАЦІВНИКИ / ВІЙСЬКОВІ БІЛЕТИ");
        }

        private void TabelButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string excelFilePath = Directory.GetCurrentDirectory() + @"\Excel\forma_p-5_6.xls";
                Process.Start("cmd.exe", "/c " + excelFilePath);
            }
            catch (Exception)
            {
                MessageBox.Show("Сталася помилка при відкритті файлу, будь-ласка, перевірте чи стоїть у вас позамовчуванням программа для відкриття .xls файлів. І те що сам файл табелю не видалений.");
            }
        }
    }
}
