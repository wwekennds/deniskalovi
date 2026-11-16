using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
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


namespace Lab09App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<TaskItem> Tasks { get; }
            = new ObservableCollection<TaskItem>();

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand MarkDoneCommand { get; }

        public MainWindow()
        {
            InitializeComponent();
            TaskList.ItemsSource = Tasks;

            AddCommand = new RelayCommand(
                _ => AddTask(),
                _ => !string.IsNullOrWhiteSpace(InputBox.Text));

            DeleteCommand = new RelayCommand(
                parameter =>
                {
                    if (parameter is TaskItem task)
                    {
                        Tasks.Remove(task);
                        UpdateCounters();
                    }
                },
                parameter => parameter is TaskItem);

            MarkDoneCommand = new RelayCommand(
                parameter =>
                {
                    if (parameter is TaskItem task)
                    {
                        task.IsDone = true;
                        CommandManager.InvalidateRequerySuggested();
                        UpdateCounters();
                    }
                },
                parameter => parameter is TaskItem task && !task.IsDone);

            DataContext = this;
        }

        private void AddTask()
        {
            Tasks.Add(new TaskItem { Title = InputBox.Text.Trim() });
            InputBox.Clear();
            UpdateCounters();
        }

        private void UpdateCounters()
        {
            int done = Tasks.Count(t => t.IsDone);
            CounterText.Text = "Выполнено: " + done + " из " + Tasks.Count;
        }

    }
}