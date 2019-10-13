using Huawei.ToDo.Entities.Models;
using Huawei.ToDo.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huawei.ToDo.Service.Concrete
{

    public class TodoListItemSevice : Service<TodoListItem>, ITodoListItemSevice
    {
        public TodoListItemSevice(DbContext context) : base(context)
        {
        }
    }
}
