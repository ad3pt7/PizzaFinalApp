using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
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
    /// Логика взаимодействия для DishEdit.xaml
    /// </summary>
    public partial class DishEdit : Page
    {
        PizzaContext context = new PizzaContext();
        Dish editableDish = new Dish();
        List<Ingredient> pizzaIngredients = new List<Ingredient>();

        //public ObservableCollection<Ingredient> Ingredients { get; set; }
        public DishEdit(Dish dish)
        {
            InitializeComponent();
            List<Ingredient> ingredients = context.Ingredients.ToList();
            if (dish != null)
            {
                editableDish = dish;

                DishName.Text = editableDish.Name;
                Desciption.Text = editableDish.Description;
                Photo.Text = editableDish.Image;

                //Ингредиенты
                List<Ingredient> pizzaIngredient = context.DishIngredients.Where(i => i.DishId == editableDish.Id).Select(d =>d.Ingredient).ToList();
                //MessageBox.Show(pizzaIngredient.Count.ToString());
                foreach (Ingredient ingredient in ingredients)
                {
                    foreach (Ingredient pizzaIngredient1 in pizzaIngredient)
                    {
                        if (pizzaIngredient1.Name.Equals(ingredient.Name))
                        {
                            //pizzaIngredients.Add(ingredient);
                            ingredient.HasInPizza = true;
                        }
                    }
                }
                var size = editableDish.Sizes.ToList()[0];
                //MessageBox.Show(size.Id + " | " + size.Price + " | " + size.Weight + " | " + size.DishId);
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
            Navigator.Navigate(new AdminPanel());
        }

        private void ChoosePhoto(object sender, RoutedEventArgs e)
        {
            var imageOpenFileDialog = new OpenFileDialog();
            imageOpenFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            if (imageOpenFileDialog.ShowDialog() == true)
            {
                Photo.Text = imageOpenFileDialog.FileName;
                //}
                //string[] ew = Photo.Text.Split('\\');
                //var imagePath = Photo.Text;
                //var newImagePath = $"../../Images/{ew.Last()}";
                //var newPath = $"../Images/{ew.Last()}";
                //if (!File.Exists(imagePath))
                //{
                //    if (Photo.Text.Length == 0)
                //    {
                //    }
                //    else
                //    {
                //        MessageBox.Show("Такого изображения не существует");
                //    }
                //}
                //else
                //{
                //    FileInfo file = new FileInfo(imagePath);
                //    file.CopyTo(newImagePath);
            }
        }

        private void RemoveFromPizza(object sender, RoutedEventArgs e)
        {
            Ingredient ingr = (sender as Button).DataContext as Ingredient;
            var parent = (sender as Button).Parent as Panel;
            var amountTextBox = parent.Children[0] as CheckBox; ;
            amountTextBox.IsChecked = false;
            pizzaIngredients.Remove(ingr);
            //editableDish.Ingredients.Remove(ingr);
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
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {

            var lettersRegex = new Regex(@"[\p{Ll}\p{Lt}]+");

            var errors = new StringBuilder();

            var name = DishName.Text;
            if (!lettersRegex.IsMatch(name) || string.IsNullOrWhiteSpace(name))
            {
                errors.AppendLine("Назвние блюда не может быть пустым или содержать цифры");
            }

            var description = Desciption.Text;
            if (!lettersRegex.IsMatch(description) || string.IsNullOrWhiteSpace(description))
            {
                errors.AppendLine("Описание блюда не может быть пустым или содержать цифры");
            }

            var photo = Photo.Text;
            //var dishPhotoPath = "";
            if (!string.IsNullOrWhiteSpace(name))
            {
                if (File.Exists(photo))
                {
                    //string[] photoPath = photo.Split('\\');
                    //var copyImagePath = $"../../Images/{photoPath.Last()}";
                    //var saveDishImage = $"../Images/{photoPath.Last()}";
                    //try
                    //{
                    //    FileInfo file = new FileInfo(photo);
                    //    file.CopyTo(copyImagePath);
                    //}
                    //catch { }
                    //dishPhotoPath = saveDishImage;
                }
                else
                {
                    errors.AppendLine("Изображение найдено");
                }
            }
            else
            {
                errors.AppendLine("Изображение блюда не может быть пустым");
            }

            var price23cm = PriceFor23cm.Text;
            if (lettersRegex.IsMatch(price23cm) || string.IsNullOrWhiteSpace(price23cm))
            {
                errors.AppendLine("Цена за 23см. не может быть пустой или содержать цифры");
            }

            var price30cm = PriceFor30cm.Text;
            if (lettersRegex.IsMatch(price30cm) || string.IsNullOrWhiteSpace(price30cm))
            {
                errors.AppendLine("Цена за 30см. не может быть пустой или содержать цифры");
            }

            var price40cm = PriceFor40cm.Text;
            if (lettersRegex.IsMatch(price40cm) || string.IsNullOrWhiteSpace(price40cm))
            {
                errors.AppendLine("Цена за 40см. не может быть пустой или содержать цифры");
            }

            var weight23cm = WeightFor23cm.Text;
            if (lettersRegex.IsMatch(weight23cm) || string.IsNullOrWhiteSpace(weight23cm))
            {
                errors.AppendLine("Вес за 23.см не может быть пустым или содержать цифры");
            }

            var weight30cm = WeightFor30cm.Text;
            if (lettersRegex.IsMatch(weight30cm) || string.IsNullOrWhiteSpace(weight30cm))
            {
                errors.AppendLine("Вес за 30.см не может быть пустым или содержать цифры");
            }

            var weight40cm = WeightFor40cm.Text;
            if (lettersRegex.IsMatch(weight40cm) || string.IsNullOrWhiteSpace(weight40cm))
            {
                errors.AppendLine("Вес за 40.см не может быть пустым или содержать цифры");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                List<Size> sizes = new List<Size>();
                if (editableDish.Name != null)
                {
                    var count = context.Dishes.Where(d => d.Id == editableDish.Id).FirstOrDefault().Id;
                    //MessageBox.Show(count.ToString());
                    //context.Dishes.Where(d => d.Id == editableDish.Id).FirstOrDefault().Ingredients.Clear();
                    sizes = context.Sizes.Where(s => s.DishId == editableDish.Id).ToList();
                }
                else
                {
                    editableDish = new Dish();
                    var index = context.Dishes.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
                    //(from m in db.Таблица select m.Нужное_поле
                   // MessageBox.Show(index.ToString());
                    editableDish.Id = index;
                    sizes.Add(new Size { Size1 = 23 });
                    sizes.Add(new Size { Size1 = 30 });
                    sizes.Add(new Size { Size1 = 40 });
                }
                //MessageBox.Show(sizes.Count.ToString() + " - "+ sizes[0].DishId.ToString()+  " - " + editableDish.Id.ToString());
                sizes[0].DishId = editableDish.Id;
                sizes[0].Price = Convert.ToInt32(price23cm);
                sizes[0].Weight = Convert.ToInt32(weight23cm);

                sizes[1].DishId = editableDish.Id;
                sizes[1].Price = Convert.ToInt32(price30cm);
                sizes[1].Weight = Convert.ToInt32(weight30cm);

                sizes[2].DishId = editableDish.Id;
                sizes[2].Price = Convert.ToInt32(price40cm);
                sizes[2].Weight = Convert.ToInt32(weight40cm);

                editableDish.Name = name;
                editableDish.Description = description;
                editableDish.Image = photo;

                foreach (Size size in sizes)
                {
                    context.Sizes.AddOrUpdate(size);
                }
                foreach(Ingredient ingredient in pizzaIngredients)
                {
                    context.DishIngredients.Add(new DishIngredient { IngredientId = ingredient.Id, DishId = editableDish.Id });
                }
                context.Dishes.AddOrUpdate(editableDish);
                context.SaveChanges();
                Navigator.Navigate(new AdminPanel());
               

            }
        }
    }
}
