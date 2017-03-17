using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using carnotify.Models;
using System.Net;
using System.Threading;
using carnotify.Utils;
using System.Net.Http;
using Newtonsoft.Json;
using System.Dynamic;
using carnotify.Constants;
using System.Linq;

namespace carnotify.Services
{
    public static class PushRegistrationService
    {
        public static string InstallationId = null;

        public static async Task RegisterDeviceAsync(CancellationToken cancellationToken, string pushPlatform, string registrationId, IList<string> pushTags = null)
        {
            var statusCode = await CreateOrUpdateInstallationAsync(CreateDeviceInstallation(pushPlatform, registrationId, pushTags),
                    ConfigurationConstants.NotificationHubHubName, ConfigurationConstants.NotificationHubListenConnectionString, cancellationToken);
        }

        private static async Task<HttpStatusCode> CreateOrUpdateInstallationAsync(DeviceInstallation deviceInstallation, string hubName, string listenConnectionString, CancellationToken cancellationToken)
        {
            if (deviceInstallation.installationId == null)
                return HttpStatusCode.BadRequest;

            ConnectionStringUtility connectionSaSUtil = new ConnectionStringUtility(listenConnectionString);
            string hubResource = "installations/" + deviceInstallation.installationId + "?";
            string apiVersion = "api-version=2016-07";

            string uri = connectionSaSUtil.Endpoint + hubName + "/" + hubResource + apiVersion;

            string SasToken = connectionSaSUtil.getSaSToken(uri, 60);

            using (var httpClient = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(deviceInstallation);

                httpClient.DefaultRequestHeaders.Add("Authorization", SasToken);

                var response = await httpClient.PutAsync(uri, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
                return response.StatusCode;

                // return HttpStatusCode.OK;
            }
        }

        private static DeviceInstallation CreateDeviceInstallation(string pushPlatfrom, string registrationId, IList<string> pushTags = null)
        {
            InstallationId = registrationId;
            var pushChannel = registrationId;
            dynamic pushTemplates = new ExpandoObject();
            switch (pushPlatfrom)
            {
                case PushPlatforms.ANDROID:
                    pushTemplates = TemplateConstants.GetAndroidTemplates();
                    break;
                case PushPlatforms.IOS:
                    InstallationId = ParseDeviceToken(registrationId);
                    pushChannel = ParseDeviceToken(registrationId);
                    pushTemplates = TemplateConstants.GetIosTemplates();
                    break;
                case PushPlatforms.WINDOWS:
                    InstallationId = GetSha1Hash(registrationId);
                    pushTemplates = TemplateConstants.GetWindowsTemplates();
                    break;
                default:
                    break;
            }
            if(pushTags == null)
            {
                pushTags = new List<string>();
            }

            pushTags.Add($"id:{InstallationId}");
            
            return new DeviceInstallation
            {
                templates = pushTemplates,
                installationId = InstallationId,
                platform = pushPlatfrom,
                pushChannel = pushChannel,
                tags = pushTags.ToArray()
            };
        }

        private static string GetSha1Hash(string text)
        {
            var sha1 = new System.Security.Cryptography.HMACSHA1();

            string result = null;

            var arrayData = Encoding.ASCII.GetBytes(text);
            var arrayResult = sha1.ComputeHash(arrayData);
            foreach (var t in arrayResult)
            {
                var temp = Convert.ToString(t, 16);
                if (temp.Length == 1)
                    temp = string.Format("0{0}", temp);
                result += temp;
            }
            return result;
        }

        private static string ParseDeviceToken(string deviceToken)
        {
            if (deviceToken == null)
            {
                throw new ArgumentNullException("deviceToken");
            }

            return deviceToken.Trim('<', '>').Replace(" ", string.Empty).ToUpperInvariant();
        }
    }
}
