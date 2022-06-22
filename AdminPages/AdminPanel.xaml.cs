using PizzaFinalApp.RegistrationPages;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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

namespace PizzaFinalApp.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Page
    {
        PizzaContext context = new PizzaContext();
        public AdminPanel()
        {
            InitializeComponent();
            UsersList.ItemsSource = context.Users.ToList();
            PizzasListView.ItemsSource = context.Dishes.ToList();
            IngridientsList.ItemsSource = context.Ingredients.ToList();
        }

        private void EditPizza(object sender, RoutedEventArgs e)
        {
            Navigator.Navigate(new DishEdit((sender as Button).DataContext as Dish));
        }

        private void DeletePizza(object sender, RoutedEventArgs e)
        {
            if (PizzasListView.SelectedIndex != -1)
            {
                var selectedPizza = PizzasListView.SelectedItem as Dish;
                context.Dishes.Remove(selectedPizza);
                context.SaveChanges();
                Navigator.Navigate(new AdminPanel());
            }
            else
            {
                MessageBox.Show("Пожалуйста выберите блюдо", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("Пожалуйста выберите пользователя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("Пожалуйста выберите пользователя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddPizza(object sender, RoutedEventArgs e)
        {
            
            Navigator.Navigate(new DishEdit(null));
        }

        private void SearchCertainUsers(object sender, TextChangedEventArgs e)
        {
           // UsersList.Items.Clear();
            //MessageBox.Show(context.Users.ToList()[0].FullName);
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
                MessageBox.Show("Пожалуйста выберите ингридиент", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show(removeList.Count.ToString());
                foreach(DishIngredient ingredient in removeList)
                {
                    context.DishIngredients.Remove(ingredient);
                }
                context.SaveChanges();
                Navigator.Navigate(new AdminPanel());
            }
            else
            {
                MessageBox.Show("Пожалуйста выберите ингридиент", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
    }
}
