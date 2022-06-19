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
    /// Логика взаимодействия для RegistrationName.xaml
    /// </summary>
    public partial class RegistrationName : Page
    {
        public RegistrationName()
        {
            InitializeComponent();
            UserFirstName.Text = RegistrationUser.FirstName;
            UserLastName.Text = RegistrationUser.LastName;
            UserMiddleName.Text = RegistrationUser.MiddleName;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            Navigator.Navigate(new RegistrationEmail());
        }

        private void NextStep(object sender, RoutedEventArgs e)
        {
            var lettersRegex = new Regex(@"[\p{Ll}\p{Lt}]+");
            if (!string.IsNullOrWhiteSpace(UserFirstName.Text) && lettersRegex.IsMatch(UserFirstName.Text)
                && !string.IsNullOrWhiteSpace(UserLastName.Text) && lettersRegex.IsMatch(UserLastName.Text)
                && !string.IsNullOrWhiteSpace(UserFirstName.Text) && lettersRegex.IsMatch(UserMiddleName.Text))
            {
                RegistrationUser.FirstName = UserFirstName.Text;
                RegistrationUser.LastName = UserLastName.Text;
                RegistrationUser.MiddleName = UserMiddleName.Text;
                Navigator.Navigate(new RegistrationPhone());
            }
            else
            {
                MessageBox.Show("Проверьте правильность введенных данных", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
