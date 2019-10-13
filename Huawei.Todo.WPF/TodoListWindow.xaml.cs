using Huawei.Todo.WPF.Helpers;
using Huawei.ToDo.Entities.Models;
using Huawei.ToDo.Service.Abstract;
using Huawei.ToDo.Service.Concrete;
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
using System.Windows.Shapes;

namespace Huawei.Todo.WPF
{
    /// <summary>
    /// Interaction logic for TodoList.xaml
    /// </summary>
    public partial class TodoListWindow : Window
    {
        IToDoListSevice service = new ToDoListService(new ToDoDb());
        public TodoListWindow()
        {
            InitializeComponent();
        }

        public User User { get; internal set; }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            Helper.InvokeAction(()=> {

                if (string.IsNullOrWhiteSpace(this.textBoxName.Text))
                {
                    Helper.Error("Name is required");
                    return;
                }
                var temp = new ToDoList { Name = this.textBoxName.Text, UserId = User.Id };
                service.Add(temp);
                service.SaveChanges();
                listBox.ItemsSource = service.GetAll(x => x.UserId == User.Id);
            });
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Helper.InvokeAction(() => {
                var temp = service.GetAll(x => x.UserId == User.Id); 
                listBox.ItemsSource = temp;

            });
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Helper.InvokeAction(() => {

                if (listBox.SelectedItems.Count == 0)
                {
                    Helper.Error("Not item selected");
                    return;
                }

                ToDoList item = (ToDoList)listBox.SelectedItems[0];              
               
                var temp = service.FirstOrDefault(x=>x.Id == item.Id);
                if (temp == null)
                {
                    Helper.Error("ToDo Not Found");
                    return;
                }
                service.Remove(temp);
                service.SaveChanges();
                listBox.ItemsSource = service.GetAll(x => x.UserId == User.Id);
            });
        }

        private void ButtonShowToDoList_Click(object sender, RoutedEventArgs e)
        {
            Helper.InvokeAction(() => {

                if (listBox.SelectedItems.Count == 0)
                {
                    Helper.Error("Not item selected");
                    return;
                }

                ToDoList item = (ToDoList)listBox.SelectedItems[0];

                var temp = service.FirstOrDefault(x => x.Id == item.Id);
                if (temp == null)
                {
                    Helper.Error("ToDo Not Found");
                    return;
                }
                TodoListItemsWindow todoListItemsWindow = new TodoListItemsWindow(temp);
                todoListItemsWindow.ShowDialog();

            });
        }
    }
}
