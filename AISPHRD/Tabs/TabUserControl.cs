using System.Windows.Controls;

namespace AISPHRD.Tabs
{
    /// <summary>
    /// Логика взаимодействия для TabUserControl.xaml
    /// </summary>
    public class TabUserControl : UserControl
    {
        public TabUserControl() : base() { }

        public virtual void UpdateData() { }

        public void OnSelection()
        {
            UpdateData();
        }
    }
}
