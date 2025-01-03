using Application.Core;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using tow_drivers_microservice.Src.Infrastructure.Controllers.Responses;
using TowDrivers.Domain;
using TowDrivers.Infrastructure;

namespace tow_drivers_microservice.Src.Infrastructure.Queries
{
    public class FindActiveTowDriversQuery : IService<object, List<FindActiveTowDriversResponse>>
    {
        private readonly IMongoCollection<MongoTowDriver> _towDriverCollection;
        public FindActiveTowDriversQuery()
        {
            MongoClient client = new MongoClient(Environment.GetEnvironmentVariable("CONNECTION_URI"));
            IMongoDatabase database = client.GetDatabase(Environment.GetEnvironmentVariable("DATABASE_NAME"));
            _towDriverCollection = database.GetCollection<MongoTowDriver>("tow-drivers");
        }

        public async Task<Result<List<FindActiveTowDriversResponse>>> Execute(object data)
        {
            var filter = Builders<MongoTowDriver>.Filter.Eq(towDriver => towDriver.Status, "Active");
            var res = await _towDriverCollection.Find(filter).ToListAsync();

            if (res.IsNullOrEmpty()) return Result<List<FindActiveTowDriversResponse>>.MakeError(new TowDriversNotFoundError());

            var towDrivers = res.Select(
                towDriver => new FindActiveTowDriversResponse(
                    towDriver.TowDriverId,
                    towDriver.Name,
                    towDriver.Email,
                    towDriver.DrivingLiceseOwnerName,
                    towDriver.DrivingLicenseIssueDate,
                    towDriver.DrivingLicenseExpirationDate,
                    towDriver.MedicalCertificateOwnerName,
                    towDriver.MedicalCertificateOwnerAge,
                    towDriver.MedicalCertificateIssueDate,
                    towDriver.MedicalCertificateExpirationDate,
                    towDriver.IdentificationNumber,
                    towDriver.Location!,
                    towDriver.Status!
                )
            ).ToList();

            return Result<List<FindActiveTowDriversResponse>>.MakeSuccess(towDrivers);
        }
    }
}
