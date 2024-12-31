using Application.Core;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json.Nodes;
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

    }
}
