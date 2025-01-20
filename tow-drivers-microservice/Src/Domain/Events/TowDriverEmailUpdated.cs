using Application.Core;

namespace TowDriver.Domain
{
    public class TowDriverEmailUpdatedEvent(string publisherId, string type, TowDriverEmailUpdated context) : DomainEvent(publisherId, type, context) { }

    public class TowDriverEmailUpdated(string email)
    {
        public readonly string TowDriverEmail = email;

        static public TowDriverEmailUpdatedEvent CreateEvent(TowDriverId publisherId, TowDriverEmail email)
        {
            return new TowDriverEmailUpdatedEvent(
                publisherId.GetValue(),
                typeof(TowDriverEmailUpdated).Name,
                new TowDriverEmailUpdated(
                    email.GetValue()
                )
            );
        }
    }
}
