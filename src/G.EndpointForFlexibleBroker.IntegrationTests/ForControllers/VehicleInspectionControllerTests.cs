using FluentAssertions;
using G.EndpointForFlexibleBroker.Shared.DTOs;
using G.EndpointForFlexibleBroker.App.Infrastructure.BrokerClients;
using System.Text;
using System.Text.Json;
using Xunit;

namespace G.EndpointForFlexibleBroker.IntegrationTests.ForControllers
{
    public class VehicleInspectionControllerTests : IClassFixture<WebApplicationFactoryForTests<Program>>
    {
        private readonly WebApplicationFactoryForTests<Program> _factory;

        public VehicleInspectionControllerTests(WebApplicationFactoryForTests<Program> factory)
        {
            _factory = factory;
        }


        [Fact]
        public async Task For_ExistingInConfigBroker_Should_ReturnAccepted()
        {
            // arrange
            var client = _factory.CreateClient();

            var url = "api/vehicleInspection";

            var vehicleInspection = new VehicleInspectionDto("Broker1", "KR 12345", 1, 100);
            var data = PreparePostPayload(vehicleInspection);

            // act
            var response = await client.PostAsync(url, data);

            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Accepted);
        }

        [Fact]
        public async Task For_NonExistingInConfigBroker_Should_ReturnNotAllowed()
        {
            // arrange
            var client = _factory.CreateClient();

            var url = "api/vehicleInspection";

            var vehicleInspection = new VehicleInspectionDto("BrokerThatDoesNotExistInAppSettings1", "KR 12345", 1, 100);
            var data = PreparePostPayload(vehicleInspection);

            // act
            var response = await client.PostAsync(url, data);

            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.MethodNotAllowed);
        }

        [Fact]
        public async Task For_BrokerWithNotImplementedClient_Should_ReturnError()
        {
            // arrange
            var client = _factory.CreateClient();

            var url = "api/vehicleInspection";

            var vehicleInspection = new VehicleInspectionDto("BrokerThatHasWrongType1", "KR 12345", 1, 100);
            var data = PreparePostPayload(vehicleInspection);

            // act
            var response = await client.PostAsync(url, data);

            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task For_MissingLocationId_Should_ReturnBadRequest()
        {
            // arrange
            var client = _factory.CreateClient();

            var url = "api/vehicleInspection";

            var vehicleInspection = new VehicleInspectionDto("Broker1", "KR 12345", 0 /*HERE ERROR*/, 100);
            var data = PreparePostPayload(vehicleInspection);

            // act
            var response = await client.PostAsync(url, data);

            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task For_MissingEpochTimeMs_Should_ReturnBadRequest()
        {
            // arrange
            var client = _factory.CreateClient();

            var url = "api/vehicleInspection";

            var vehicleInspection = new VehicleInspectionDto("Broker1", "KR 12345", 1, 0 /*HERE ERROR*/);
            var data = PreparePostPayload(vehicleInspection);

            // act
            var response = await client.PostAsync(url, data);

            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        private static StringContent PreparePostPayload(VehicleInspectionDto vehicleInspection)
        {
            var json = JsonSerializer.Serialize(vehicleInspection);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            return data;
        }
    }
}
