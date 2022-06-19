using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        PizzaContext context = new PizzaContext();
        public Login()
        {
            InitializeComponent();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void LogIn(object sender, RoutedEventArgs e)
        {
            var user = context.Users.Where(u => u.Login == UserLogin.Text && u.Password == UserPassword.Password).FirstOrDefault();
            if(user != null)
            {
                Navigator.Navigate(new UsersPages.MainCatalog(user));
            }
            else
            {
                MessageBox.Show("Проверьте правильность введенных данных","Ошибка авторизации",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void RegIn(object sender, RoutedEventArgs e)
        {
            Navigator.Navigate(new RegistrationEmail());
        }
    }
}
