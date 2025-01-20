using MassTransit;
using RabbitMQ.Contracts;

namespace Application.Core
{
    public class CreateTowDriverConsumer(IServiceProvider serviceProvider) : IConsumer<CreateTowDriver>
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        public Task Consume(ConsumeContext<CreateTowDriver> @event)
        {
            var message = @event.Message;
            new MessageProcessor(_serviceProvider).ProcessMessage(message);

            return Task.CompletedTask;
        }
    }
}