using Lab11App;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.PerformanceData;
using System.Globalization;
using System.Linq;
using System.Printing;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Lab11App
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

        public void SaveData(int i = 0)
        {
            var dialog = new SaveFileDialog
            {
                Title = "Сохранение списка задач",
                Filter = "JSON-файлы (*.json)|*.json|Все файлы (*.*)|*.*",
                FileName = "tasks.json"
            };

            if(i == 1) if (dialog.ShowDialog() != true) return;

            try
            {
                string json = JsonSerializer.Serialize(Tasks.ToList());
                File.WriteAllText(dialog.FileName, json);
                if(i == 1)
                {
                    MessageBox.Show("Файл сохранён.", "Сохранение",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить файл: " + ex.Message,
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        public void LoadData(int i = 0)
        {
            var dialog = new OpenFileDialog
            {
                Title = "Открытие списка задач",
                Filter = "JSON-файлы (*.json)|*.json|Все файлы (*.*)|*.*"
            };

            if (i == 1) if (dialog.ShowDialog() != true) return;

            try
            {
                string json = i == 0 ? File.ReadAllText("tasks.json") : File.ReadAllText(dialog.FileName);
                var tasks = JsonSerializer.Deserialize<List<TaskItem>>(json);

                Tasks.Clear();
                if (tasks != null)
                {
                    foreach (TaskItem task in tasks)
                    {
                        Tasks.Add(task);
                    }
                }
                UpdateCounters();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось открыть файл: " + ex.Message,
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddTask()
        {
            int priority = PriorityBox.SelectedItem is ComboBoxItem selectedPriority
                && int.TryParse(selectedPriority.Content?.ToString(), out int selectedValue)
                    ? selectedValue
                    : 1;
            Tasks.Add(new TaskItem
            {
                Title = InputBox.Text.Trim(),
                Priority = priority
            });
            InputBox.Clear();
            UpdateCounters();
        }

        private void TaskCheckBox_Click(object sender, RoutedEventArgs e)
        {
            UpdateCounters();
        }

        private void UpdateCounters()
        {
            int done = Tasks.Count(t => t.IsDone);
            CounterText.Text = "Выполнено: " + done + " из " + Tasks.Count;
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            LoadData(1);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveData(1);
        }

        private void ExportSummary_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog
            {
                Title = "Экспорт сводки задач",
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                FileName = "task-summary.txt"
            };

            if (dialog.ShowDialog() != true) return;

            int completed = Tasks.Count(task => task.IsDone);
            int incomplete = Tasks.Count - completed;
            string summary = $"Сводка задач от {DateTime.Now:dd.MM.yyyy HH:mm:ss}{Environment.NewLine}" +
                $"Всего задач: {Tasks.Count}{Environment.NewLine}" +
                $"Выполнено: {completed}{Environment.NewLine}" +
                $"Не выполнено: {incomplete}{Environment.NewLine}";

            try
            {
                File.WriteAllText(dialog.FileName, summary, Encoding.UTF8);
                MessageBox.Show("Сводка сохранена.", "Экспорт",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить сводку: " + ex.Message,
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveData();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        //PropertyChanged — не свойство задачи с данными, а событие для уведомления интерфейса WPF об изменении свойства. Оно возникает во
        //время работы приложения, когда меняются Title, IsDone, Priority или CreatedAt. События не являются сохранёнными данными объекта,
        //поэтому сериализатор не записывает их в JSON.

    }
}