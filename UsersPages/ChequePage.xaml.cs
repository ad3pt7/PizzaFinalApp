using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PizzaFinalApp.UsersPages
{
    /// <summary>
    /// Логика взаимодействия для ChequePage.xaml
    /// </summary>
    public partial class ChequePage : Page
    {
        User currentUser;
        List<Dish> dishToOrder = new List<Dish>();
        StringBuilder Cheque = new StringBuilder();
        public ChequePage(User user, List<Dish> dishes)
        {
            InitializeComponent();
            currentUser = user;
            dishToOrder = dishes;
            MakeCheque();
        }


        private void OpenMenu(object sender, RoutedEventArgs e)
        {
            Navigator.Navigate(new MainCatalog(currentUser));
        }

        public void MakeCheque()
        {
            
            Cheque.AppendLine("КАССОВЫЙ ЧЕК");
            Cheque.AppendLine("ЦАРЬ ПИЦЦА");
            Cheque.AppendLine("ИНН 589923478904");
            Cheque.AppendLine("");
            Cheque.AppendLine(DateTime.Now.ToString());
            Cheque.AppendLine("Смена №" + new Random().Next(0, 20));
            Cheque.AppendLine("Кассир Жмышенко В.А.");
            Cheque.AppendLine("РН ККТ " + new Random().Next(1000000,2000000));
            Cheque.AppendLine("Пермь");
            Cheque.AppendLine("Место расчетов офис");
            Cheque.AppendLine("Пермь");
            Cheque.AppendLine($"Заказчик: {currentUser.FullName}");
            Cheque.AppendLine($"Адрес доставки: {currentUser.Street} д.{currentUser.Building.ToString()} кв.{currentUser.Room.ToString()} " +
                $"под.{currentUser.Porch.ToString()}  этаж {currentUser.Floor.ToString()}");
            Cheque.AppendLine("");
            Cheque.AppendLine("Состав заказа");
            double totalSum = 0;
            double discount = 0;
            foreach (Dish dish in dishToOrder)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (dish.Sizes.ToList()[i].DishAmount != 0)
                    {
                        totalSum += dish.Sizes.ToList()[i].DishAmount * dish.Sizes.ToList()[i].Price;
                        Cheque.AppendLine($"{dish.Sizes.ToList()[i].DishAmount} x {dish.Name} ({dish.Sizes.ToList()[i].Size1} см.) " +
                            $"{dish.Sizes.ToList()[i].DishAmount * dish.Sizes.ToList()[i].Price} р.");
                        ////order.AppendLine(dish.Name + " - " + dish.Sizes.ToList()[i].Size1 + " -- " + dish.Sizes.ToList()[i].DishAmount);
                    }
                }
            }
            Cheque.AppendLine($"ИТОГО {totalSum} р.");
            if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
            {
                discount = totalSum * 0.15f;
                discount = Math.Round(discount, 2);    
            }
            Cheque.AppendLine($"СКИДКА {discount} р.");
            Cheque.AppendLine($"К ОПЛАТЕ {totalSum - discount} р.");
            Cheque.AppendLine("");
            Cheque.AppendLine($"ОПЛАЧЕНО {totalSum - discount} р.");
            Cheque.AppendLine($"СУММА НДС {Math.Round((totalSum - discount)*0.2f),2} р.");
            Cheque.AppendLine("CАЙТ НДС www.nalog.ru");
            Cheque.AppendLine($"ФД {new Random().Next(10000000,99999999)}");
            Cheque.AppendLine($"ФН {new Random().Next(10000000, 99999999)}");
            Cheque.AppendLine($"ФП {new Random().Next(10000000, 99999999)}");

            //OrderSum.Content = orderSum.ToString();

            //double discount = 0;
            //else
            //{
            //    DiscountSum.Content = discount + "р. (0%)";
            //}
            //TotalSum.Content = (orderSum - discount);
            OrderCheque.Text = Cheque.ToString();
        }

        private void SaveCheque(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Выберите папку для сохранения";
            dialog.ShowDialog();
            if(dialog.SelectedPath != "")
            {
                StreamWriter writer = new StreamWriter(dialog.SelectedPath + @"\" + new Random().Next(100000, 9999999)+".txt");
                writer.WriteLine(Cheque);
                writer.Close();
                System.Windows.MessageBox.Show("Чек успешно сохранен");
            }
            else
            {
                System.Windows.MessageBox.Show("Не удалось сохранить файл");
            }
        }
    }
}
