using Huawei.ToDo.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huawei.ToDo.Service.Concrete
{
    public class Service<TEntity> : Repository<TEntity>, IService<TEntity> where TEntity : class
    {
        public Service(DbContext context) : base(context)
        {
            
        }
    }
}
