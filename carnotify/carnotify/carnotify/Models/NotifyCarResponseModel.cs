using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace carnotify.Models
{
    public class NotifyCarResponseModel
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}
