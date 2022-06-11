using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PizzaFinalApp
{
    public static class Navigator
    {
        private static Frame _frame;

        public static void SetFrame(Frame frame)
        {
            _frame = frame;
        }

        public static void Navigate(Page page)
        {
            _frame.Navigate(page);
        }
    }
}
