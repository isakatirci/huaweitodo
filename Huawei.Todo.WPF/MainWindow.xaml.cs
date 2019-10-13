using Huawei.Todo.WPF.Helpers;
using Huawei.Todo.WPF.ViewModels;
using Huawei.ToDo.Entities.Models;
using Huawei.ToDo.Service.Abstract;
using Huawei.ToDo.Service.Concrete;
using System;
using System.Collections.Generic;
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

namespace Huawei.Todo.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IUserSevice service = new UserSevice(new ToDoDb());

        public MainWindow()
        {
            InitializeComponent();
        }



        private void ButtonUserSave_Click(object sender, RoutedEventArgs e)
        {
            if (!TestEmailRegex(this.textBoxEmail.Text))
            {
                Helper.Error("Email is not valid!");
                return;
            }
            if (string.IsNullOrWhiteSpace(this.passwordBoxPassword.Password))
            {
                Helper.Error("Password is empty!");
                return;
            }

            Helper.InvokeAction(() =>
            {

                if (service.FirstOrDefault(x => x.Email == this.textBoxEmail.Text) != null)
                {
                    Helper.Error("Email address has already registered");
                    return;
                }

                service.Add(new User
                {
                    Email = this.textBoxEmail.Text,
                    Password = this.passwordBoxPassword.Password
                });
                service.SaveChanges();
                this.textBoxEmail.Text = string.Empty;
                this.passwordBoxPassword.Password = string.Empty;
            });

        }

        public bool TestEmailRegex(string emailAddress)
        {
            string patternStrict = @"^(([^<>()[\]\\.,;:\s@\""]+"
            + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
            + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
            + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
            + @"[a-zA-Z]{2,}))$";
            Regex reStrict = new Regex(patternStrict);
            bool isStrictMatch = reStrict.IsMatch(emailAddress);
            return isStrictMatch;

        }

        private void ButtonUserLogin_Click(object sender, RoutedEventArgs e)
        {
            Helper.InvokeAction(() =>
            {
                var user = service.FirstOrDefault(x => x.Email == this.textBoxEmailLogin.Text && x.Password == this.passwordBoxLogin.Password);
                if (user == null)
                {
                    Helper.Error("User Not Found");
                    return;
                }
                TodoListWindow todoListWindow = new TodoListWindow();
                todoListWindow.User = user;
                todoListWindow.ShowDialog();
            });
        }
    }
}
