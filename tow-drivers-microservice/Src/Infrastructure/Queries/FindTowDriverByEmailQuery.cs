using Application.Core;
using MongoDB.Driver;
using tow_drivers_microservice.Src.Infrastructure.Controllers.Dtos;
using tow_drivers_microservice.Src.Infrastructure.Controllers.Responses;
using TowDrivers.Domain;
using TowDrivers.Infrastructure;

namespace tow_drivers_microservice.Src.Infrastructure.Queries
{
    public class FindTowDriverByEmailQuery : IService<FindTowDriverByEmailDto, FindTowDriverByEmailResonse>
    {
        private readonly IMongoCollection<MongoTowDriver> _towDriverCollection;

        public FindTowDriverByEmailQuery()
        {
            MongoClient client = new MongoClient(Environment.GetEnvironmentVariable("CONNECTION_URI"));
            IMongoDatabase database = client.GetDatabase(Environment.GetEnvironmentVariable("DATABASE_NAME"));
            _towDriverCollection = database.GetCollection<MongoTowDriver>("tow-drivers");
        }

        public async Task<Result<FindTowDriverByEmailResonse>> Execute(FindTowDriverByEmailDto query)
        {
            var filter = Builders<MongoTowDriver>.Filter.Eq(towDriver => towDriver.Email, query.Email);
            var res = await _towDriverCollection.Find(filter).FirstOrDefaultAsync();

            if (res == null) return Result<FindTowDriverByEmailResonse>.MakeError(new TowDriverNotFoundError());

            return Result<FindTowDriverByEmailResonse>.MakeSuccess(
                new FindTowDriverByEmailResonse(
                    res.TowDriverId,
                    res.Name,
                    res.Email,
                    res.DrivingLiceseOwnerName,
                    res.DrivingLicenseIssueDate,
                    res.DrivingLicenseExpirationDate,
                    res.MedicalCertificateOwnerName,
                    res.MedicalCertificateOwnerAge,
                    res.MedicalCertificateIssueDate,
                    res.MedicalCertificateExpirationDate,
                    res.IdentificationNumber,
                    res.Location,
                    res.Status
                )
            );
        }
    }
}
