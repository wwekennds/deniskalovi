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

namespace Lab01App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int count = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            count++;
            string name = NameBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Введите имя.", "Ошибка ввода",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ResultText.Text = "Здравствуйте, " + name + "!";
            Title = $"Первое приложение (Кол-во кликов кнопки 'Приветствовать': {count})";
        }

        private void Button_Clear(object sender, RoutedEventArgs e) //задание 1
        {
            NameBox.Text = "";
            ResultText.Text = "";
        }

        private void Button_Close(object sender, RoutedEventArgs e) //событие кнопки закрыть
        {
            this.Close();  //закрытие окна
        }
    }
}