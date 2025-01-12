using Application.Core;
using MassTransit;
using RabbitMQ.Contracts;

public class GetTowDriversListConsumer : IConsumer<FindAllTowDrivers>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IPublishEndpoint _publishEndpoint;

    public GetTowDriversListConsumer(IServiceProvider serviceProvider, IPublishEndpoint publishEnpoint)
    {
        _serviceProvider = serviceProvider;
        _publishEndpoint = publishEnpoint;
    }

    public async Task Consume(ConsumeContext<FindAllTowDrivers> context)
    {
        Console.WriteLine($"Recibiendo este es el evento que esta llegando: {context}");
        var message = context.Message;

        if (message == null)
        {
            Console.WriteLine("Error: El mensaje es nulo");
            return;
        }

        Console.WriteLine($"Recibiendo mensaje en TowDriverMicroservice {message.GetType().Name}");
        var res = new FindAllTowDriversMessageProcessor(_serviceProvider, _publishEndpoint).ProcessMessage(message);

        if (res == null)
        {
            Console.WriteLine("Error: La respuesta del procesador de mensajes es nula");
            return;
        }

        await context.RespondAsync(res);
    }
}
