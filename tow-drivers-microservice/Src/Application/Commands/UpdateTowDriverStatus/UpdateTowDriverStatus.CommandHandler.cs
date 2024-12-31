using Application.Core;
using MongoDB.Driver;
using tow_drivers_microservice.Src.Application.Commands.UpdateTowDriverStatus.Types;
using TowDrivers.Application;
using TowDrivers.Domain;

namespace tow_drivers_microservice.Src.Application.Commands.UpdateTowDriverStatus
{
    public class UpdateTowDriverStatusCommandHandler
    (
        IEventStore eventStore,
        IdService<string> idService,
        ITowDriverRepository towDriverRepository,
        IMessageBrokerService messageBrokerService
    ) : IService<UpdateTowDriverStatusCommand, UpdateTowDriverStatusResponse>
    {
        private readonly IEventStore _eventStore = eventStore;
        private readonly IdService<string> _idService = idService;
        private readonly ITowDriverRepository _towDriverRepository = towDriverRepository;
        private readonly IMessageBrokerService _messageBrokerService = messageBrokerService;
        public async Task<Result<UpdateTowDriverStatusResponse>> Execute(UpdateTowDriverStatusCommand command)
        {
            var towDriverRegistred = await _towDriverRepository.FindById(command.towDriverId);
            if (towDriverRegistred == null) Result<UpdateTowDriverResponse>.MakeError(new TowDriverNotFoundError());
            var towDriver = towDriverRegistred.Unwrap();

            if(command.status != null)
                towDriver.UpdateDriverStatus(new TowDriverStatus(command.status));

            var events = towDriver.PullEvents();
            await _towDriverRepository.Save(towDriver);
            await _eventStore.AppendEvents(events);
            await _messageBrokerService.Publish(events);

            return Result<UpdateTowDriverStatusResponse>.MakeSuccess(new UpdateTowDriverStatusResponse(towDriver.GetTowDriverId().GetValue()));
        }
    }
}
