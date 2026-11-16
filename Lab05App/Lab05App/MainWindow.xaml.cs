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

namespace Lab05App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenSecondWindow_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new SecondWindow { Owner = this };
            wnd.Show();
        }

        private void LightTheme_Click(object sender, RoutedEventArgs e)
        {
            App.ApplyTheme("Light");
        }

        private void DarkTheme_Click(object sender, RoutedEventArgs e)
        {
            App.ApplyTheme("Dark");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void EmailBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AgeBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }

        private void ConfirmBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }

        private void AgreeCheck_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void AgreeCheck_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}