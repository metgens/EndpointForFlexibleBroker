using G.EndpointForFlexibleBroker.Shared.DTOs;
using G.EndpointForFlexibleBroker.App.Infrastructure.BrokerClients;
using G.EndpointForFlexibleBroker.App.Infrastructure.BrokerPayloadSerializers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using G.EndpointForFlexibleBroker.Shared;

namespace G.EndpointForFlexibleBroker.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize] 
    public class VehicleInspectionController : ControllerBase
    {

        private readonly IBrokerPublisher _brokerPublisher;

        public VehicleInspectionController(IBrokerPublisher brokerPublisher)
        {
            _brokerPublisher = brokerPublisher;
        }

        /// <summary>
        /// Endpoint for vehicle inspection messages
        /// </summary>
        /// <param name="vehicleInspection">Vehicle inspection message</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReceiveVehicleInspection(VehicleInspectionDto vehicleInspection)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _brokerPublisher.SendAsync(vehicleInspection);
            if (result.Failure)
            {
                return result switch
                {
                    NotFoundResult<IBrokerClient> errResult => StatusCode((int)StatusCodes.Status405MethodNotAllowed, errResult.Message),
                    ErrorResult<IBrokerClient> errResult => StatusCode((int)StatusCodes.Status500InternalServerError, errResult.Message),
                    _ => StatusCode((int)HttpStatusCode.InternalServerError)
                };
            }
                       
            return Accepted();
        }
    }
}
