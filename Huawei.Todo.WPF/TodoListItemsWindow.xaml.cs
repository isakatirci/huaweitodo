using Huawei.Todo.WPF.ViewModels;
using Huawei.ToDo.Entities.Models;
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
    /// Interaction logic for TodoListItemsWindow.xaml
    /// </summary>
    public partial class TodoListItemsWindow : Window
    {
        private readonly ToDoList todoList;

        public TodoListItemsWindow(ToDoList TodoList)
        {
            InitializeComponent();
            this.DataContext = new ToDoListItemViewModel(TodoList);
            todoList = TodoList;
        }
    }
}
