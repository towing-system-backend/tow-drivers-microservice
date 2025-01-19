using Application.Core;

namespace TowDriver.Domain
{
    public class SupplierCompanyIdUpdatedEvent(string publisherId, string type, SupplierCompanyIdUpdated context) : DomainEvent(publisherId, type, context) { }

    public class SupplierCompanyIdUpdated(string supplierCompanyId)
    {
        public readonly string SupplierCompanyId = supplierCompanyId;

        public static SupplierCompanyIdUpdatedEvent CreateEvent(TowDriverId publisherId, SupplierCompanyId supplierCompanyId)
        {
            return new SupplierCompanyIdUpdatedEvent(
                publisherId.GetValue(),
                typeof(SupplierCompanyIdUpdated).Name,
                new SupplierCompanyIdUpdated(
                    supplierCompanyId.GetValue()
                )
            );
        }
    }
}