using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PizzaFinalApp.RegistrationPages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationAdress.xaml
    /// </summary>
    public partial class RegistrationAdress : Page
    {
        public RegistrationAdress()
        {
            InitializeComponent();
            Street.Text = RegistrationUser.Street;
            Floor.Text = RegistrationUser.Floor.ToString();
            Porch.Text = RegistrationUser.Porch.ToString();
            Room.Text = RegistrationUser.Room.ToString();
            House.Text = RegistrationUser.Building.ToString();


        }

        private void Back(object sender, RoutedEventArgs e)
        {
            Navigator.Navigate(new RegistrationPhone());
        }

        private void FinishRegistration(object sender, RoutedEventArgs e)
        {
            var lettersRegex = new Regex(@"[a-zA-Z]");
            if (!string.IsNullOrWhiteSpace(Street.Text) && lettersRegex.IsMatch(Street.Text)
               && !string.IsNullOrWhiteSpace(Floor.Text) && !lettersRegex.IsMatch(Floor.Text)
               && !string.IsNullOrWhiteSpace(Porch.Text) && !lettersRegex.IsMatch(Porch.Text)
               && !string.IsNullOrWhiteSpace(Porch.Text) && !lettersRegex.IsMatch(Room.Text)
               && !string.IsNullOrWhiteSpace(House.Text) && !lettersRegex.IsMatch(House.Text))
            {
                RegistrationUser.Street = Street.Text;
                RegistrationUser.Floor = Int32.Parse(Floor.Text);
                RegistrationUser.Porch = Int32.Parse(Porch.Text);
                RegistrationUser.Room = Int32.Parse(Room.Text);
                RegistrationUser.Building = Int32.Parse(House.Text);
                Navigator.Navigate(new RegistrationFinal());
            }
            else
            {
                MessageBox.Show("Проверьте правильность введенных данных", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
