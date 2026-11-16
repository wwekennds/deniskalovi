using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lab10App.Models;

namespace Lab10App.ViewModels
{
    public partial class TodoViewModel : ObservableObject
    {
        public ObservableCollection<TaskItem> Todos { get; } = new();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddCommand))]
        private string _newTaskTitle = "";

        [ObservableProperty]
        private int _newTaskPriority;

        public string Summary =>
            $"Выполнено: {Todos.Count(t => t.IsDone)} из {Todos.Count}";

        public TodoViewModel()
        {
            Todos.CollectionChanged += OnTodosCollectionChanged;
        }

        [RelayCommand(CanExecute = nameof(CanAdd))]
        private void Add()
        {
            Todos.Add(new TaskItem
            {
                Title = NewTaskTitle.Trim(),
                Priority = NewTaskPriority + 1
            });
            NewTaskTitle = "";
        }

        private bool CanAdd() => !string.IsNullOrWhiteSpace(NewTaskTitle);

        [RelayCommand(CanExecute = nameof(CanDelete))]
        private void Delete(TaskItem task)
        {
            Todos.Remove(task);
        }

        private bool CanDelete(TaskItem? task) => task is not null;

        [RelayCommand(CanExecute = nameof(CanMarkDone))]
        private void MarkDone(TaskItem task)
        {
            task.IsDone = true;
        }

        private bool CanMarkDone(TaskItem? task)
            => task is not null && !task.IsDone;

        private void OnTodosCollectionChanged(
            object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
                foreach (INotifyPropertyChanged item in e.OldItems)
                    item.PropertyChanged -= OnTaskPropertyChanged;

            if (e.NewItems != null)
                foreach (INotifyPropertyChanged item in e.NewItems)
                    item.PropertyChanged += OnTaskPropertyChanged;

            OnPropertyChanged(nameof(Summary));
        }

        private void OnTaskPropertyChanged(
            object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TaskItem.IsDone))
                OnPropertyChanged(nameof(Summary));
        }
    }
}