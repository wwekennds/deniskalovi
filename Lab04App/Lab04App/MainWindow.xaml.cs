using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab04App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> errors = new List<string>();
        private bool isRegistered = false;
        public MainWindow()
        {
            InitializeComponent();

            UpdatePasswordStrength();
        }

        private bool ValidateName()
        {
            if (string.IsNullOrWhiteSpace(NameBox.Text))
            {
                NameBox.BorderBrush = Brushes.Red;
                NameBox.BorderThickness = new Thickness(2);

                NameBox.Focus();

                return false;
            }

            NameBox.ClearValue(TextBox.BorderBrushProperty);
            NameBox.ClearValue(TextBox.BorderThicknessProperty);

            return true;
        }

        private bool IsPasswordStrong(string password)
        {
            if (password.Length < 8) return false;
            if (!Regex.IsMatch(password, @"\d")) return false;       
            if (!Regex.IsMatch(password, @"[A-Z]")) return false;    
            return true;
        }

        private void UpdatePasswordStrength()
        {
            string password = PasswordBox.Password;

            if (password.Length == 0)
            {
                PasswordStrengthText.Text = "";
                return;
            }

            bool hasLength = password.Length >= 8;
            bool hasDigit = Regex.IsMatch(password, @"\d");
            bool hasUpper = Regex.IsMatch(password, @"[A-Z]");

            int score = (hasLength ? 1 : 0) + (hasDigit ? 1 : 0) + (hasUpper ? 1 : 0);

            switch (score)
            {
                case 3:
                    PasswordStrengthText.Text = "Надёжный пароль";
                    PasswordStrengthText.Foreground = Brushes.Green;
                    break;
                case 2:
                    PasswordStrengthText.Text = "Средний пароль";
                    PasswordStrengthText.Foreground = Brushes.Orange;
                    break;
                default:
                    PasswordStrengthText.Text = "Слабый пароль";
                    PasswordStrengthText.Foreground = Brushes.Red;
                    break;
            }
        }

        private bool HasUnsavedData()
        {
            if (isRegistered) return false;

            if (!string.IsNullOrWhiteSpace(NameBox.Text)) return true;
            if (!string.IsNullOrWhiteSpace(EmailBox.Text)) return true;
            if (!string.IsNullOrWhiteSpace(AgeBox.Text)) return true;
            if (PasswordBox.Password.Length > 0) return true;
            if (ConfirmBox.Password.Length > 0) return true;
            if (AgreeCheck.IsChecked == true) return true;

            return false;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            errors.Clear();

            if (!ValidateName())
                errors.Add("Не указано имя.");

            string email = EmailBox.Text.Trim();
            if (!email.Contains("@") || email.Length < 5)
                errors.Add("Некорректный адрес электронной почты.");

            if (!int.TryParse(AgeBox.Text, out int age) || age < 14 || age > 100)
                errors.Add("Возраст должен быть числом от 14 до 100.");

            string password = PasswordBox.Password;
            if (password != ConfirmBox.Password)
            {
                errors.Add("Пароли не совпадают.");
            }
            else if (!IsPasswordStrong(password))
            {
                errors.Add("Пароль должен содержать не менее 8 символов, " +
                           "хотя бы одну цифру и одну заглавную латинскую букву.");
            }

            if (AgreeCheck.IsChecked != true)
                errors.Add("Не принято согласие на обработку данных.");

            if (errors.Count > 0)
            {
                MessageBox.Show("Исправьте ошибки ввода:" + Environment.NewLine +
                                string.Join(Environment.NewLine, errors),
                    "Проверка данных", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult answer = MessageBox.Show(
                "Вы уверены, что хотите завершить регистрацию?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (answer != MessageBoxResult.Yes)
            {
                MessageBox.Show("Регистрация отменена.",
                    "Отмена", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            isRegistered = true;

            MessageBox.Show("Регистрация выполнена.", "Готово",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AgeBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(AgeBox.Text.Trim(), out int age) && age >= 0 && age <= 100)
            {
                int birthYear = DateTime.Now.Year - age;
                BirthYearText.Text = $"Год рождения: {birthYear}";
            }
            else
            {
                BirthYearText.Text = "";
            }
        }

        private void AgreeCheck_Checked(object sender, RoutedEventArgs e)
        {
            RegisterButton.IsEnabled = true;
        }

        private void AgreeCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            RegisterButton.IsEnabled = false;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            UpdatePasswordStrength();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!HasUnsavedData())
                return; 

            MessageBoxResult answer = MessageBox.Show(
                "В форме есть несохранённые данные. Вы действительно хотите закрыть окно?",
                "Подтверждение закрытия",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (answer != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}