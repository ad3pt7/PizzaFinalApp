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

namespace PizzaFinalApp.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для UserEdit.xaml
    /// </summary>
    public partial class UserEdit : Page
    {
        User editableUser;
        public UserEdit(User user)
        {
            InitializeComponent();
            if(user != null)
            {
                editableUser = user;

                FirstName.Text = editableUser.FirstName;
                LastName.Text = editableUser.LastName;
                MiddleName.Text = editableUser.MiddleName;
                Phone.Text = editableUser.Phone;
                Email.Text = editableUser.Email;
                Street.Text = editableUser.Street;
                Building.Text = editableUser.Building.ToString();
                Room.Text = editableUser.Room.ToString();
                Porch.Text = editableUser.Porch.ToString();
                Floor.Text = editableUser.Floor.ToString();

            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {

        }

        private void SaveUserData(object sender, RoutedEventArgs e)
        {

        }
    }
}
