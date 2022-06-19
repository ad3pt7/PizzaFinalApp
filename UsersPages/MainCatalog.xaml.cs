using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для MainCatalog.xaml
    /// </summary>
    public partial class MainCatalog : Page
    {
        PizzaContext context = new PizzaContext();
        User currentUser;
        public MainCatalog(User user)
        {
            InitializeComponent();
            currentUser = user;
            PizzasListView.ItemsSource = context.Dishes.ToList();

        }

        private void ChangePriceAndWeightTo23cm(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("1");
            SwitchSizeOptionAndUpdateContext(sender, 0);
            Button button = sender as Button;
            button.Style = this.FindResource("WhiteButton") as Style;
        }

        private void ChangePriceAndWeightTo30cm(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("2");
            SwitchSizeOptionAndUpdateContext(sender, 1);
        }

        private void ChangePriceAndWeghtTo40cm(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("3");
            SwitchSizeOptionAndUpdateContext(sender, 2);
        }

        private void DecreaseAmountByOne(object sender, RoutedEventArgs e)
        {
            var pizza = (sender as Button).DataContext as Dish;

            MessageBox.Show(pizza.Name);
            var currentSizeDishAmount = pizza.Sizes.ToList()[pizza.SelectedSizeIndex].DishAmount;
            MessageBox.Show(currentSizeDishAmount.ToString());
            if (currentSizeDishAmount > 0)
            {
                currentSizeDishAmount--;
            }

            var parent = ((sender as Button).Parent as Panel);
            var amountTextBox = parent.Children[2] as TextBox;
            amountTextBox.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
        }

        private void IncreaseAmountByOne(object sender, RoutedEventArgs e)
        {
            var pizza = (sender as Button).DataContext as Dish;
            var currentSizeDishAmount = pizza.Sizes.ToList()[pizza.SelectedSizeIndex].DishAmount;
            MessageBox.Show(currentSizeDishAmount.ToString());
            if (currentSizeDishAmount < 15)
            {
                currentSizeDishAmount++;
            }

            var parent = ((sender as Button).Parent as Panel);
            var amountTextBox = parent.Children[2] as TextBox;
            amountTextBox.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
        }

        private void InputPizzasAmount(object sender, TextChangedEventArgs e)
        {

        }

        private void SwitchSizeOptionAndUpdateContext(object sender, int optionIndex)
        {
            var pizza = (sender as Button).DataContext as Dish;
            pizza.SelectedSizeIndex = optionIndex;
            Debug.WriteLine(pizza.SelectedSizeIndex.ToString());
            var sourceParent = (sender as Button).Parent as Panel;
            var targetParent = ((sourceParent.Children[6] as Panel).Children[0] as Panel);

            var priceTextBox = targetParent.Children[0] as Label;
            priceTextBox.GetBindingExpression(ContentProperty).UpdateTarget();
            var weightTextBox = targetParent.Children[1] as Label;
            weightTextBox.GetBindingExpression(ContentProperty).UpdateTarget();
        }

        private void GoToOrder(object sender, RoutedEventArgs e)
        {
            Navigator.Navigate(new Orders(currentUser));
        }

        private void GoToMix(object sender, RoutedEventArgs e)
        {

        }
    }
}
