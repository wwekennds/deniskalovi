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

namespace Lab07App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //закоментировано для задания 3, при желании можно раскоментировать
            //DataContext = new UserProfile
            //{
            //    LastName = "Иванов",
            //    FirstName = "Иван",
            //    Surname = "Иванович",
            //    Age = 19,
            //    Email = "5gB0K@example.com",
            //    NumberPhone = "+7 (999) 999-99-99"
            //};

        }
    }
}