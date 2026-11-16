using System;
using System.Collections.Generic;
using System.Text;

namespace Lab10App.ViewModels
{
    public class BmiViewModel : ViewModelBase
    {
        private double _weight = 60;
        private double _height = 170;

        public double Weight
        {
            get { return _weight; }
            set
            {
                if (_weight == value) return;
                _weight = value;
                OnPropertyChanged("Weight");
                OnPropertyChanged("Bmi");
                OnPropertyChanged("Category");
            }
        }

        public double Height
        {
            get { return _height; }
            set
            {
                if (_height == value) return;
                _height = value;
                OnPropertyChanged("Height");
                OnPropertyChanged("Bmi");
                OnPropertyChanged("Category");
            }
        }

        public double Bmi
        {
            get
            {
                double meters = Height / 100.0;
                if (meters <= 0) return 0;
                return Math.Round(Weight / (meters * meters), 1);
            }
        }

        public string Category
        {
            get
            {
                if (Bmi < 18.5) return "Недостаточный вес";
                if (Bmi < 25) return "Норма";
                if (Bmi < 30) return "Избыточный вес";
                return "Ожирение";
            }
        }

    }
}
