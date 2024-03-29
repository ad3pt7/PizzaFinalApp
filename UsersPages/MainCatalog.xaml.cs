﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace PizzaFinalApp.UsersPages
{
    /// <summary>
    /// Логика взаимодействия для MainCatalog.xaml
    /// </summary>
    public partial class MainCatalog : Page
    {
        PizzaContext context = new PizzaContext();
        List<Dish> dishToOrder = new List<Dish>();
        int PizzasInOrder = 0;
        User currentUser;
        int PizzaAmount = 0;
        public MainCatalog(User user)
        {
            InitializeComponent();
            currentUser = user;
            PizzasListView.ItemsSource = context.Dishes.ToList();
            GoToOrderButton.Content = PizzasInOrder.ToString();

        }

        private void ChangePriceAndWeightTo23cm(object sender, RoutedEventArgs e)
        {
//            MessageBox.Show("1");
            SwitchSizeOptionAndUpdateContext(sender, 0);
            SetActiveButtonRed(1, sender);
        }

        private void ChangePriceAndWeightTo30cm(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("2");
            SwitchSizeOptionAndUpdateContext(sender, 1);
            SetActiveButtonRed(2, sender);
        }


        private void ChangePriceAndWeghtTo40cm(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("3");
            SwitchSizeOptionAndUpdateContext(sender, 2);
            SetActiveButtonRed(3, sender);
        }

        public void SetActiveButtonRed(int index,object sender)
        {
            var parent = ((sender as Button).Parent as Panel);
            
            for(int i =1; i< 4; i++)
            {
                var button = parent.Children[i] as Button;
                button.Style = this.FindResource("RedButton") as Style;
            }
            var activeButton = parent.Children[index] as Button;
            activeButton.Style = this.FindResource("WhiteButton") as Style;
            
        }
        private void DecreaseAmountByOne(object sender, RoutedEventArgs e)
        {
            var pizza = (sender as Button).DataContext as Dish;

            //MessageBox.Show(pizza.Name);
            var currentSizeDishAmount = pizza.Sizes.ToList()[pizza.SelectedSizeIndex].DishAmount;
            //MessageBox.Show(currentSizeDishAmount.ToString());
            if (currentSizeDishAmount > 0)
            {
                bool hasInOrder = false;
                for (int i = 0; i < 3; i++)
                {
                    if (pizza.Sizes.ToList()[i].DishAmount != 0)
                    {
                        hasInOrder = true;
                    }
                }
                if (currentSizeDishAmount == 1 && !hasInOrder)
                {
                    dishToOrder.Remove(pizza);
                }
                //currentSizeDishAmount--;
                pizza.Sizes.ToList()[pizza.SelectedSizeIndex].DishAmount--;
                PizzasInOrder--;
                GoToOrderButton.Content = PizzasInOrder.ToString();
            }

            var parent = ((sender as Button).Parent as Panel);
            var amountTextBox = parent.Children[2] as TextBox;
            //amountTextBox.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            amountTextBox.Text = pizza.Sizes.ToList()[pizza.SelectedSizeIndex].DishAmount.ToString();
        }

        private void IncreaseAmountByOne(object sender, RoutedEventArgs e)
        {
            var pizza = (sender as Button).DataContext as Dish;
            var currentSizeDishAmount = pizza.Sizes.ToList()[pizza.SelectedSizeIndex].DishAmount;
            //MessageBox.Show(currentSizeDishAmount.ToString());
            if (currentSizeDishAmount < 15)
            {
                bool hasInOrder = false;
                for (int i = 0; i < 3; i++)
                {
                    if (pizza.Sizes.ToList()[i].DishAmount != 0)
                    {
                        hasInOrder = true;
                    }
                }
                if (currentSizeDishAmount == 0 && !hasInOrder)
                {
                    dishToOrder.Add(pizza);
                }
                //currentSizeDishAmount++;
                pizza.Sizes.ToList()[pizza.SelectedSizeIndex].DishAmount++;
                PizzasInOrder++;
                GoToOrderButton.Content = PizzasInOrder.ToString();
            }
            //MessageBox.Show(currentSizeDishAmount.ToString());
            var parent = ((sender as Button).Parent as Panel);
            var amountTextBox = parent.Children[2] as TextBox;
            //amountTextBox.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            amountTextBox.Text = pizza.Sizes.ToList()[pizza.SelectedSizeIndex].DishAmount.ToString();
        }


        private void SwitchSizeOptionAndUpdateContext(object sender, int optionIndex)
        {
            var pizza = (sender as Button).DataContext as Dish;
            pizza.SelectedSizeIndex = optionIndex;
            Debug.WriteLine(pizza.SelectedSizeIndex.ToString());
            var sourceParent = (sender as Button).Parent as Panel;
            var targetParent = (sourceParent.Children[6] as Panel).Children[0] as Panel;

            var priceTextBox = targetParent.Children[0] as Label;
            priceTextBox.GetBindingExpression(ContentProperty).UpdateTarget();
            var weightTextBox = targetParent.Children[1] as Label;
            weightTextBox.GetBindingExpression(ContentProperty).UpdateTarget();
            var amountPartent = targetParent.Children[2] as Panel;
            var amountTextBox = amountPartent.Children[2] as TextBox;
            amountTextBox.Text = pizza.Sizes.ToList()[pizza.SelectedSizeIndex].DishAmount.ToString();
        }

        private void GoToOrder(object sender, RoutedEventArgs e)
        {
            Navigator.Navigate(new Orders(currentUser, dishToOrder));
        }

        private void GoToMix(object sender, RoutedEventArgs e)
        {
            Navigator.Navigate(new MixPage(currentUser));
            //MessageBox.Show(dishToOrder.Count.ToString());
            //var order = new StringBuilder();

            //foreach (Dish dish in dishToOrder)
            //{
            //    for (int i = 0; i < 3; i++)
            //    {
            //        if(dish.Sizes.ToList()[i].DishAmount != 0)
            //        {
            //            PizzasInOrder++;
            //            order.AppendLine(dish.Name + " - " + dish.Sizes.ToList()[i].Size1 + " -- " + dish.Sizes.ToList()[i].DishAmount);
            //        }
            //    }
            //}
            // MessageBox.Show(order.ToString());
            //amountTextBox.Text = pizza.Sizes.ToList()[pizza.SelectedSizeIndex].DishAmount.ToString();
        }

        private void InputPizzasAmount(object sender, TextCompositionEventArgs e)
        {
            var lettersRegex = new Regex(@"[\p{Ll}\p{Lt}]+");
            TextBox count = sender as TextBox;
            //if (count.Text == "")
            //{
            //    count.Text = 0.ToString();
            //}
            //else
            //{

            //}
            ////if()
            if (lettersRegex.IsMatch(e.Text))
            {
                //if (Convert.ToInt32((sender as TextBox).Text) < 15)
                //{
                    e.Handled = true;
                //}
                //else
                //{
                //    (sender as TextBox).Text = "15";
                //}
            }
        }

        private void CheckAmount(object sender, RoutedEventArgs e)
        {
            var pizza = (sender as TextBox).DataContext as Dish;
            TextBox amount = sender as TextBox;
            if(PizzaAmount == 0)
            {
                if(Convert.ToInt32(amount.Text) > 15)
                {
                    pizza.Amount = 15;
                }
                bool hasInOrder = false;
                for (int i = 0; i < 3; i++)
                {
                    if (pizza.Sizes.ToList()[i].DishAmount != 0)
                    {
                        hasInOrder = true;
                    }
                }
                if (hasInOrder == true)
                {
                    dishToOrder.Add(pizza);
                }
                PizzasInOrder += pizza.Amount - PizzaAmount;
                amount.Text = pizza.Amount.ToString();
                GoToOrderButton.Content = PizzasInOrder.ToString();
            }

            
            //int inputCount = Convert.ToInt32(amount.Text);
            //if(amount.Text == "")
            //{    
            //    bool hasInOrder = false;
            //    pizza.Sizes.ToList()[pizza.SelectedSizeIndex].DishAmount = 0;
            //    amount.Text = 0.ToString();
            //    for (int i = 0; i < 3; i++)
            //    {
            //        if (pizza.Sizes.ToList()[i].DishAmount != 0)
            //        {
            //            hasInOrder = true;
            //        }
            //    }
            //    if (!hasInOrder)
            //    {
            //        dishToOrder.Remove(pizza);
            //        PizzasInOrder -= inputCount;
            ////        if(pizza.Sizes.ToList()[pizza.SelectedSizeIndex].DishAmount != 0)
            //    }
            //    GoToOrderButton.Content = PizzasInOrder.ToString();
            //}
            ////MessageBox.Show(dishToOrder.Count.ToString());
            
            //var pizza = (sender as TextBox).DataContext as Dish;
            //if ((sender as TextBox).Text == "")
            //{
            //    (sender as TextBox).Text = 0.ToString();
            //}
            //else
            //{
            //    if(int.Parse((sender as TextBox).Text) > 15)
            //    {
            //        (sender as TextBox).Text = 15.ToString();
            //        {
            //            int count = pizza.Sizes.ToList()[pizza.SelectedSizeIndex].DishAmount;
            //            pizza.Sizes.ToList()[pizza.SelectedSizeIndex].DishAmount = 15;
            //            int totalCount = 15 ;
            //            MessageBox.Show(count + " - " + totalCount);
            //            PizzasInOrder += count;

            //            GoToOrderButton.Content = PizzasInOrder.ToString();
            //        }
            //    }
            //if(int.Parse((sender as TextBox).Text) < 0)
            //{
            //    int count = pizza.Sizes.ToList()[pizza.SelectedSizeIndex].DishAmount;
            //    if(count > 0)
            //    {

            //    }
            //    (sender as TextBox).Text = 0.ToString();
            //    pizza.Sizes.ToList()[pizza.SelectedSizeIndex].DishAmount = 0;
            //}
            //else
            //{
            //    if (int.Parse((sender as TextBox).Text) > 15)
            //    {
            //        (sender as TextBox).Text = 15.ToString();
            //        int count = pizza.Sizes.ToList()[pizza.SelectedSizeIndex].DishAmount;
            //        pizza.Sizes.ToList()[pizza.SelectedSizeIndex].DishAmount = 15;
            //        int totalCount = 15 - count;
            //        PizzasInOrder += count;
            //        GoToOrderButton.Content = PizzasInOrder.ToString();
            //        if (count < 15)
            //        {
            //            bool hasInOrder = false;
            //            for (int i = 0; i < 3; i++)
            //            {
            //                if (pizza.Sizes.ToList()[i].DishAmount != 0)
            //                {
            //                    hasInOrder = true;
            //                }
            //            }
            //            if (count == 0 && !hasInOrder)
            //            {
            //                dishToOrder.Add(pizza);
            //            }
            //            //currentSizeDishAmount++;
            //            pizza.Sizes.ToList()[pizza.SelectedSizeIndex].DishAmount++;
            //            PizzasInOrder++;
            //            GoToOrderButton.Content = PizzasInOrder.ToString();
            //        }
            //    }
            //    else
            //    {
            //        bool hasInOrder = false;
            //        for (int i = 0; i < 3; i++)
            //        {
            //            if (pizza.Sizes.ToList()[i].DishAmount != 0)
            //            {
            //                hasInOrder = true;
            //            }
            //        }
            //        if (count == 0 && !hasInOrder)
            //        {
            //            dishToOrder.Add(pizza);
            //        }
            //    }
            //}
        }

        private void ChangeAmount(object sender, RoutedEventArgs e)
        {
            var pizza = (sender as TextBox).DataContext as Dish;
            PizzaAmount = pizza.Amount;
        }
    }
}

