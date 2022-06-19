﻿using System;
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
    /// Логика взаимодействия для ChequePage.xaml
    /// </summary>
    public partial class ChequePage : Page
    {
        User currentUser;
        public ChequePage(User user)
        {
            InitializeComponent();
            currentUser = user;
        }


        private void OpenMenu(object sender, RoutedEventArgs e)
        {
            Navigator.Navigate(new MainCatalog(currentUser));
        }
    }
}
