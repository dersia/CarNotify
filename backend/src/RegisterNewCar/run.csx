using System;

public static async Task Run(string req, string country, string plate, ICollector<CarNotify> outputTable, TraceWriter log)
{
    log.Info($"New registration arrived for: coutnry: {country} --> plate: {plate} --> notify: {req}");
    try 
    {
        outputTable.Add(new CarNotify() { PartitionKey = country, RowKey = plate, RegistrationId = req });
    } catch(Exception ex) {
        log.Error(ex.GetType().ToString());
    }
}

public class CarNotify
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public string RegistrationId { get; set; }
}