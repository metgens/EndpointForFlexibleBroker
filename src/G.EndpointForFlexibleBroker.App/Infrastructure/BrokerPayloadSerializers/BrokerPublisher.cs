using G.EndpointForFlexibleBroker.Shared;
using G.EndpointForFlexibleBroker.Shared.Interfaces;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;

namespace G.EndpointForFlexibleBroker.App.Infrastructure.BrokerPayloadSerializers
{
    public class BrokerPublisher : IBrokerPublisher
    {
        private readonly IBrokerClientFactory _brokerClientFactory;
        private readonly IBrokerMessageSerializer _brokerMessageSerializer;
        private readonly ILogger<BrokerPublisher> _logger;

        public BrokerPublisher(IBrokerClientFactory brokerClientFactory, IBrokerMessageSerializer brokerMessageSerializer, ILogger<BrokerPublisher> logger)
        {
            _brokerClientFactory = brokerClientFactory;
            _brokerMessageSerializer = brokerMessageSerializer;
            _logger = logger;
        }

        public async Task<Result> SendAsync(IEntityWithBrokerDeclaration message)
        {
            var resultBrokerFactory = _brokerClientFactory.Get(message.BrokerName);
            if (resultBrokerFactory.Failure)
                return resultBrokerFactory;

            var brokerClient = resultBrokerFactory.Data;

            var messageSerialized = _brokerMessageSerializer.Serialize(message);

            var resultSend = await brokerClient.SendAsync(messageSerialized.payload, messageSerialized.properties);
            if (resultSend.Failure)
                return resultSend;

            return new SuccessResult();
        }
    }
}