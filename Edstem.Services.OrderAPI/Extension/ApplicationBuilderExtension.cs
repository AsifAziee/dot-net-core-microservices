using Edstem.Services.OrderAPI.Messaging;

namespace Edstem.Services.OrderAPI.Extension;

public static class ApplicationBuilderExtension
{
    private static IAzureServiceBusConsumer? ServiceBusConsumer { get; set; }

    public static IApplicationBuilder UseAzureServiceBusConsumer(this IApplicationBuilder app)
    {
        ServiceBusConsumer = app.ApplicationServices.GetService<IAzureServiceBusConsumer>();

        var hostAppplicationLife = app.ApplicationServices.GetService<IHostApplicationLifetime>();

        hostAppplicationLife.ApplicationStarted.Register(OnStart);
        hostAppplicationLife.ApplicationStopped.Register(OnStop);
        return app;
    }

    private static void OnStart()
    {
        ServiceBusConsumer!.Start();
    }
    private static void OnStop()
    {
        ServiceBusConsumer!.Stop();
    }
}
