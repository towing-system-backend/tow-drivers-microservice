using Application.Core;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using TowDriver.Application;

namespace TowDriver.Infrastructure
{
    public class FindActiveTowDriversQuery
    {
        private readonly IMongoCollection<MongoTowDriver> _towDriverCollection;
        public FindActiveTowDriversQuery()
        {
            MongoClient client = new MongoClient(Environment.GetEnvironmentVariable("CONNECTION_URI_READ_MODELS"));
            IMongoDatabase database = client.GetDatabase(Environment.GetEnvironmentVariable("DATABASE_NAME_READ_MODELS"));
            _towDriverCollection = database.GetCollection<MongoTowDriver>("tow-drivers");
        }

        public async Task<Result<List<FindActiveTowDriversResponse>>> Execute()
        {
            var filter = Builders<MongoTowDriver>.Filter.Eq(towDriver => towDriver.Status, "Active");
            var res = await _towDriverCollection.Find(filter).ToListAsync();

            if (res.IsNullOrEmpty()) return Result<List<FindActiveTowDriversResponse>>.MakeError(new TowDriverNotFound());

            var towDrivers = res.Select(
                towDriver => new FindActiveTowDriversResponse(
                    towDriver.TowDriverId,
                    towDriver.Name,
                    towDriver.Email,
                    towDriver.DrivingLicenseOwnerName,
                    towDriver.DrivingLicenseIssueDate,
                    towDriver.DrivingLicenseExpirationDate,
                    towDriver.MedicalCertificateOwnerName,
                    towDriver.MedicalCertificateOwnerAge,
                    towDriver.MedicalCertificateIssueDate,
                    towDriver.MedicalCertificateExpirationDate,
                    towDriver.IdentificationNumber,
                    towDriver.Location!,
                    towDriver.Status!,
                    towDriver.TowAssigned
                )
            ).ToList();

            return Result<List<FindActiveTowDriversResponse>>.MakeSuccess(towDrivers);
        }
    }
}
