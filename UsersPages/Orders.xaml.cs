using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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

namespace PizzaFinalApp.UsersPages
{
    /// <summary>
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : Page
    {
        User currentUser;
        List<Dish> orderList;
        PizzaContext context = new PizzaContext();
        public Orders(User user,List<Dish> dishes)
        {
            InitializeComponent();
            currentUser = user;
            orderList = dishes;
            CalculateAllPrice();
            LoadUserInfo(currentUser);
        }

        private void OpenMenu(object sender, RoutedEventArgs e)
        {
            Navigator.Navigate(new MainCatalog(currentUser));
        }

        public void CalculateAllPrice()
        {
            float orderSum = 0;
            foreach (Dish dish in orderList)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (dish.Sizes.ToList()[i].DishAmount != 0)
                    {
                        orderSum += dish.Sizes.ToList()[i].DishAmount * dish.Sizes.ToList()[i].Price;
                        //order.AppendLine(dish.Name + " - " + dish.Sizes.ToList()[i].Size1 + " -- " + dish.Sizes.ToList()[i].DishAmount);
                    }
                }
            }
            OrderSum.Content = orderSum.ToString();

            double discount = 0;
            if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
            {
                discount = orderSum * 0.15f;
                discount = Math.Round(discount, 2);
                DiscountSum.Content = discount + "р. (15%)";
            }
            else
            {
                DiscountSum.Content = discount + "р. (0%)";
            }
            TotalSum.Content = (orderSum - discount);
        }

        private void MakeOrder(object sender, RoutedEventArgs e)
        {
            SaveUserInfo(currentUser);
            Order order = new Order
            {
                StatusId = 1,
                UserId = currentUser.ID,
                Date = DateTime.Now
            };
            context.Orders.Add(order);
            foreach(Dish dish in orderList)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (dish.Sizes.ToList()[i].DishAmount != 0)
                    {
                        OrderDish orderDish = new OrderDish
                        {
                            DishId = dish.Id,
                            Amount = dish.Sizes.ToList()[i].DishAmount,
                            SelectedDishSize = dish.Sizes.ToList()[i].Size1,
                            OrderId = order.Id
                        };
                        context.OrderDishes.Add(orderDish);
                        //order.AppendLine(dish.Name + " - " + dish.Sizes.ToList()[i].Size1 + " -- " + dish.Sizes.ToList()[i].DishAmount);
                    }
                }
            }
            context.SaveChanges();
            Navigator.Navigate(new ChequePage(currentUser, orderList));
        }

        public void LoadUserInfo(User user)
        {
            FirstName.Text = user.FirstName;
            LastName.Text = user.LastName;
            MiddleName.Text = user.MiddleName;
            UserPhone.Text = user.Phone;
            Email.Text = user.Email;
            Street.Text = user.Street;
            Building.Text = user.Building.ToString();
            Room.Text = user.Room.ToString();
            Porch.Text = user.Porch.ToString();
            Floor.Text = user.Floor.ToString();
        }

        public void SaveUserInfo(User user)
        {
            var lettersRegex = new Regex(@"[\p{Ll}\p{Lt}]+");
            var phoneRegex = new Regex(@"^((\+7|8)+([0-9]){10})$");
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            var errors = new StringBuilder();

            var firstName = FirstName.Text;
            if (string.IsNullOrWhiteSpace(firstName) || !lettersRegex.IsMatch(firstName))
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

            var phone = UserPhone.Text;
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
            if (!int.TryParse(building, out _))
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
            if (!int.TryParse(floor, out _))
            {
                errors.AppendLine("Этаж не может быть пустым или содержать буквы");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                user.FirstName = firstName;
                user.LastName = lastName;
                user.MiddleName = middleName;
                user.Phone = phone;
                user.Email = email;
                user.Login = email;
                user.Street = street;
                user.Building = int.Parse(building);
                user.Room = int.Parse(room);
                user.Floor = int.Parse(floor);
                user.Porch = int.Parse(porch);
                user.RightGroupId = 2;
                context.Users.AddOrUpdate(user);
                context.SaveChanges();
            }
        }
    }
}
