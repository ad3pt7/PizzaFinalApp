using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<Ingredient> Ingredients { get; set; }
        public DishEdit()
        {
            InitializeComponent();
            Ingredients = new ObservableCollection<Ingredient>(context.Ingredients.ToList());


        }

        private void Back(object sender, RoutedEventArgs e)
        {

        }
    }
}
