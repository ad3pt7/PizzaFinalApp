using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Diagnostics;
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

namespace PizzaFinalApp.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для UserEdit.xaml
    /// </summary>
    public partial class UserEdit : Page
    {
        User editableUser = null;
        PizzaContext context = new PizzaContext();
        public UserEdit(User user)
        {
            InitializeComponent();
            if(user != null)
            {
                editableUser = user;

                FirstName.Text = editableUser.FirstName;
                LastName.Text = editableUser.LastName;
                MiddleName.Text = editableUser.MiddleName;
                Phone.Text = editableUser.Phone;
                Email.Text = editableUser.Email;
                Street.Text = editableUser.Street;
                Building.Text = editableUser.Building.ToString();
                Room.Text = editableUser.Room.ToString();
                Porch.Text = editableUser.Porch.ToString();
                Floor.Text = editableUser.Floor.ToString();

            }
            else
            {
                editableUser = new User();
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            Navigator.Navigate(new AdminPanel());
        }

        private void SaveUserData(object sender, RoutedEventArgs e)
        {
            var lettersRegex = new Regex(@"[\p{Ll}\p{Lt}]+");
            var phoneRegex = new Regex(@"^((\+7|8)+([0-9]){10})$");
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            var errors = new StringBuilder();

            var firstName = FirstName.Text;
            if(string.IsNullOrWhiteSpace(firstName) || !lettersRegex.IsMatch(firstName))
            {
                errors.AppendLine("Имя не может быть пустым или содержать цифры");
            }

            var lastName = LastName.Text;
            if (string.IsNullOrWhiteSpace(lastName) || !lettersRegex.IsMatch(lastName))
            {
                errors.AppendLine("Фамилия не может быть пустым или содержать цифры");
            }

            var middleName = MiddleName.Text;
            if (string.IsNullOrWhiteSpace(middleName) || !lettersRegex.IsMatch(middleName))
            {
                errors.AppendLine("Отчество не может быть пустым или содержать цифры");
            }

            var email = Email.Text;
            if (string.IsNullOrWhiteSpace(email) || !emailRegex.IsMatch(email))
            {
                errors.AppendLine("Почта не может быть пустой и должна быть в формате x@x.x");
            }

            var phone = Phone.Text;
            if (string.IsNullOrWhiteSpace(phone) || !phoneRegex.IsMatch(phone))
            {
                errors.AppendLine("Телефон не может быть пустым и должен быть в правильном формате");
            }

            var street = Street.Text;
            if (string.IsNullOrWhiteSpace(street) || !lettersRegex.IsMatch(street))
            {
                errors.AppendLine("Улица не может быть пустой");
            }

            var building = Building.Text;
            if(!int.TryParse(building, out _))
            {
                errors.AppendLine("Дом не может быть пустым или содержать символы");
            }

            var room = Room.Text;
            if (!int.TryParse(room, out _))
            {
                errors.AppendLine("Квартира не может быть пустой или содержать буквы");
            }

            var porch = Porch.Text;
            if (!int.TryParse(porch, out _))
            {
                errors.AppendLine("Подъезд не может быть пустым и содержать буквы");
            }

            var floor = Floor.Text;
            if(!int.TryParse(floor, out _))
            {
                errors.AppendLine("Этаж не может быть пустым или содержать буквы");
            }

            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(),"Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            else
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
                if(editableUser.Password is null)
                {
                    editableUser.Password = password;
                }
                editableUser.FirstName = firstName;
                editableUser.LastName = lastName;
                editableUser.MiddleName = middleName;
                editableUser.Phone = phone;
                editableUser.Email = email;
                editableUser.Login = email;
                editableUser.Street = street;
                editableUser.Building = int.Parse(building);
                editableUser.Room = int.Parse(room);
                editableUser.Floor = int.Parse(floor);
                editableUser.Porch = int.Parse(porch);
                editableUser.RightGroupId = 2;
                context.Users.AddOrUpdate(editableUser);
                context.SaveChanges();
                
                Navigator.Navigate(new AdminPanel());
            }
        }
    }
}
