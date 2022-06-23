using LiveCharts;
using LiveCharts.Wpf;
using PizzaFinalApp.RegistrationPages;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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

namespace PizzaFinalApp.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Page
    {
        PizzaContext context = new PizzaContext();
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public AdminPanel()
        {
            InitializeComponent();
            UsersList.ItemsSource = context.Users.ToList();
            PizzasListView.ItemsSource = context.Dishes.ToList();
            IngridientsList.ItemsSource = context.Ingredients.ToList();
            OrdersList.ItemsSource = context.Orders.ToList();
            MakeGraphic();
        }

        private void EditPizza(object sender, RoutedEventArgs e)
        {
            Navigator.Navigate(new DishEdit((sender as System.Windows.Controls.Button).DataContext as Dish));
        }

        private void DeletePizza(object sender, RoutedEventArgs e)
        {
            if (PizzasListView.SelectedIndex != -1)
            {
                var selectedPizza = PizzasListView.SelectedItem as Dish;
                var removeList = context.DishIngredients.Where(ing => ing.DishId == selectedPizza.Id).ToList();
                //MessageBox.Show(removeList.Count.ToString());
                foreach (DishIngredient ingredient in removeList)
                {
                    context.DishIngredients.Remove(ingredient);
                }
                //
                context.Dishes.Remove(selectedPizza);
                context.SaveChanges();
                Navigator.Navigate(new AdminPanel());
            }
            else
            {
                System.Windows.MessageBox.Show("Пожалуйста выберите блюдо", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            Navigator.Navigate(new Login());
        }

        private void AddUser(object sender, RoutedEventArgs e)
        {
            Navigator.Navigate(new UserEdit(null));
        }

        private void EditUser(object sender, RoutedEventArgs e)
        {
            if (UsersList.SelectedIndex != -1)
            {
                Navigator.Navigate(new UserEdit(UsersList.SelectedItem as User));
            }
            else
            {
                System.Windows.MessageBox.Show("Пожалуйста выберите пользователя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            if (UsersList.SelectedIndex != -1)
            {
                var selectedUser = UsersList.SelectedItem as User;
                context.Users.Remove(selectedUser);
                context.SaveChanges();
                Navigator.Navigate(new AdminPanel());
            }
            else
            {
                System.Windows.MessageBox.Show("Пожалуйста выберите пользователя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddPizza(object sender, RoutedEventArgs e)
        {

            Navigator.Navigate(new DishEdit(null));
        }

        private void SearchCertainUsers(object sender, TextChangedEventArgs e)
        {
            if (UsersFilter.Text == "")
            {
                UsersList.ItemsSource = context.Users.ToList();
            }
            else
            {
                var users = context.Users.ToList();
                UsersList.ItemsSource = users.Where(u => u.FullName.ToLower().Contains(UsersFilter.Text.ToLower()));
            }
        }

        private void SearchCertainPizza(object sender, TextChangedEventArgs e)
        {
            if (PizzaFilter.Text == "")
            {
                PizzasListView.ItemsSource = context.Dishes.ToList();
            }
            else
            {
                var pizza = context.Dishes.ToList();
                PizzasListView.ItemsSource = pizza.Where(u => u.Name.ToLower().Contains(PizzaFilter.Text.ToLower()));
            }
        }

        private void EditIngredient(object sender, RoutedEventArgs e)
        {
            if (IngridientsList.SelectedIndex != -1)
            {
                Navigator.Navigate(new IngredientEdit(IngridientsList.SelectedItem as Ingredient));
            }
            else
            {
                System.Windows.MessageBox.Show("Пожалуйста выберите ингридиент", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddIngredient(object sender, RoutedEventArgs e)
        {
            Navigator.Navigate(new IngredientEdit(null));
        }

        private void DeleteIngredient(object sender, RoutedEventArgs e)
        {
            if (IngridientsList.SelectedIndex != -1)
            {
                var selectedIngredient = IngridientsList.SelectedItem as Ingredient;
                context.Ingredients.Remove(selectedIngredient);
                var removeList = context.DishIngredients.Where(ing => ing.IngredientId == selectedIngredient.Id).ToList();
                //MessageBox.Show(removeList.Count.ToString());
                foreach (DishIngredient ingredient in removeList)
                {
                    context.DishIngredients.Remove(ingredient);
                }
                context.SaveChanges();
                Navigator.Navigate(new AdminPanel());
            }
            else
            {
                System.Windows.MessageBox.Show("Пожалуйста выберите ингридиент", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchCertainIngredient(object sender, TextChangedEventArgs e)
        {
            if (IngredientFilter.Text == "")
            {
                IngridientsList.ItemsSource = context.Ingredients.ToList();
            }
            else
            {
                var ingredients = context.Ingredients.ToList();
                IngridientsList.ItemsSource = ingredients.Where(u => u.Name.ToLower().Contains(IngredientFilter.Text.ToLower()));
            }
        }

        private void ExportIngredients(object sender, RoutedEventArgs e)
        {
            if (File.Exists("Ingredients.svg"))
            {
                File.Delete("Ingredients.svg");
            }
            if (context.Ingredients.Count() > 0)
            {
                StreamWriter writer = new StreamWriter("Ingredients.svg");
                foreach (Ingredient ingredient in context.Ingredients.ToList())
                {
                    var data = $"{ingredient.Id}|{ingredient.Name}|{ingredient.Price}|{ingredient.Weight}";
                    writer.WriteLine(data);
                }
                writer.Close();
                System.Windows.MessageBox.Show("Записи успешно экспортированы");

            }
            else
            {
                System.Windows.MessageBox.Show("Нет данных для экспорта", "Уведомление");
            }
        }

        private void ImportIngredient(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = "C:\\";
            fileDialog.Filter = "SVG (*.svg)|*.svg";
            fileDialog.ShowDialog();
            if (fileDialog.FileName != "")
            {
                StreamReader reader = new StreamReader(fileDialog.FileName);
                while (!reader.EndOfStream)
                {
                    Ingredient ingredient = new Ingredient();

                    string[] data = reader.ReadLine().Split('|');
                    ingredient.Id = Convert.ToInt32(data[0]);
                    ingredient.Name = data[1];
                    ingredient.Price = Convert.ToInt32(data[2]);
                    ingredient.Weight = Convert.ToInt32(data[3]);

                    context.Ingredients.AddOrUpdate(ingredient);
                }
                context.SaveChanges();
                IngridientsList.ItemsSource = context.Ingredients.ToList();
                System.Windows.MessageBox.Show("Данные импортированы");

            }
            else
            {
                System.Windows.MessageBox.Show("Файл для импорта не выбран");
            }
        }

        private void ExportUsers(object sender, RoutedEventArgs e)
        {
            if (File.Exists("Users.svg"))
            {
                File.Delete("Users.svg");
            }
            if (context.Users.Count() > 0)
            {
                StreamWriter writer = new StreamWriter("Users.svg");
                foreach (User user in context.Users.ToList())
                {
                    var data = $"{user.ID} {user.Login} {user.Password} {user.Email} {user.FirstName} {user.LastName} {user.MiddleName}" +
                        $" {user.Phone} {user.Street} {user.Building} {user.Room} {user.Porch} {user.Floor} {user.RightGroupId}";
                    writer.WriteLine(data);
                }
                writer.Close();
                System.Windows.MessageBox.Show("Записи успешно экспортированы");

            }
            else
            {
                System.Windows.MessageBox.Show("Нет данных для экспорта", "Уведомление");
            }
        }

        private void ImportUsers(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = "C:\\";
            fileDialog.Filter = "SVG (*.svg)|*.svg";
            fileDialog.ShowDialog();
            if (fileDialog.FileName != "")
            {
                StreamReader reader = new StreamReader(fileDialog.FileName);
                while (!reader.EndOfStream)
                {
                    User user = new User();

                    string[] data = reader.ReadLine().Split(' ');
                    user.ID = int.Parse(data[0]);
                    user.Login = data[1];
                    user.Password = data[2];
                    user.Email = data[3];
                    user.FirstName = data[4];
                    user.LastName = data[5];
                    user.MiddleName = data[6];
                    user.Phone = data[7];
                    user.Street = data[8];
                    user.Building = int.Parse(data[9]);
                    user.Room = int.Parse(data[10]);
                    user.Porch = int.Parse(data[11]);
                    user.Floor = int.Parse(data[12]);
                    user.RightGroupId = int.Parse(data[13]);
                    context.Users.AddOrUpdate(user);
                }
                context.SaveChanges();
                UsersList.ItemsSource = context.Users.ToList();
                System.Windows.MessageBox.Show("Данные импортированы");

            }
            else
            {
                System.Windows.MessageBox.Show("Файл для импорта не выбран");
            }
        }


        private void ImportDishes(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = "C:\\";
            fileDialog.Filter = "SVG (*.svg)|*.svg";
            fileDialog.ShowDialog();
            if (fileDialog.FileName != "")
            {
                StreamReader reader = new StreamReader(fileDialog.FileName);
                while (!reader.EndOfStream)
                {
                    Dish dish = new Dish();

                    string[] data = reader.ReadLine().Split(';');
                    dish.Id = Convert.ToInt32(data[0]);
                    dish.Name = data[1];
                    dish.Image = data[2];
                    dish.Description = data[3];

                    context.Dishes.AddOrUpdate(dish);
                }
                context.SaveChanges();
                PizzasListView.ItemsSource = context.Dishes.ToList(); ;
                System.Windows.MessageBox.Show("Данные импортированы");

            }
            else
            {
                System.Windows.MessageBox.Show("Файл для импорта не выбран");
            }
        }

        private void ExportDishes(object sender, RoutedEventArgs e)
        {
            if (File.Exists("Dishes.svg"))
            {
                File.Delete("Dishes.svg");
            }
            if (context.Users.Count() > 0)
            {
                StreamWriter writer = new StreamWriter("Dishes.svg");
                foreach (Dish dish in context.Dishes.ToList())
                {
                    var data = $"{dish.Id};{dish.Name};{dish.Image};{dish.Description}";
                    writer.WriteLine(data);
                }
                writer.Close();
                System.Windows.MessageBox.Show("Записи успешно экспортированы");

            }
            else
            {
                System.Windows.MessageBox.Show("Нет данных для экспорта", "Уведомление");
            }
        }

        private void EditOrderStatus(object sender, RoutedEventArgs e)
        {
            Order order = (sender as System.Windows.Controls.Button).DataContext as Order;
            if (order.StatusId != context.OrderStatuses.OrderByDescending(os => os.Id).FirstOrDefault().Id)
            {
                order.StatusId++;
            }
            else
            {
                System.Windows.MessageBox.Show("Заказ имеет окончательный статус", "Уведомление", MessageBoxButton.OK);
            }
            context.Orders.AddOrUpdate(order);
            context.SaveChanges();
            OrdersList.ItemsSource = context.Orders.ToList();
        }

        private void SearchCertainOrder(object sender, TextChangedEventArgs e)
        {

            if (OrdersFilter.Text == "")
            {
                OrdersList.ItemsSource = context.Orders.ToList();
            }
            else
            {
                var orders = context.Orders.ToList();
                OrdersList.ItemsSource = orders.Where(u => u.FullName.ToLower().Contains(OrdersFilter.Text.ToLower()));
            }
        }

        public void MakeGraphic()
        {
            DataContext = this;
            //SeriesCollection SeriesCollection;
            //string[] Labels;
            //Func<double, string> YFormatter;

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Продажи в день",
                    Values = new ChartValues<int>(context.Orders.Select(o => o.OrderDishes.Select(od => od.Amount).Sum()))
                }
            };
            var labels = context.Orders.ToArray().Select(o => o.Date.ToString("dd-mm-yyyy")).ToArray();
            Labels = labels;
            YFormatter = value => value.ToString("N");

        }
    
    }
}
