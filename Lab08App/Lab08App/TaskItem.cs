using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;


namespace Lab08App
{
    public class TaskItem : INotifyPropertyChanged
    {
        private string _title = "";
        private bool _isDone;
        private int _priority = 1;
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title == value) return;
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        public bool IsDone
        {
            get { return _isDone; }
            set
            {
                if (_isDone == value) return;
                _isDone = value;
                OnPropertyChanged("IsDone");
            }
        }
        public int Priority
        {
            get { return _priority; }
            set
            {
                if (_priority == value) return;
                _priority = value;
                OnPropertyChanged("Priority");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }

    }
}
