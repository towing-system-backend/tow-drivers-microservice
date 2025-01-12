using TowDriver.Infrastructure;

namespace RabbitMQ.Contracts
{
    public interface IRabbitMQMessage { };

    public record EventType(
        string PublisherId,
        string Type,
        object Context,
        DateTime OcurredDate
    );

    public record FindAllTowDrivers() : IRabbitMQMessage;

    public record FindAllTowDriversQueryResponse(List<FindAllTowDriversResponse> TowDrivers) : IRabbitMQMessage;
}