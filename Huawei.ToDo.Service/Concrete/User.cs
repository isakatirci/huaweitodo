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

    public class UserSevice : Service<User>, IUserSevice
    {
        public UserSevice(DbContext context) : base(context)
        {
            
        }
    }
}
