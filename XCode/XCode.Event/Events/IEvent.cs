using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCode.Events
{
    public interface IEvent
    {
        /// <summary>
        /// 产生领域事件的时间
        /// </summary>
        DateTime CreateTime { get; set; }

        /// <summary>
        /// 产生领域事件的事件源对象
        /// </summary>
        object Source { get; set; }


    }
}
