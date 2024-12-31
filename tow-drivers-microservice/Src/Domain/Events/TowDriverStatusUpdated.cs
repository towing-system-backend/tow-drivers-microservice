using Application.Core;

namespace TowDrivers.Domain
{
    public class TowDriverStatusUpdatedEvent(string publisherId, string type, TowDriverStatusUpdated context) : DomainEvent(publisherId, type, context) { }

    public class TowDriverStatusUpdated(string status)
    {
        public readonly string Status = status;

        static public TowDriverStatusUpdatedEvent CreateEvent(TowDriverId publisherId, TowDriverStatus status)
        {
            return new TowDriverStatusUpdatedEvent(
                publisherId.GetValue(),
                typeof(TowDriverStatusUpdated).Name,
                new TowDriverStatusUpdated(
                    status.GetValue()
                )
            );
        }
    }
}
