using Application.Core;
using TowDriver.Domain;

namespace TowDriver.Domain
{
    public class TowDriverTowAssignedUpdatedEvent(string publisherId, string type, TowDriverTowAssignedUpdated context) : DomainEvent(publisherId, type, context) { }

    public class TowDriverTowAssignedUpdated(string towDriverTowAssigned)
    {
        public readonly string TowDriverTowAssigned = towDriverTowAssigned;

        public static TowDriverTowAssignedUpdatedEvent CreateEvent(TowDriverId PublisherId, TowDriverTowAssigned towDriverTowAssigned) 
        {
            return new TowDriverTowAssignedUpdatedEvent(
                PublisherId.GetValue(),
                typeof(TowDriverTowAssignedUpdated).Name,
                new TowDriverTowAssignedUpdated(towDriverTowAssigned.GetValue())
            );
        }
    }
}
