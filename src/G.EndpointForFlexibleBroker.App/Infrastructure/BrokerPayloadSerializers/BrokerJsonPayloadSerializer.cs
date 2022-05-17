using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;

namespace G.EndpointForFlexibleBroker.App.Infrastructure.BrokerPayloadSerializers
{
    public class BrokerJsonMessageSerializer : IBrokerMessageSerializer
    {
        public (byte[] payload, Dictionary<string, object>? properties) Serialize(object obj)
        {
            var jsonString = JsonSerializer.Serialize(obj);
            var payload = Encoding.UTF8.GetBytes(jsonString);
            var properties = new Dictionary<string, object>();
            properties.Add("MessageType", obj.GetType().Name);

            return (payload, properties);
        }
    }
}