using carnotify.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using carnotify.Models;
using System.Net.Http;
using System.Threading;
using carnotify.Constants;

namespace carnotify.Services
{
    public class CarNotifyService : ICarNotifyService
    {
        public async Task<NotifyCarResponseModel> NotifyCar(NotifyCarRequestModel request, CancellationToken cancellationToken)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigurationConstants.NotifyCarEndpoint);
                httpClient.DefaultRequestHeaders.Add("x-functions-key", ConfigurationConstants.NotifyCarKey);
                var response = await httpClient.PostAsync($"{request.Country}/{request.Plate}/{request.MessageId}", new StringContent(""));
                return new NotifyCarResponseModel { Success = response.IsSuccessStatusCode, StatusCode = response.StatusCode };
            }
        }

        public async Task<RegisterCarResponseModel> RegisterCar(RegisterCarRequestModel request, CancellationToken cancellationToken)
        {
            using(var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigurationConstants.RegisterCarEndpoint);
                httpClient.DefaultRequestHeaders.Add("x-functions-key", ConfigurationConstants.RegisterCarKey);
                var response = await httpClient.PostAsync($"{request.Country}/{request.Plate}", new StringContent(request.RegistrationId));
                return new RegisterCarResponseModel { Success = response.IsSuccessStatusCode, StatusCode = response.StatusCode };
            }
        }
        
    }
}
