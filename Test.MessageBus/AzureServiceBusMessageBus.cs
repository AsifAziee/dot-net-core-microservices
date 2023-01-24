using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;

namespace Test.MessageBus;

public class AzureServiceBusMessageBus : IMessageBus
{
    private readonly string _connectionString;

    public AzureServiceBusMessageBus(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task Publish(BaseMessage message, string topics)
    {
        await using var client = new ServiceBusClient(_connectionString);
        var sender = client.CreateSender(topics);
        var jsonMessage = JsonConvert.SerializeObject(message);
        var serviceBusMessage = new ServiceBusMessage(jsonMessage);
        await sender.SendMessageAsync(serviceBusMessage);

    }
}
