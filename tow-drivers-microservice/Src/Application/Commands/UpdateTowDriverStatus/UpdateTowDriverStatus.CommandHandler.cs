using Application.Core;
using TowDriver.Domain;

namespace TowDriver.Application
{
    public class UpdateTowDriverStatusCommandHandler
    (
        IMessageBrokerService messageBrokerService,
        IEventStore eventStore,
        ITowDriverRepository towDriverRepository
    ) : IService<UpdateTowDriverStatusCommand, UpdateTowDriverStatusResponse>
    {
        private readonly IEventStore _eventStore = eventStore;
        private readonly ITowDriverRepository _towDriverRepository = towDriverRepository;
        private readonly IMessageBrokerService _messageBrokerService = messageBrokerService;
        public async Task<Result<UpdateTowDriverStatusResponse>> Execute(UpdateTowDriverStatusCommand command)
        {
            var towDriverRegistred = await _towDriverRepository.FindById(command.TowDriverId);
            if (!towDriverRegistred.HasValue()) return Result<UpdateTowDriverStatusResponse>.MakeError(new TowDriverNotFound());
            var towDriver = towDriverRegistred.Unwrap();

            if(command.Status != null) towDriver.UpdateDriverStatus(new TowDriverStatus(command.Status));

            var events = towDriver.PullEvents();
            Console.WriteLine($"ESTOS SON LO EVENTOSSSSS::: {events[0].GetType()}");
            await _towDriverRepository.Save(towDriver);
            await _eventStore.AppendEvents(events);
            //await _messageBrokerService.Publish(events);

            return Result<UpdateTowDriverStatusResponse>.MakeSuccess(new UpdateTowDriverStatusResponse(towDriver.GetTowDriverId().GetValue()));
        }
    }
}