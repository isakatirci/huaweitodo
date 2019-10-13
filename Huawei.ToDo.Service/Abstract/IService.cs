using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huawei.ToDo.Service.Abstract
{
    public interface IService<TEntity> : IRepository<TEntity> where TEntity : class
    {

    }
}
