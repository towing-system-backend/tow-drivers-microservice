using Application.Core;
using Microsoft.AspNetCore.Mvc;
using TowDriver.Application;
using TowDriver.Domain;

namespace TowDriver.Infrastructure
{
    [ApiController]
    [Route("api/towdriver")]
    public class TowDriverController(
        IdService<string> idService,
        Logger logger,
        IMessageBrokerService messageBrokerService,
        IEventStore eventStore,
        ITowDriverRepository towDriverRepository,
        IPerformanceLogsRepository performanceLogsRepository
    ): ControllerBase
    {
        private readonly IdService<string> _idService = idService;
        private readonly Logger _logger = logger;
        private readonly IMessageBrokerService _messageBrokerService = messageBrokerService;
        private readonly IEventStore _eventStore = eventStore;
        private readonly ITowDriverRepository _towDriverRepository = towDriverRepository;
        private readonly IPerformanceLogsRepository _performanceLogsRepository = performanceLogsRepository;

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ObjectResult> CreateTowDriver([FromBody] CreateTowDriverDto createTowDriverDto)
        {
            var command = new CreateTowDriverCommand(
                createTowDriverDto.Id,
                createTowDriverDto.SupplierCompanyId,
                createTowDriverDto.Name,
                createTowDriverDto.Email,
                createTowDriverDto.LicenseOwnerName,
                createTowDriverDto.LicenseIssueDate,
                createTowDriverDto.LicenseExpirationDate,
                createTowDriverDto.MedicalCertificateOwnerName,
                createTowDriverDto.MedicalCertificateAge,
                createTowDriverDto.MedicalCertificateIssueDate,
                createTowDriverDto.MedicalCertificateExpirationDate,
                createTowDriverDto.IdentificationNumber
            );

            var handler =
                new ExceptionCatcher<CreateTowDriverCommand, CreateTowDriverResponse>(
                    new PerfomanceMonitor<CreateTowDriverCommand, CreateTowDriverResponse>(
                        new LoggingAspect<CreateTowDriverCommand, CreateTowDriverResponse>(
                            new CreateTowDriverCommandHandler(_idService, _messageBrokerService, _eventStore, _towDriverRepository), _logger
                        ), _logger, _performanceLogsRepository, nameof(CreateTowDriverCommandHandler), "Write"
                    ), ExceptionParser.Parse
                );
            var res = await handler.Execute(command);

            return Ok(res.Unwrap());
        }

        [HttpPatch("update")]
        public async Task<ObjectResult> UpdateTowDriver([FromBody] UpdateTowDriverDto udpateTowDriverDto)
        {
            var command = new UpdateTowDriverCommand(
                udpateTowDriverDto.Id,
                udpateTowDriverDto.SupplierCompanyId,
                udpateTowDriverDto.Name,
                udpateTowDriverDto.Email,
                udpateTowDriverDto.LicenseOwnerName,
                udpateTowDriverDto.LicenseIssueDate,
                udpateTowDriverDto.LicenseExpirationDate,
                udpateTowDriverDto.MedicalCertificateOwnerName,
                udpateTowDriverDto.MedicalCertificateAge,
                udpateTowDriverDto.MedicalCertificateIssueDate,
                udpateTowDriverDto.MedicalCertificateExpirationDate,
                udpateTowDriverDto.IdentificationNumber
            );

            var handler =
                new ExceptionCatcher<UpdateTowDriverCommand, UpdateTowDriverResponse>(
                    new PerfomanceMonitor<UpdateTowDriverCommand, UpdateTowDriverResponse>(
                        new LoggingAspect<UpdateTowDriverCommand, UpdateTowDriverResponse>(
                            new UpdateTowDriverCommandHandler( _messageBrokerService, _eventStore, _towDriverRepository), _logger
                        ), _logger, _performanceLogsRepository, nameof(UpdateTowDriverCommandHandler), "Write"
                    ), ExceptionParser.Parse
                );
            var res = await handler.Execute(command);

            return Ok(res.Unwrap());
        }

        [HttpPatch("update/location")]
        public async Task<ObjectResult> UpdateTowDriverLocation([FromBody] UpdateTowDriverLocationDto updateTowDriverLocationDto)
        {
            var command = new UpdateTowDriverLocationCommand(
                updateTowDriverLocationDto.Id,
                updateTowDriverLocationDto.Location
            );

            var handler =
                new ExceptionCatcher<UpdateTowDriverLocationCommand, UpdateTowDriverLocationResponse>(
                    new PerfomanceMonitor<UpdateTowDriverLocationCommand, UpdateTowDriverLocationResponse>(
                        new LoggingAspect<UpdateTowDriverLocationCommand, UpdateTowDriverLocationResponse>(
                            new UpdateTowDriverLocationCommandHandler(_messageBrokerService, _eventStore, _towDriverRepository), _logger
                        ), _logger, _performanceLogsRepository, nameof(UpdateTowDriverLocationCommandHandler), "Write"
                    ), ExceptionParser.Parse
                );
            var res = await handler.Execute(command);

            return Ok(res.Unwrap());
        }

        [HttpPatch("update/status")]
        public async Task<ObjectResult> UpdateTowDriverStatus([FromBody] UpdateTowDriverStatusDto updateTowDriverStatusDto)
        {
            var command = new UpdateTowDriverStatusCommand(
                updateTowDriverStatusDto.Id,
                updateTowDriverStatusDto.Status
            );

            var handler =
                new ExceptionCatcher<UpdateTowDriverStatusCommand, UpdateTowDriverStatusResponse>(
                    new PerfomanceMonitor<UpdateTowDriverStatusCommand, UpdateTowDriverStatusResponse>(
                        new LoggingAspect<UpdateTowDriverStatusCommand, UpdateTowDriverStatusResponse>(
                            new UpdateTowDriverStatusCommandHandler(_messageBrokerService, _eventStore, _towDriverRepository), _logger
                    ), _logger, _performanceLogsRepository, nameof(UpdateTowDriverStatusCommandHandler), "Write"
                ), ExceptionParser.Parse
            );
            var res = await handler.Execute(command);

            return Ok(res.Unwrap());
        }

        [HttpGet("find/{Email}")]
        public async Task<ObjectResult> FindTowDriverByEmail(string Email)
        {
            var query = new FindTowDriverByEmailDto(Email);
            var handler = new FindTowDriverByEmailQuery();
            var res = await handler.Execute(query);

            return Ok(res.Unwrap());
        }

        [HttpGet("find/ActiveTowDriver")]
        public async Task<ObjectResult> FindActiveTowDriver()
        {
            var handler = new FindActiveTowDriversQuery();
            var res = await handler.Execute();

            return Ok(res.Unwrap());
        }

        [HttpGet("find/AllTowDriver")]
        public async Task<ObjectResult> FindAllTowDriver()
        {
            var handler = new FindAllTowDriversQuery();
            var res = await handler.Execute();

            return Ok(res.Unwrap());
        }
    }
}
