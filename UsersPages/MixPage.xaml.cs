using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
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
        User currentUser = null;
        List<Ingredient> pizzaIngredients = new List<Ingredient>();
        public MixPage(User user)
        {
            InitializeComponent();
            currentUser = user;
            IngredientListView.ItemsSource = context.Ingredients.ToList();
            CalculateAll();
        }

        private void AddDish(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(pizzaIngredients.Count.ToString());
            Dish dishX = new Dish();
            dishX.Id = context.Dishes.OrderByDescending(d => d.Id).FirstOrDefault().Id + 1;
            dishX.Name = "Микс Х";
            dishX.Image = "../Images/icon.png";
            dishX.Description = "Микс X позволяет созадвать собственные пиццы";
            

            int activeSize = 0;
            if (Size23cm.IsChecked == true) activeSize = 1;
            else if (Size30cm.IsChecked == true) activeSize = 2;
            else if (Size40cm.IsChecked == true) activeSize = 3;

            Size23cm.IsChecked = true;
            Size size23 = new Size();
            size23.DishId = dishX.Id;
            size23.Size1 = 23;
            CalculateAll();
            size23.Price = Convert.ToInt32(DishPrice.Content);
            size23.Weight = Convert.ToInt32(DishWeight.Content);

            Size30cm.IsChecked = true;
            Size size30 = new Size();
            size30.DishId = dishX.Id;
            size30.Size1 = 30;
            CalculateAll();
            size30.Price = Convert.ToInt32(DishPrice.Content);
            size30.Weight = Convert.ToInt32(DishWeight.Content);

            Size40cm.IsChecked = true;
            Size size40 = new Size();
            size40.DishId = dishX.Id;
            size40.Size1 = 30;
            CalculateAll();
            size40.Price = Convert.ToInt32(DishPrice.Content);
            size40.Weight = Convert.ToInt32(DishWeight.Content);

            if (activeSize == 1) Size23cm.IsChecked = true;
            else if (activeSize == 2) Size30cm.IsChecked = true;
            else if (activeSize == 3) Size40cm.IsChecked = true;

            context.Dishes.Add(dishX);
            context.Sizes.Add(size23);
            context.Sizes.Add(size30);
            context.Sizes.Add(size40);
            foreach(Ingredient ingredient in pizzaIngredients)
            {
                DishIngredient dishIngredient = new DishIngredient();
                dishIngredient.DishId = dishX.Id;
                dishIngredient.IngredientId = ingredient.Id;
                context.DishIngredients.Add(dishIngredient);
            }
           
            context.SaveChanges();

           
            MessageBox.Show("Созданная вами пицца добавлена в каталог");
            Navigator.Navigate(new MainCatalog(currentUser));

        }

        private void OpenMenu(object sender, RoutedEventArgs e)
        {
            Navigator.Navigate(new MainCatalog(currentUser));
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
