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
    /// Логика взаимодействия для RegistrationFinal.xaml
    /// </summary>
    public partial class RegistrationFinal : Page
    {
        PizzaContext context = new PizzaContext();
        public RegistrationFinal()
        {
            InitializeComponent();
            CreateUserPassword();
        }

        private void EndRegistration(object sender, RoutedEventArgs e)
        {
            User newUser = new User
            {
                ID = context.Users.OrderByDescending(u => u.ID).FirstOrDefault().ID+1,
                Login = RegistrationUser.Login,
                Email = RegistrationUser.Email,
                FirstName = RegistrationUser.FirstName,
                LastName = RegistrationUser.LastName,
                MiddleName = RegistrationUser.MiddleName,
                Phone = RegistrationUser.Phone,
                Floor = RegistrationUser.Floor,
                Street = RegistrationUser.Street,
                Porch = RegistrationUser.Porch,
                Building = RegistrationUser.Building,
                Password = RegistrationUser.Password,
                Room = RegistrationUser.Room,
                RightGroupId = 2
            };
            context.Users.Add(newUser);
            context.SaveChanges();
            Navigator.Navigate(new Login());
        }

        public void CreateUserPassword()
        {
            var password = string.Empty;
            var random = new Random();

            for (int i = 0; i < 10; i++)
            {
                var nextPart = (char)random.Next(0x21, 0x7E);
                if (nextPart != 0x2C)
                {
                    password += nextPart;
                }
                else
                {
                    i--;
                    continue;
                }
            }
            RegistrationUser.Password = password;
            UserPassword.Content = password;
        }

    }
}
