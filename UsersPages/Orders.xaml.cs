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

namespace PizzaFinalApp.UsersPages
{
    /// <summary>
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : Page
    {
        User currentUser;
        List<Dish> orderList;
        public Orders(User user,List<Dish> dishes)
        {
            InitializeComponent();
            currentUser = user;
            orderList = dishes;
            CalculateAllPrice();
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
    }
}
