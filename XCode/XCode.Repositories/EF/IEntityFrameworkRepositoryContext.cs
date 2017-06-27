using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCode.Domain.IRepositories;

namespace XCode.Repositories.EF
{

    /// <summary>
    /// EF仓储上下文契约
    /// 只需要定义具体ORM上下文
    /// </summary>
    public interface IEntityFrameworkRepositoryContext:IRepositoryContext
    {

        //XCodeDbContext DbContext { get; }
        DbContext DbContext { get; }
    }
}
