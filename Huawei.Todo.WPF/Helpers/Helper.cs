using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Huawei.Todo.WPF.Helpers
{
    public class Helper
    {
        public static void InvokeAction(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Error(ex.ToString());
            }
        }

        public static void Error(string message)
        {

            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        public static DateTime DateParse(string value)
        {
            DateTime now = DateTime.Now;
            DateTime.TryParseExact(value,new string[] { "dd.MM.yyyy", "dd/MM/yyyy", }, null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out now);
            return now;
        }
    }
}
