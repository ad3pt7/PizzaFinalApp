using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Логика взаимодействия для DishEdit.xaml
    /// </summary>
    public partial class DishEdit : Page
    {
        PizzaContext context = new PizzaContext();
        Dish editableDish = null;
        List<Ingredient> pizzaIngredients = new List<Ingredient>();
        
        //public ObservableCollection<Ingredient> Ingredients { get; set; }
        public DishEdit(Dish dish)
        {
            InitializeComponent();
            List<Ingredient> ingredients = context.Ingredients.ToList();
            if(dish != null)
            {
                editableDish = dish;

                DishName.Text = editableDish.Name;
                Desciption.Text = editableDish.Description;
                Photo.Text = editableDish.Image;

                //Ингредиенты
                List<Ingredient> pizzaIngredient = editableDish.Ingredients.ToList();
                MessageBox.Show(pizzaIngredient.Count.ToString());
                foreach(Ingredient ingredient in ingredients)
                {
                    foreach(Ingredient pizzaIngredient1 in pizzaIngredient)
                    {
                        if(pizzaIngredient1.Name.Equals(ingredient.Name))
                        {
                                // MessageBox.Show(ingredient.Name + " " + pizzaIngredient1.Name);
                            ingredient.HasInPizza = true;

                        }
                    }
                }
              

                //


                PriceFor23cm.Text = editableDish.Sizes.ToList()[0].Price.ToString();
                WeightFor23cm.Text = editableDish.Sizes.ToList()[0].Weight.ToString();
                PriceFor30cm.Text = editableDish.Sizes.ToList()[1].Price.ToString();
                WeightFor30cm.Text = editableDish.Sizes.ToList()[1].Weight.ToString();
                PriceFor40cm.Text = editableDish.Sizes.ToList()[2].Price.ToString();
                WeightFor40cm.Text = editableDish.Sizes.ToList()[2].Weight.ToString();


            }
            IngridientList.ItemsSource = ingredients;
            //Ingredients = new ObservableCollection<Ingredient>(context.Ingredients.ToList());


        }

        private void Back(object sender, RoutedEventArgs e)
        {

        }

        private void ChoosePhoto(object sender, RoutedEventArgs e)
        {
            var imageOpenFileDialog = new OpenFileDialog();
            imageOpenFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            if (imageOpenFileDialog.ShowDialog() == true)
            {
                Photo.Text = imageOpenFileDialog.FileName;
            }
            string[] ew = Photo.Text.Split('\\');
            var imagePath = Photo.Text;
            var newImagePath = $"../../Images/{ew.Last()}";
            var newPath = $"../Images/{ew.Last()}";
            if (!File.Exists(imagePath))
            {
                if (Photo.Text.Length == 0)
                {
                }
                else
                {
                    MessageBox.Show("Такого изображения не существует");
                }
            }
            else
            {
                FileInfo file = new FileInfo(imagePath);
                file.CopyTo(newImagePath);
            }
        }

        private void RemoveFromPizza(object sender, RoutedEventArgs e)
        {
            Ingredient ingr = (sender as Button).DataContext as Ingredient;
            var parent = (sender as Button).Parent as Panel;
            var amountTextBox = parent.Children[0] as CheckBox; ;
            amountTextBox.IsChecked = false;
            pizzaIngredients.Remove(ingr);
        }

        private void AddToPizza(object sender, RoutedEventArgs e)
        {
            Ingredient ingr = (sender as Button).DataContext as Ingredient;
            var parent = (sender as Button).Parent as Panel;
            var amountTextBox = parent.Children[0] as CheckBox;
            if(amountTextBox.IsChecked == false)
            {
                amountTextBox.IsChecked = true;
                pizzaIngredients.Add(ingr);

            }
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            foreach(Ingredient ingr in pizzaIngredients)
            {
                MessageBox.Show(ingr.Name);
            }
        }
    }
}
