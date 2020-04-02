using AISPHRD.Models;
using AISPHRD.Repositories;
using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using AISPHRD.Windows;

namespace AISPHRD.Tabs.Edit
{
    /// <summary>
    /// Логика взаимодействия для EditConscriptUserControl.xaml
    /// </summary>
    public partial class EditConscriptUserControl : TabUserControl
    {
        private Conscript _editedConscript;

        public EditConscriptUserControl(Conscript conscript)
        {
            _editedConscript = conscript;
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _editedConscript.StudentId = (StudentComboBox.SelectedItem as Student).StudentId;
                _editedConscript.Commissariat = CommissariatTextBox.Text;
                App.ServiceProvider.GetService<IConscriptRepository>().Update(_editedConscript);
                MessageBox.Show("Інформація про військовозобов'язаного успішно оновлено!");
                App.ServiceProvider.GetService<TabsWindow>().CloseTab("ВІЙСЬКОВОЗОБОВ'ЯЗАНИЙ / РЕДАГУВАННЯ");
            }
            catch (Exception)
            {
                MessageBox.Show("Виникла помилка при редагуванні військовозобов'язаного.");
                App.ServiceProvider.GetService<TabsWindow>().CloseTab("ВІЙСЬКОВОЗОБОВ'ЯЗАНИЙ / РЕДАГУВАННЯ");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            App.ServiceProvider.GetService<TabsWindow>().CloseTab("ВІЙСЬКОВОЗОБОВ'ЯЗАНИЙ / РЕДАГУВАННЯ");
        }

        private void TabUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CommissariatTextBox.Text = _editedConscript.Commissariat;
            StudentComboBox.ItemsSource = App.ServiceProvider.GetService<IStudentRepository>().GetAll();
            if (StudentComboBox.Items.Count > 0)
            {
                for (int i = 0; i < StudentComboBox.Items.Count; i++)
                {
                    if ((StudentComboBox.Items[i] as Student).StudentId == _editedConscript.StudentId)
                    {
                        StudentComboBox.SelectedIndex = i;
                    }
                }
            }
        }
    }
}
