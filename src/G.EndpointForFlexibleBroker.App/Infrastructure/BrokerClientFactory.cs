using Autofac.Features.Indexed;
using G.EndpointForFlexibleBroker.App.Infrastructure.BrokerClients;

namespace G.EndpointForFlexibleBroker.App.Infrastructure
{
    public class BrokerClientFactory : IBrokerClientFactory
    {
        private readonly IConfiguration _configuration;
        private readonly IIndex<BrokerClientType, IBrokerClient> _brokerClients;

        public BrokerClientFactory(IConfiguration configuration, IIndex<BrokerClientType, IBrokerClient> brokerClients)
        {
            _configuration = configuration;
            _brokerClients = brokerClients;
        }

        public Result<IBrokerClient> Get(string brokerName)
        {
            var configBrokersExists = _configuration.GetSection("Brokers").Exists();
            if(!configBrokersExists)
                return new ErrorResult<IBrokerClient>("Missing 'Brokers' section in appsettings");

            var configSpecificBroker = _configuration.GetSection($"Brokers").GetChildren().FirstOrDefault(x => x.Key == brokerName);
            if (configSpecificBroker == null)
                return new NotFoundResult<IBrokerClient>(nameof(brokerName), $"Broker with name '{brokerName}' is not allowed");

            var resultEnumParse = Enum.TryParse(configSpecificBroker["Type"], out BrokerClientType brokerType);
            if (!resultEnumParse)
                return new ErrorResult<IBrokerClient>($"Wrong broker type appsetting configured in seciton: '{brokerName}'. Provided value: {configSpecificBroker["Type"]}");


            var brokerClient = _brokerClients[brokerType];

            if (brokerClient.Equals(null))
                return new ErrorResult<IBrokerClient>($"Broker client for key: '{brokerType}' not registered in IoC container.");

            brokerClient.Configure(configSpecificBroker);

            return new SuccessResult<IBrokerClient>(brokerClient);
        }
    }
}
