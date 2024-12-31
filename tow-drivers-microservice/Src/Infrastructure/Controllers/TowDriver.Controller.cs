using Application.Core;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json.Nodes;
using tow_drivers_microservice.Src.Application.Commands.UpdateTowDriverLocation;
using tow_drivers_microservice.Src.Application.Commands.UpdateTowDriverLocation.Types;
using tow_drivers_microservice.Src.Application.Commands.UpdateTowDriverStatus;
using tow_drivers_microservice.Src.Application.Commands.UpdateTowDriverStatus.Types;
using tow_drivers_microservice.Src.Infrastructure.Controllers.Dtos;
using TowDrivers.Application;
using TowDrivers.Domain;


namespace TowDrivers.Infrastructure
{
    [ApiController]
    [Route("api/towDriver")]
    public class TowDriverController(
        IdService<string> idService,
        IMessageBrokerService messageBrokerService,
        IEventStore eventStore,
        ITowDriverRepository towDriverRepository
    )
        : ControllerBase
    {
        private readonly IdService<string> _idService = idService;
        private readonly IMessageBrokerService _messageBrokerService = messageBrokerService;
        private readonly IEventStore _eventStore = eventStore;
        private readonly ITowDriverRepository _towDriverRepository = towDriverRepository;


        [HttpPost("create")]
        public async Task<ObjectResult> CreateTowDriver([FromBody] CreateTowDriverDto dto)
        {
            var command = new CreateTowDriverCommand(
                dto.towDriverName,
                dto.towDriverEmail,
                dto.licenseOwnerName,
                dto.licenseIssueDate,
                dto.licenseExpirationDate,
                dto.medicalCertificateOwnerName,
                dto.medicalCertificateAge,
                dto.medicalCertificateIssueDate,
                dto.medicalCertificateExpirationDate,
                dto.towDriverIdentificationNumber
            );

            var handler =
                new CreateTowDriverCommandHandler(
                    _eventStore,
                    _idService,
                    _towDriverRepository,
                    _messageBrokerService
                );

            var res = await handler.Execute(command);

            return Ok(res.Unwrap());
        }

        [HttpPatch("update")]
        public async Task<ObjectResult> UpdateTowDriver([FromBody] UpdateTowDriverDto dto)
        {
            var command = new UpdateTowDriverCommand(
                dto.towDriverId,
                dto.towDriverName,
                dto.towDriverEmail,
                dto.licenseOwnerName,
                dto.licenseIssueDate,
                dto.licenseExpirationDate,
                dto.medicalCertificateOwnerName,
                dto.medicalCertificateAge,
                dto.medicalCertificateIssueDate,
                dto.medicalCertificateExpirationDate,
                dto.towDriverIdentificationNumber
            );

            var handler =
                new UpdateTowDriverCommandHandler(
                    _eventStore,
                    _idService,
                    _towDriverRepository,
                    _messageBrokerService
                );

            var res = await handler.Execute(command);

            return Ok(res.Unwrap());
        }

        [HttpPatch("update/location")]
        public async Task<ObjectResult> UpdateTowDriverLocation([FromBody] UpdateTowDriverLocationDto dto)
        {
            var command = new UpdateTowDriverLocationCommand(
                dto.towDriverId,
                dto.towDriverLocation
            );

            var handler =
                new UpdateTowDriverLocationCommandHandler(
                    _eventStore,
                    _idService,
                    _towDriverRepository,
                    _messageBrokerService
                );

            var res = await handler.Execute(command);

            return Ok(res.Unwrap());
        }

        [HttpPatch("update/status")]
        public async Task<ObjectResult> UpdateTowDriverStatus([FromBody] UpdateTowDriverStatusDto dto)
        {
            var command = new UpdateTowDriverStatusCommand(
                dto.towDriverId,
                dto.towDriverStatus
            );

            var handler =
                new UpdateTowDriverStatusCommandHandler(
                    _eventStore,
                    _idService,
                    _towDriverRepository,
                    _messageBrokerService
                );

            var res = await handler.Execute(command);

            return Ok(res.Unwrap());
        }

    }
}
