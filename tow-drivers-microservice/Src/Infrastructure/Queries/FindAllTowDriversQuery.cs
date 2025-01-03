using Application.Core;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using tow_drivers_microservice.Src.Infrastructure.Controllers.Responses;
using TowDrivers.Domain;
using TowDrivers.Infrastructure;

namespace tow_drivers_microservice.Src.Infrastructure.Queries
{
    public class FindAllTowDriversQuery : IService<Object, List<FindAllTowDriversResponse>>
    {
        private readonly IMongoCollection<MongoTowDriver> _towDriverCollection;
        public FindAllTowDriversQuery()
        {
            MongoClient client = new MongoClient(Environment.GetEnvironmentVariable("CONNECTION_URI"));
            IMongoDatabase database = client.GetDatabase(Environment.GetEnvironmentVariable("DATABASE_NAME"));
            _towDriverCollection = database.GetCollection<MongoTowDriver>("tow-drivers");
        }
        public async Task<Result<List<FindAllTowDriversResponse>>> Execute(object data)
        {
            var filter = Builders<MongoTowDriver>.Filter.Empty;
            var res = await _towDriverCollection.Find(filter).ToListAsync();

            if (res.IsNullOrEmpty()) return Result<List<FindAllTowDriversResponse>>.MakeError(new TowDriversNotFoundError());

            var towDrivers = res.Select(
                towDriver => new FindAllTowDriversResponse(
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

            return Result<List<FindAllTowDriversResponse>>.MakeSuccess(towDrivers);

        }
    }
}
