using Application.Core;
using TowDriver.Domain;

namespace TowDriver.Application
{
    public class UpdateTowDriverLocationCommandHandler
    (
        IMessageBrokerService messageBrokerService,
        IEventStore eventStore,
        ITowDriverRepository towDriverRepository
    ) : IService<UpdateTowDriverLocationCommand, UpdateTowDriverLocationResponse>
    {
        private readonly IEventStore _eventStore = eventStore;
        private readonly ITowDriverRepository _towDriverRepository = towDriverRepository;
        private readonly IMessageBrokerService _messageBrokerService = messageBrokerService;
        public async Task<Result<UpdateTowDriverLocationResponse>> Execute(UpdateTowDriverLocationCommand command)
        {
            var towDriverRegistred = await _towDriverRepository.FindById(command.TowDriverId);
            if (towDriverRegistred == null) return Result<UpdateTowDriverLocationResponse>.MakeError(new TowDriverNotFound());
            var towDriver = towDriverRegistred.Unwrap();

            if (command.Location != null) 
                towDriver.UpdateDriverLocation(new TowDriverLocation(command.Location));

            var events = towDriver.PullEvents();
            await _towDriverRepository.Save(towDriver);
            await _eventStore.AppendEvents(events);
            await _messageBrokerService.Publish(events);

            return Result<UpdateTowDriverLocationResponse>.MakeSuccess(new UpdateTowDriverLocationResponse(towDriver.GetTowDriverId().GetValue()));
        }
    }
}
