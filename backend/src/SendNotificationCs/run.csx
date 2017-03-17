#r "Newtonsoft.Json"
#r "Microsoft.Azure.WebJobs.Extensions.NotificationHubs"
#r "Microsoft.Azure.NotificationHubs"
using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.NotificationHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.NotificationHubs;

public static async Task Run(NotifyItem myQueueItem, Binder binder, TraceWriter log)
{

    log.Info(JsonConvert.SerializeObject(myQueueItem));
    var notificationHubBinding = new NotificationHubAttribute() 
    { 
        TagExpression = $"id:{myQueueItem.notifyurl} && {myQueueItem.messageid}",
        ConnectionStringSetting = "carnotify_NOTIFICATIONHUB",
        HubName = "carnotify",
        EnableTestSend = false
    };
    log.Info(JsonConvert.SerializeObject(notificationHubBinding));
    var client = await binder.BindAsync<IAsyncCollector<IDictionary<string,string>>>(notificationHubBinding);
    await client.AddAsync(GetTemplateProperties(myQueueItem.plate));
}

private static IDictionary<string, string> GetTemplateProperties(string plate)
{
    Dictionary<string, string> templateProperties = new Dictionary<string, string>();
    templateProperties["plate"] = plate;
    return templateProperties;
}

public class NotifyItem 
{
    public string plate { get; set; }
    public string notifyurl { get; set; }
    public string messageid { get; set; }

}