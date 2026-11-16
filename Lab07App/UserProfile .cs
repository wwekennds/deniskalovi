using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab07App
{
    public class UserProfile : INotifyPropertyChanged
    {
        private string _lastName = "";
        private string _firstName = "";
        private string _surName = "";
        private int _age;
        private string _email = "";
        private string _numberPhone = "";
        private string _initials = "";

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName == value) return;
                _lastName = value;
                OnPropertyChanged("LastName");
                OnPropertyChanged("FullName");
                OnPropertyChanged("Initials");
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName == value) return;
                _firstName = value;
                OnPropertyChanged("FirstName");
                OnPropertyChanged("FullName");
                OnPropertyChanged("Initials");
            }
        }

        public string Surname
        {
            get { return _surName; }
            set
            {
                if (_surName == value) return;
                _surName = value;
                OnPropertyChanged("SurName");
                OnPropertyChanged("Initials");
            }
        }

        public int Age
        {
            get { return _age; }
            set
            {
                if (_age == value) return;
                _age = value;
                OnPropertyChanged("Age");
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                if (_email == value) return;
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        public string NumberPhone
        {
            get { return _numberPhone; }
            set
            {
                if (_numberPhone == value) return;
                _numberPhone = value;
                OnPropertyChanged("NumberPhone");
            }
        }

        public string FullName
        {
            get { return (LastName + " " + FirstName).Trim(); }
        }

        public string Initials
        {
            get { return (LastName + " " + FirstName[0] + "." + Surname[0]).Trim(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }

    }
}
