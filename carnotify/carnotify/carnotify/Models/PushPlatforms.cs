using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace carnotify.Models
{
    public static class PushPlatforms
    {
        public const string WINDOWS = "wns";
        public const string IOS = "apns";
        public const string ANDROID = "gcm";
        public const string WINPHONE = "mpns";
    }
}
