using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace carnotify.Models
{
    public class NotifyCarRequestModel
    {
        public string Country { get; set; }
        public string Plate { get; set; }

        public string MessageId { get; set; }
    }
}
