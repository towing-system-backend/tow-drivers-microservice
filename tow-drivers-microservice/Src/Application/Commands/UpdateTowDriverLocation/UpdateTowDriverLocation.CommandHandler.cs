using Application.Core;
using MongoDB.Driver;
using tow_drivers_microservice.Src.Application.Commands.UpdateTowDriverLocation.Types;
using TowDrivers.Application;
using TowDrivers.Domain;

namespace tow_drivers_microservice.Src.Application.Commands.UpdateTowDriverLocation
{
    public class UpdateTowDriverLocationCommandHandler
    (
        IEventStore eventStore,
        IdService<string> idService,
        ITowDriverRepository towDriverRepository,
        IMessageBrokerService messageBrokerService
    ) : IService<UpdateTowDriverLocationCommand, UpdateTowDriverLocationResponse>
    {
        private readonly IEventStore _eventStore = eventStore;
        private readonly IdService<string> _idService = idService;
        private readonly ITowDriverRepository _towDriverRepository = towDriverRepository;
        private readonly IMessageBrokerService _messageBrokerService = messageBrokerService;
        public async Task<Result<UpdateTowDriverLocationResponse>> Execute(UpdateTowDriverLocationCommand command)
        {
            var towDriverRegistred = await _towDriverRepository.FindById(command.towDriverId);
            if (towDriverRegistred == null) Result<UpdateTowDriverResponse>.MakeError(new TowDriverNotFoundError());
            var towDriver = towDriverRegistred.Unwrap();

            if (command.location != null) 
                towDriver.UpdateDriverLocation(new TowDriverLocation(command.location));

            var events = towDriver.PullEvents();
            await _towDriverRepository.Save(towDriver);
            await _eventStore.AppendEvents(events);
            await _messageBrokerService.Publish(events);

            return Result<UpdateTowDriverLocationResponse>.MakeSuccess(new UpdateTowDriverLocationResponse(towDriver.GetTowDriverId().GetValue()));
        }
    }
}
