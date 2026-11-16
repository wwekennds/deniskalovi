using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lab03App
{
    /// <summary>
    /// Логика взаимодействия для PlaceAnOrder.xaml
    /// </summary>
    public partial class PlaceAnOrder : Window
    {
        private class Dish
        {
            public string? Name { get; set; }
            public double Price { get; set; }
            public CheckBox? Check { get; set; }
        }

        // Цены блюд хранятся здесь
        private readonly List<Dish> dishes =
        [
            new () { Name = "Пицца Маргарита", Price = 450 },
            new () { Name = "Салат Цезарь",    Price = 320 },
            new () { Name = "Стейк",           Price = 890 },
            new () { Name = "Паста Карбонара", Price = 410 },
            new () { Name = "Суп Том Ям",      Price = 380 },
            new () { Name = "Десерт",          Price = 220 },
        ];
        public PlaceAnOrder()
        {
            InitializeComponent();
            BuildDishesUI();
        }
        private void BuildDishesUI()
        {
            foreach (var dish in dishes)
            {
                var cb = new CheckBox
                {
                    Content = $"{dish.Name} — {dish.Price:F2} ₽",
                    Margin = new Thickness(0, 3, 0, 3)
                };
                dish.Check = cb;
                DishesPanel.Children.Add(cb);
            }
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            double baseSum = 0;
            foreach (var dish in dishes)
                if (dish.Check?.IsChecked == true)
                    baseSum += dish.Price;

            double sizeCoef = 1.0;
            if (MediumRadio.IsChecked == true) sizeCoef = 1.3;
            else if (LargeRadio.IsChecked == true) sizeCoef = 1.6;

            double tipPercent = TipSlider.Value;

            double sumWithSize = baseSum * sizeCoef;
            double tip = sumWithSize * tipPercent / 100.0;
            double total = sumWithSize + tip;

            TotalText.Text =
                $"Блюда: {baseSum:F2} ₽\n" +
                $"С учётом размера (×{sizeCoef}): {sumWithSize:F2} ₽\n" +
                $"Чаевые ({tipPercent:F0}%): {tip:F2} ₽\n" +
                $"Итого: {total:F2} ₽";

            PayText.Text = $"К оплате: {total:F2} руб.";

            AnimateProgress();
        }

        private void AnimateProgress()
        {
            Progress.Value = 0;

            var animation = new DoubleAnimation
            {
                From = 0,
                To = 100,
                Duration = TimeSpan.FromSeconds(1.5),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            Progress.BeginAnimation(ProgressBar.ValueProperty, animation);
        }

        private void TipSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TipText?.Text = $"Чаевые: {e.NewValue:F0}%";
        }
    }
}
