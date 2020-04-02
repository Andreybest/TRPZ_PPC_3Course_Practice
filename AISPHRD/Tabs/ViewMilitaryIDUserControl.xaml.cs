using AISPHRD.Windows;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace AISPHRD.Tabs
{
    /// <summary>
    /// Логика взаимодействия для ViewMilitaryIDUserControl.xaml
    /// </summary>
    public partial class ViewMilitaryIDUserControl : TabUserControl
    {
        private byte[] _image;
        public ViewMilitaryIDUserControl(byte[] image)
        {
            _image = image;
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            App.ServiceProvider.GetService<TabsWindow>().CloseTab("ПРАЦІВНИК / ПЕРЕГЛЯД ВІЙСЬКОВОГО БІЛЕТУ");
        }

        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        private void TabUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            BitmapImage bitmapImage = LoadImage(_image);
            if (bitmapImage == null)
            {
                MessageBox.Show("Цей працівник не має білету, або не вдалося загрузити картинку з БД");
                App.ServiceProvider.GetService<TabsWindow>().CloseTab("ПРАЦІВНИК / ПЕРЕГЛЯД ВІЙСЬКОВОГО БІЛЕТУ");
                return;
            }
            MilitaryIDImage.Source = bitmapImage;
        }
    }
}
