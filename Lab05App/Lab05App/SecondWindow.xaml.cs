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
using System.Windows.Shapes;

namespace Lab05App
{
    /// <summary>
    /// Логика взаимодействия для SecondWindow.xaml
    /// </summary>
    public partial class SecondWindow : Window
    {
        public SecondWindow()
        {
            InitializeComponent();
        }

        private void OrdinaryButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Обычная кнопка нажата.", "Второе окно");
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Действие необратимо!", "Второе окно",
                           MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
