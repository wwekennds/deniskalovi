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

namespace Lab02App
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

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            string digit = ((Button)sender).Content.ToString()!;
            
            if(Display_txtBox.Text == "0")
            {
                Display_txtBox.Text = digit;
            }
            else
            {
                Display_txtBox.Text += digit;
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Display_txtBox.Text = "0";
        }

        private void OpenLoginWindow_Click(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new Login();
            loginWindow.ShowDialog();
        }
    }
}