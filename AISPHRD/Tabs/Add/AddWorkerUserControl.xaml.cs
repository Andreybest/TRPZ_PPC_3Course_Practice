using AISPHRD.Models;
using AISPHRD.Repositories;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

        private void FilePickerButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".jpeg";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";


            // Display OpenFileDialog by calling ShowDialog method 
            bool? result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                FilePickerButton.Content = dlg.SafeFileName;

                byte[] bytes = File.ReadAllBytes(dlg.FileName);
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = new MemoryStream(bytes);
                bitmap.EndInit();

                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                MemoryStream stream = new MemoryStream();
                encoder.Save(stream);
                _newWorker.MilitaryID = stream.ToArray();
            }
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
