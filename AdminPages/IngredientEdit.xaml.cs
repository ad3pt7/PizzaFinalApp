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
    /// Логика взаимодействия для IngredientEdit.xaml
    /// </summary>
    public partial class IngredientEdit : Page
    {
        Ingredient editableIngredient = null;
        PizzaContext context = new PizzaContext();
        public IngredientEdit(Ingredient ingredient)
        {
            InitializeComponent();
            if(ingredient != null)
            {
                editableIngredient = ingredient;

                IngredientName.Text = editableIngredient.Name;
                Weight.Text = editableIngredient.Weight.ToString();
                Price.Text = editableIngredient.Price.ToString();
            }
            else
            {
                editableIngredient = new Ingredient();
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            Navigator.Navigate(new AdminPanel());
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            editableIngredient.Name = IngredientName.Text;
            editableIngredient.Weight = Convert.ToInt32(Weight.Text);
            editableIngredient.Price = Convert.ToInt32(Price.Text);

            context.Ingredients.AddOrUpdate(editableIngredient);
            context.SaveChanges();
            Navigator.Navigate(new AdminPanel());
        }
    }
}
