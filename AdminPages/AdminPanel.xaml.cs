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
        }

        private void EditPizza(object sender, RoutedEventArgs e)
        {

        }

        private void DeletePizza(object sender, RoutedEventArgs e)
        {

        }

        private void Back(object sender, RoutedEventArgs e)
        {

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
    }
}
