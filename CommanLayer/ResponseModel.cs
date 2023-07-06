using System;
using System.Collections.Generic;
using System.Text;

namespace CommanLayer
{
    public class ResponseModel<T>
    {
        public bool status { get; set; }
        public string message { get; set; } 
        public T response { get; set; }
    }
}
