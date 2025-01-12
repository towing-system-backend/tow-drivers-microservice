using MassTransit;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Contracts;
using System.Reflection;
using TowDriver.Infrastructure;

public class FindAllTowDriversMessageProcessor
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IPublishEndpoint _publishEndpoint;

    public FindAllTowDriversMessageProcessor(IServiceProvider serviceProvider, IPublishEndpoint publishEndpoint)
    {
        _serviceProvider = serviceProvider;
        _publishEndpoint = publishEndpoint;
    }

    public FindAllTowDriversQueryResponse? ProcessMessage(IRabbitMQMessage message)
    {
        Console.WriteLine("Estamos en el procesador de mensajes de towdrivermicroservice");

        var controller = _serviceProvider.GetRequiredService<TowDriverController>();
        var method = controller.GetType().GetMethod("FindAllTowDriver", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

        if (method == null)
        {
            Console.WriteLine("Error: Método no encontrado");
            return null;
        }

        Console.WriteLine("Fino, si encontramos el metodo");

        try
        {
            var methodResponse = (Task<ObjectResult>)method.Invoke(controller, null)!;
            methodResponse.Wait();

            Console.WriteLine($"Esta es la respuestaa: {methodResponse.Result}");

            if (methodResponse.Result == null)
            {
                Console.WriteLine("Error: El resultado del método es nulo");
                return null;
            }

            var aux = (List<FindAllTowDriversResponse>?)methodResponse.Result.Value;

            if (aux == null)
            {
                Console.WriteLine("Error: El valor del resultado es nulo");
                return null;
            }

            var res = new FindAllTowDriversQueryResponse(aux);
            return res;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return null;
    }
}
