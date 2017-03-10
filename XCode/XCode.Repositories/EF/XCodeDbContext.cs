using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCode.Repositories.EF
{
    public sealed class XCodeDbContext:DbContext
    {
        public XCodeDbContext():base("")
        {
        }
    }
}
