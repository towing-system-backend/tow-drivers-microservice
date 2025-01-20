using Application.Core;

namespace TowDriver.Domain
{
    public class TowDriverIdentificationNumberUpdatedEvent(string publisherId, string type, TowDriverIdentificationNumberUpdated context) : DomainEvent(publisherId, type, context) { }

    public class TowDriverIdentificationNumberUpdated(int identificationNumber)
    {
        public readonly int TowDriverIdentificationNumber = identificationNumber;
    
        static public TowDriverIdentificationNumberUpdatedEvent CreateEvent(TowDriverId publisherId, TowDriverIdentificationNumber identificationNumber)
        {
            return new TowDriverIdentificationNumberUpdatedEvent(
                publisherId.GetValue(),
                typeof(TowDriverIdentificationNumberUpdated).Name,
                new TowDriverIdentificationNumberUpdated(
                    identificationNumber.GetValue()
                )
            );
        }
    }
}