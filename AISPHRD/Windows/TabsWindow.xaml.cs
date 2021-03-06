﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AISPHRD.Tabs;
using Microsoft.Extensions.DependencyInjection;

namespace AISPHRD.Windows
{
    /// <summary>
    /// Логика взаимодействия для TabsWindow.xaml
    /// </summary>
    public partial class TabsWindow : Window
    {
        private IServiceProvider _serviceProvider;
        public TabsWindow()
        {
            _serviceProvider = App.ServiceProvider;
            InitializeComponent();
        }

        public TabItem FindTab(string name)
        {
            for (int i = 0; i < TabablzTabControl.Items.Count; i++)
            {
                if ((TabablzTabControl.Items[i] as TabItem).Header.ToString() == name)
                {
                    return (TabablzTabControl.Items[i] as TabItem);
                }
            }
            return null;
        }

        public bool TabExists(string name)
        {
            if (FindTab(name) != null) return true;
            return false;
        }

        public void SwitchTab(string name)
        {
            TabItem tab = FindTab(name);
            if (tab != null)
            {
                TabablzTabControl.SelectedItem = tab;
            }
        }

        public void CreateNewTab(UserControl userControl, string name)
        {
            if (TabExists(name))
            {
                SwitchTab(name);
                return;
            }

            TabItem newTabItem = new TabItem
            {
                Header = name,
                Content = userControl
            };
            TabablzTabControl.Items.Add(newTabItem);
            SwitchTab(name);
        }

        public void CloseTab(string name)
        {
            TabItem tab = FindTab(name);
            if (tab != null)
            {
                TabablzTabControl.Items.Remove(tab);
            }
        }

        private void HeaderGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (WindowState == WindowState.Maximized)
                {
                    WindowState = WindowState.Normal;
                }
                else if (WindowState == WindowState.Normal)
                {
                    WindowState = WindowState.Maximized;
                }
            }

            DragMove();
        }

        private void PowerButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = _serviceProvider.GetService<LoginWindow>();
            loginWindow.Show();
            for (int i = 0; i < Application.Current.Windows.Count; i++)
            {
                if (Application.Current.Windows[i] is TabsWindow)
                {
                    Application.Current.Windows[i].Hide();
                }
            }
        }

        private void TabablzTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TabablzTabControl.SelectedItem != null)
            {
                ((TabablzTabControl.SelectedItem as TabItem).Content as TabUserControl).OnSelection();
            }
        }

        private void CloseTabButton_Click(object sender, RoutedEventArgs e)
        {
            if ((TabablzTabControl.SelectedItem as TabItem).Header is string)
            {
                CloseTab((TabablzTabControl.SelectedItem as TabItem).Header.ToString());
            }
        }
    }
}
