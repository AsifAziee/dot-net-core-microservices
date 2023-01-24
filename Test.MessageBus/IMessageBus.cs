
namespace Test.MessageBus;

public interface IMessageBus
{
    Task Publish(BaseMessage message, string topic);
}
