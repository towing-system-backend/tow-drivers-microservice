using Application.Core;

namespace TowDriver.Domain
{
    public class TowDriverLocationUpdatedEvent(string publisherId, string type, TowDriverLocationUpdated context) : DomainEvent(publisherId, type, context) { }

    public class TowDriverLocationUpdated(string location)
    {
        public readonly string TowDriverLocation = location;

        static public TowDriverLocationUpdatedEvent CreateEvent(TowDriverId publisherId, TowDriverLocation location)
        {
            return new TowDriverLocationUpdatedEvent(
                publisherId.GetValue(),
                typeof(TowDriverLocationUpdated).Name,
                new TowDriverLocationUpdated(
                    location.GetValue()
                )
            );
        }
    }
}
