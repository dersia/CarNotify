using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace carnotify.Constants
{
    public static class TemplateConstants
    {
        public static IDictionary<string, string> TemplateMessageToName { get; } = new Dictionary<string, string>
        {
            {"Move your car", "moveCarTemplate" },
            {"Car was hit", "hitCarTemplate" },
            {"Car towed", "carTowedTemplate" }
        };

        public static JObject GetWindowsTemplates()
        {
            var WindowsTemplates = new JObject();
            WindowsTemplates.Add("moveCarTemplate", JToken.FromObject( new
            {
                body = @"<toast><visual><binding template=""ToastImageAndText02""><image id=""1"" src=""image1"" alt=""image1""/><text id=""1"">Your Car is blocking mine</text><text id=""2"">Please come to your Car. You are blocking me!</text></binding></visual></toast>",
                headers = new Dictionary<string, string>() { ["X-WNS-Type"] = "wns/toast" }
            }));
            WindowsTemplates.Add("carTowedTemplate", JToken.FromObject( new
            {
                body = @"<toast><visual><binding template=""ToastImageAndText02""><image id=""1"" src=""image1"" alt=""image1""/><text id=""1"">Your Car is being towed</text><text id=""2"">Please come to your Car. Your Car is being towed!</text></binding></visual></toast>",
                headers = new Dictionary<string, string>() { ["X-WNS-Type"] = "wns/toast" }
            }));
            WindowsTemplates.Add("hitCarTemplate", JToken.FromObject( new
            {
                body = @"<toast><visual><binding template=""ToastImageAndText02""><image id=""1"" src=""image1"" alt=""image1""/><text id=""1"">Your Car has been hit</text><text id=""2"">Please come to your Car. I accidently hit your car!</text></binding></visual></toast>",
                headers = new Dictionary<string, string>() { ["X-WNS-Type"] = "wns/toast" }
            }));
            return WindowsTemplates;
        }

        public static JObject GetIosTemplates()
        {
            var iOsTemplates = new JObject();
            iOsTemplates.Add("moveCarTemplate", JToken.FromObject( new
            {
                body = JsonConvert.SerializeObject(new
                {
                    aps = new
                    {
                        alert = new
                        {
                            body = "Please come to your Car. You are blocking me!",
                            title = "Your Car is blocking mine"
                        }
                    }
                })
            }));
            iOsTemplates.Add("carTowedTemplate", JToken.FromObject( new
            {
                body = JsonConvert.SerializeObject(new
                {
                    aps = new
                    {
                        alert = new
                        {
                            body = "Please come to your Car. Your Car is being towed!",
                            title = "Your Car is being towed"
                        }
                    }
                })
            }));
            iOsTemplates.Add("hitCarTemplate", JToken.FromObject( new
            {
                body = JsonConvert.SerializeObject(new
                {
                    aps = new
                    {
                        alert = new
                        {
                            body = "Please come to your Car. I accidently hit your car!",
                            title = "Your Car has been hit"
                        }
                    }
                })
            }));
            return iOsTemplates;
        }

        public static JObject GetAndroidTemplates()
        {
            var AndroidTemplates = new JObject();
            AndroidTemplates.Add("moveCarTemplate", JToken.FromObject( new
            {
                body = JsonConvert.SerializeObject(new
                {
                    priority = "normal",
                    time_to_live = 0,
                    notification = new
                    {
                        body = "Please come to your Car. You are blocking me!",
                        title = "Your Car is blocking mine"
                    }
                })
            }));
            AndroidTemplates.Add("carTowedTemplate", JToken.FromObject( new
            {
                body = JsonConvert.SerializeObject(new
                {
                    priority = "normal",
                    time_to_live = 0,
                    notification = new
                    {
                        body = "Please come to your Car. Your Car is being towed!",
                        title = "Your Car is being towed"
                    }
                })
            }));
            AndroidTemplates.Add("hitCarTemplate", JToken.FromObject( new
            {
                body = JsonConvert.SerializeObject(new
                {
                    priority = "normal",
                    time_to_live = 0,
                    notification = new
                    {
                        body = "Please come to your Car. I accidently hit your car!",
                        title = "Your Car has been hit"
                    }
                })
            }));
            return AndroidTemplates;
        }

        public static dynamic GetWinPhoneTemplates()
        {
            dynamic WindowsPhoneTemplates = new ExpandoObject();
            return WindowsPhoneTemplates;
        }
    }
}
