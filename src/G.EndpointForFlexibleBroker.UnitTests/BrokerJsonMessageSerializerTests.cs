using AutoMoq;
using FluentAssertions;
using G.EndpointForFelxibleBroker.Shared.DTOs;
using Xunit;

namespace G.EndpointForFlexibleBroker.App.Infrastructure.BrokerPayloadSerializers
{
    public class BrokerJsonMessageSerializerTests
    {
        private BrokerJsonMessageSerializer _target;

        public BrokerJsonMessageSerializerTests()
        {
            var mocker = new AutoMoqer();
            _target = mocker.Create<BrokerJsonMessageSerializer>();
        }

        [Fact]
        public void For_ValidInput_Should_ReturnOneProperty()
        {
            // arrange
            var inputMessage = new VehicleInspectionDto("Broker1", "WW 9999", 123, 1652873466000);

            // act
            var result = _target.Serialize(inputMessage);

            // assert
            result.properties.Should().NotBeNull();
            result.properties?.Count.Should().Be(1);
            result.properties?["MessageType"].Should().Be("VehicleInspectionDto");
        }

        [Fact]
        public void For_ValidInput_Should_ReturnSerializedPayload()
        {
            // arrange
            var inputMessage = new VehicleInspectionDto("Broker1", "WW 9999", 123, 1652873466000);

            // generated on https://onlineutf8tools.com/convert-utf8-to-bytes
            var expectedPayload = new byte[]
            {
0x7b,0x22,0x42,0x72,0x6f,0x6b,0x65,0x72,0x4e,0x61,0x6d,0x65,0x22,0x3a,0x22,0x42,0x72,0x6f,0x6b,0x65,0x72,0x31,0x22,0x2c,0x22,0x50,0x6c,0x61,0x74,0x65,0x4e,0x75,0x6d,0x62,0x65,0x72,0x22,0x3a,0x22,0x57,0x57,0x20,0x39,0x39,0x39,0x39,0x22,0x2c,0x22,0x4c,0x6f,0x63,0x61,0x74,0x69,0x6f,0x6e,0x49,0x64,0x22,0x3a,0x31,0x32,0x33,0x2c,0x22,0x45,0x70,0x6f,0x63,0x68,0x54,0x69,0x6d,0x65,0x4d,0x73,0x22,0x3a,0x31,0x36,0x35,0x32,0x38,0x37,0x33,0x34,0x36,0x36,0x30,0x30,0x30,0x7d
            };

            // act
            var result = _target.Serialize(inputMessage);

            // assert
            result.payload.Should().BeEquivalentTo(expectedPayload);
        }


    }

}