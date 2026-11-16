using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Lab11App
{
    public class DateToRelativeStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value is not DateTime date)
                return string.Empty;

            // Сравниваем только даты, без времени
            DateTime today = DateTime.Today;
            DateTime dateOnly = date.Date;

            int days = (today - dateOnly).Days;

            if (days <= 0)
                return "сегодня";
            if (days == 1)
                return "вчера";

            return days + " дней назад";
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
