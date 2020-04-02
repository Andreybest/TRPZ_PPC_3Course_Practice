using AISPHRD.Models;
using AISPHRD.Repositories;
using AISPHRD.Windows;
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

namespace AISPHRD.Tabs.Edit
{
    /// <summary>
    /// Логика взаимодействия для EditMilitaryIDUserControl.xaml
    /// </summary>
    public partial class EditMilitaryIDUserControl : TabUserControl
    {
        private MilitaryID _editedMilitaryID;

        public EditMilitaryIDUserControl(MilitaryID militaryID)
        {
            _editedMilitaryID = militaryID;
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
                _editedMilitaryID.MilitaryIDPhoto = stream.ToArray();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _editedMilitaryID.WorkerId = (WorkerComboBox.SelectedItem as Worker).WorkerId;
                App.ServiceProvider.GetService<IMilitaryIDRepository>().Update(_editedMilitaryID);
                MessageBox.Show("Інформація про військовий білет працівника успішно оновлено!");
                App.ServiceProvider.GetService<TabsWindow>().CloseTab("ПРАЦІВНИК / РЕДАГУВАННЯ ВІЙСЬКОВОГО БІЛЕТУ");
            }
            catch (Exception)
            {
                MessageBox.Show("Виникла помилка при редагуванні військового білету працівника.");
                App.ServiceProvider.GetService<TabsWindow>().CloseTab("ПРАЦІВНИК / РЕДАГУВАННЯ ВІЙСЬКОВОГО БІЛЕТУ");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            App.ServiceProvider.GetService<TabsWindow>().CloseTab("ПРАЦІВНИК / РЕДАГУВАННЯ ВІЙСЬКОВОГО БІЛЕТУ");
        }

        private void TabUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            WorkerComboBox.ItemsSource = App.ServiceProvider.GetService<IWorkerRepository>().GetAll();
            if (WorkerComboBox.Items.Count > 0)
            {
                for (int i = 0; i < WorkerComboBox.Items.Count; i++)
                {
                    if ((WorkerComboBox.Items[i] as Worker).WorkerId == _editedMilitaryID.WorkerId)
                    {
                        WorkerComboBox.SelectedIndex = i;
                    }
                }
            }
        }
    }
}
