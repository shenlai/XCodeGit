using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCode.DTO
{
    public class Request<T>
    {
        public Request()
        {
            Head = new RequestHead();
            Content = default(T);
        }

        public RequestHead Head { get; set; }

        public T Content { get; set; }
        
    }



    public class RequestHead
    {
        public string Version { get; set; }

        public string Token { get; set; }

        public int Source { get; set; }


    }
}
