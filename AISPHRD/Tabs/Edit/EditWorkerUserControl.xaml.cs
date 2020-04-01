using AISPHRD.Models;
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
using System.Diagnostics;
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
            DepartmentTextBox.Text = worker.Department;
            SexTextBox.Text = worker.Sex;
            BirthDatePicker.SelectedDate = worker.BirthDate;
            WorkerTypeTextBox.Text = worker.WorkerType;
        }

        private void TabUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillFields(_editedWorker);
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
                _editedWorker.MilitaryID = stream.ToArray();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _editedWorker.FullName = FullNameTextBox.Text;
                _editedWorker.Department = DepartmentTextBox.Text;
                _editedWorker.Sex = SexTextBox.Text;
                _editedWorker.BirthDate = BirthDatePicker.DisplayDate;
                _editedWorker.WorkerType = WorkerTypeTextBox.Text;
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
