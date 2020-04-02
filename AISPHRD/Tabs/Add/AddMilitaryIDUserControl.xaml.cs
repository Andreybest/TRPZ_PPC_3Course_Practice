using AISPHRD.Models;
using AISPHRD.Repositories;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Extensions.DependencyInjection;
using AISPHRD.Windows;

namespace AISPHRD.Tabs.Add
{
    /// <summary>
    /// Логика взаимодействия для AddMilitaryIDUserControl.xaml
    /// </summary>
    public partial class AddMilitaryIDUserControl : TabUserControl
    {
        private MilitaryID _newMilitaryID;

        public AddMilitaryIDUserControl()
        {
            _newMilitaryID = new MilitaryID();
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
                _newMilitaryID.MilitaryIDPhoto = stream.ToArray();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _newMilitaryID.WorkerId = (WorkerComboBox.SelectedItem as Worker).WorkerId;
                App.ServiceProvider.GetService<IMilitaryIDRepository>().Insert(_newMilitaryID);
                MessageBox.Show("Новий військовий білет працівника успішно доданий!");
                App.ServiceProvider.GetService<TabsWindow>().CloseTab("ПРАЦІВНИК / ДОДАВАННЯ ВІЙСЬКОВОГО БІЛЕТУ");
            }
            catch (Exception)
            {
                MessageBox.Show("Виникла помилка при додаванні військового білету працівника.");
                App.ServiceProvider.GetService<TabsWindow>().CloseTab("ПРАЦІВНИК / ДОДАВАННЯ ВІЙСЬКОВОГО БІЛЕТУ");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            App.ServiceProvider.GetService<TabsWindow>().CloseTab("ПРАЦІВНИК / ДОДАВАННЯ ВІЙСЬКОВОГО БІЛЕТУ");
        }

        private void TabUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            WorkerComboBox.ItemsSource = App.ServiceProvider.GetService<IWorkerRepository>().GetAll();
            if (WorkerComboBox.Items.Count > 0)
            {
                WorkerComboBox.SelectedIndex = 0;
            }
        }
    }
}
