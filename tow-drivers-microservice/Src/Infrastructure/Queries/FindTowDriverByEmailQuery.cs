using Application.Core;
using MongoDB.Driver;
using TowDriver.Application;
using TowDriver.Domain;

namespace TowDriver.Infrastructure
{
    public class FindTowDriverByEmailQuery : IService<FindTowDriverByEmailDto, FindTowDriverByEmailResponse>
    {
        private readonly IMongoCollection<MongoTowDriver> _towDriverCollection;

        public FindTowDriverByEmailQuery()
        {
            MongoClient client = new MongoClient(Environment.GetEnvironmentVariable("CONNECTION_URI_READ_MODELS"));
            IMongoDatabase database = client.GetDatabase(Environment.GetEnvironmentVariable("DATABASE_NAME_READ_MODELS"));
            _towDriverCollection = database.GetCollection<MongoTowDriver>("tow-drivers");
        }

        public async Task<Result<FindTowDriverByEmailResponse>> Execute(FindTowDriverByEmailDto query)
        {
            var filter = Builders<MongoTowDriver>.Filter.Eq(towDriver => towDriver.Email, query.Email);
            var res = await _towDriverCollection.Find(filter).FirstOrDefaultAsync();

            if (res == null) return Result<FindTowDriverByEmailResponse>.MakeError(new TowDriversNotFound());

            return Result<FindTowDriverByEmailResponse>.MakeSuccess(
                new FindTowDriverByEmailResponse(
                    res.TowDriverId,
                    res.Name,
                    res.Email,
                    res.DrivingLicenseOwnerName,
                    res.DrivingLicenseIssueDate,
                    res.DrivingLicenseExpirationDate,
                    res.MedicalCertificateOwnerName,
                    res.MedicalCertificateOwnerAge,
                    res.MedicalCertificateIssueDate,
                    res.MedicalCertificateExpirationDate,
                    res.IdentificationNumber,
                    res.Location,
                    res.Status,
                    res.TowAssigned
                )
            );
        }
    }
}
