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

namespace Lab03App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            for (int d = 1; d <= 31; d++)
            {
                DaySelect.Items.Add(d);
            }
            DaySelect.SelectedIndex = 0;

            for (int m = 1; m <= 12; m++)
            {
                MonthSelect.Items.Add(m);
            }
            MonthSelect.SelectedIndex = 0;

            for (int y = 1970; y <= 2026; y++)
            {
                YearSelect.Items.Add(y);
            }
            YearSelect.SelectedIndex = 0;
        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            string surname = SurnameBox.Text.Trim();
            string name = NameBox.Text.Trim();

            if (surname.Length == 0 || name.Length == 0)
            {
                MessageBox.Show("Заполните фамилию и имя.", "Ошибка ввода",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string group = (GroupSelect.SelectedItem as ComboBoxItem)?.Content.ToString()!;
            string course = Course1.IsChecked == true ? "1" :
                            Course2.IsChecked == true ? "2" :
                            Course3.IsChecked == true ? "3" : "4";
            string laptop = LaptopCheck.IsChecked == true
                ? "ноутбук есть" : "ноутбука нет";

            int day = (int)DaySelect.SelectedItem;
            int month = (int)MonthSelect.SelectedItem;
            int year = (int)YearSelect.SelectedItem;

            string date = $"{day:d2}.{month:d2}.{year:d2}";
            string pol = Man.IsChecked == true ? "мужской" : Woman.IsChecked == true ? "женский" : "не определено";

            SummaryBox.Text = "Студент: " + surname + " " + name + " " + "Группа: " + group + ", курс: " + course + " " + "Пол: " + 
                pol + " " + "Дата рождения: " + date + " " + "Оборудование: " + laptop + " " + "Уровень подготовки: " + LevelSlider.Value + " из 10";

        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            PlaceAnOrder open = new();
            open.Show();
        }
    }
}