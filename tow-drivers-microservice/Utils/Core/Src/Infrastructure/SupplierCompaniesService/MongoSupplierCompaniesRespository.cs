
using MongoDB.Driver;
using Order.Infrastructure;
using TowDriver.Domain;

namespace Application.Core
{
    using TowDriver.Domain;
    public class MongoSupplierCompaniesRespository : ISupplierCompanyRespository
    {
        private readonly IMongoCollection<MongoSupplierCompany> _supplierCompanyCollection;
        private readonly IMongoCollection<MongoSupplierCompany> _supplierCompanyCollectionReadModels;

        public MongoSupplierCompaniesRespository() 
        {
            var writeClient = new MongoClient(Environment.GetEnvironmentVariable("CONNECTION_URI"));
            var supplierCompanyDatabase = writeClient.GetDatabase(Environment.GetEnvironmentVariable("DATABASE_NAME"));
            _supplierCompanyCollection = supplierCompanyDatabase.GetCollection<MongoSupplierCompany>("supplier-companies");

            var readClient = new MongoClient(Environment.GetEnvironmentVariable("CONNECTION_URI_READ_MODELS"));
            var supplierCompanyReadModels = readClient.GetDatabase(Environment.GetEnvironmentVariable("DATABASE_NAME_READ_MODELS"));
            _supplierCompanyCollectionReadModels = supplierCompanyReadModels.GetCollection<MongoSupplierCompany>("supplier-companies");
        }

        public async Task SaveTowDriver(TowDriver driver)
        {
            var filter = Builders<MongoSupplierCompany>.Filter.Eq(supplierCompany => supplierCompany.SupplierCompanyId, driver.GetSupplierCompanyId().GetValue());
            var update = Builders<MongoSupplierCompany>.Update.Push(supplierCompany => supplierCompany.TowDrivers, driver.GetTowDriverId().GetValue());

            await _supplierCompanyCollection.UpdateOneAsync(filter, update);
            await _supplierCompanyCollectionReadModels.UpdateOneAsync(filter, update);
        }
    }
}
