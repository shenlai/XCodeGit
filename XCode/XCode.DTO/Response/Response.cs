using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCode.DTO.Response
{
    public class ResponseHead
    {

        public bool IsSuccess { get; set; }

        public int Code { get; set; }
        public string Message { get; set; }

        public ResponseHead()
        {
            IsSuccess = false;
            Code = 0;
            Message = "";
        }

        public ResponseHead(bool issuccess, int code, string msg)
        {
            IsSuccess = issuccess;
            Code = code;
            Message = msg;
        }

    }

    public class Response<T>
    {
        public Response()
        {
            this.Head = new ResponseHead();
            this.Content = default(T);
        }

        public ResponseHead Head { get; set; }

        public T Content { get; set; }


    }

    


}
