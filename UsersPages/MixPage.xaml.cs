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
    /// Логика взаимодействия для MixPage.xaml
    /// </summary>
    public partial class MixPage : Page
    {
        PizzaContext context = new PizzaContext();
        List<Ingredient> pizzaIngredients = new List<Ingredient>();
        public MixPage()
        {
            InitializeComponent();
            IngredientListView.ItemsSource = context.Ingredients.ToList();
            CalculateAll();
        }

        private void AddDish(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(pizzaIngredients.Count.ToString());
        }

        private void OpenMenu(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeSize(object sender, RoutedEventArgs e)
        {
            CalculateAll();
        }

        public void CalculateAll()
        {
            int price = 0;
            int weight = 0;
            if(Size23cm.IsChecked == true)
            {
                price += 200;
                weight += 150;
            }
            if (Size30cm.IsChecked == true)
            {
                price += 400;
                weight += 300;
            }
            if (Size40cm.IsChecked == true)
            {
                price += 600;
                weight += 450;
            }
            foreach(Ingredient ingredient in pizzaIngredients)
            {
                price += ingredient.Price;
                weight += ingredient.Weight;
            }
            DishPrice.Content = price;
            DishWeight.Content = weight;

        }

        private void FindIngredient(object sender, TextChangedEventArgs e)
        {
            if (IngredientFilter.Text == "")
            {
                IngredientListView.ItemsSource = context.Ingredients.ToList();
            }
            else
            {
                var ingredients = context.Ingredients.ToList();
                IngredientListView.ItemsSource = ingredients.Where(u => u.Name.ToLower().Contains(IngredientFilter.Text.ToLower()));
            }
        }

        private void AddToPizza(object sender, RoutedEventArgs e)
        {
            Ingredient ingr = (sender as Button).DataContext as Ingredient;
            var parent = (sender as Button).Parent as Panel;
            var amountTextBox = parent.Children[0] as CheckBox;
            if (amountTextBox.IsChecked == false)
            {
                amountTextBox.IsChecked = true;
                pizzaIngredients.Add(ingr);
                //editableDish.Ingredients.Remove(ingr);
            }
            CalculateAll();
        }

        private void RemoveFromPizza(object sender, RoutedEventArgs e)
        {
            Ingredient ingr = (sender as Button).DataContext as Ingredient;
            var parent = (sender as Button).Parent as Panel;
            var amountTextBox = parent.Children[0] as CheckBox; ;
            amountTextBox.IsChecked = false;
            pizzaIngredients.Remove(ingr);
            CalculateAll();
        }
    }
}
