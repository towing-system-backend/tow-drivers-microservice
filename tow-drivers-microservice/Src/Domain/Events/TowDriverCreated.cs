using Application.Core;

namespace TowDrivers.Domain 
{
    public class TowDriverCreatedEvent(string publisherId, string type, TowDriverCreated context) : DomainEvent(publisherId, type, context) { }

    public class TowDriverCreated() { }
    
    
}
