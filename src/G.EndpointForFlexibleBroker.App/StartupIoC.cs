using Autofac;
using G.EndpointForFlexibleBroker.App.Infrastructure;
using G.EndpointForFlexibleBroker.App.Infrastructure.BrokerClients;
using G.EndpointForFlexibleBroker.App.Infrastructure.BrokerClients.AzureEventHub;
using G.EndpointForFlexibleBroker.App.Infrastructure.BrokerPayloadSerializers;

namespace G.EndpointForFlexibleBroker.App
{
    public class StartupIoC
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            builder.Host.ConfigureContainer((Action<ContainerBuilder>)(autofacBuilder =>
            {
                autofacBuilder.RegisterType<BrokerPublisher>().As<IBrokerPublisher>();
                autofacBuilder.RegisterType<BrokerClientFactory>().As<IBrokerClientFactory>();
                autofacBuilder.RegisterType<AzureEventHubProducerClientFactory>().As<IAzureEventHubProducerClientFactory>().SingleInstance(); //to reuse eventhub client and avoid closing and opening connection
                autofacBuilder.RegisterType<AzureEventHubClient>().Keyed<IBrokerClient>(BrokerClientType.AzureEventHub);
                autofacBuilder.RegisterType<BrokerJsonMessageSerializer>().As<IBrokerMessageSerializer>();
            }));
        }
    }
}
