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
using System.Linq;
using System.Collections.ObjectModel;


namespace Lab08App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<TaskItem> Tasks { get; } = new ObservableCollection<TaskItem>();

        public MainWindow()
        {
            InitializeComponent();
            TaskList.ItemsSource = Tasks;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string title = InputBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(title)) return;

            int priority = PriorityBox.SelectedIndex + 1;

            Tasks.Add(new TaskItem { Title = title, Priority = priority });
            InputBox.Clear();
            UpdateCounters();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (TaskList.SelectedItem is TaskItem task)
            {
                Tasks.Remove(task);
                UpdateCounters();
            }
        }
        private void UpdateCounters()
        {
            int total = Tasks.Count();
            int done = Tasks.Count(t => t.IsDone);
            int percent = done * 100 / total;
            CounterText.Text = $"Выполнено: {done} из {total} ({percent:F1}%)";
        }

        private void Task_IsDoneChanged(object sender, RoutedEventArgs e)
        {
            UpdateCounters();
        }

        private void DeleteCompletedButton_Click(object sender, RoutedEventArgs e)
        {
            for(int i = Tasks.Count - 1; i >= 0; i--) 
            {
                if (Tasks[i].IsDone)
                {
                    Tasks.RemoveAt(i);
                }
            }
            UpdateCounters();
        }
    }
}