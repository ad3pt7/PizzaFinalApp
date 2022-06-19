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
    /// Логика взаимодействия для RegistrationEmail.xaml
    /// </summary>
    public partial class RegistrationEmail : Page
    {
        public RegistrationEmail()
        {
            InitializeComponent();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            Navigator.Navigate(new Login());
        }

        private void NextStep(object sender, RoutedEventArgs e)
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$"); ;
            if(!string.IsNullOrEmpty(UserMail.Text) && emailRegex.IsMatch(UserMail.Text))
            {
                RegistrationUser.Login = UserMail.Text;
                RegistrationUser.Email = UserMail.Text;
                Navigator.Navigate(new RegistrationName());
            }
            else
            {
                MessageBox.Show("Проверьте правильность введенных данных","Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
            }

        }
    }
}
