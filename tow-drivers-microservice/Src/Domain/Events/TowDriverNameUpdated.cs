using Application.Core;

namespace TowDriver.Domain
{
    public class TowDriverNameUpdatedEvent(string publisherId, string type, TowDriverNameUpdated context) : DomainEvent(publisherId, type, context) { }

    public class TowDriverNameUpdated(string name)
    {
        public readonly string TowDriverName = name;

        static public TowDriverNameUpdatedEvent CreateEvent(TowDriverId publisherId, TowDriverName name)
        {
            return new TowDriverNameUpdatedEvent(
                publisherId.GetValue(),
                typeof(TowDriverNameUpdated).Name,
                new TowDriverNameUpdated(
                    name.GetValue()
                )
            );
        }
    }
}
