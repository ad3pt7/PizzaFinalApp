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
    /// Логика взаимодействия для RegistrationPhone.xaml
    /// </summary>
    public partial class RegistrationPhone : Page
    {
        public RegistrationPhone()
        {
            InitializeComponent();
            UserPhone.Text = RegistrationUser.Phone;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            Navigator.Navigate(new RegistrationName());
        }

        private void NextStep(object sender, RoutedEventArgs e)
        {
            var phoneRegex = new Regex(@"^((\+7|8)+([0-9]){10})$");
            if(!string.IsNullOrWhiteSpace(UserPhone.Text) && phoneRegex.IsMatch(UserPhone.Text))
            {
                RegistrationUser.Phone = UserPhone.Text;
                Navigator.Navigate(new RegistrationAdress());
            }
            else
            {
                MessageBox.Show("Проверьте правильность введенных данных", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
