using carnotify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace carnotify.Interfaces
{
    public interface ICarNotifyService
    {
        Task<RegisterCarResponseModel> RegisterCar(RegisterCarRequestModel request, CancellationToken cancellationToken);
        Task<NotifyCarResponseModel> NotifyCar(NotifyCarRequestModel request, CancellationToken cancellationToken);
    }
}
