using Application.Core;
using MongoDB.Driver;
using TowDriver.Application;

namespace TowDriver.Infrastructure
{
    public class FindTowDriverByIdQuery : IService<FindTowDriverByIdDto, FindTowDriverByIdResponse>
    {
        private readonly IMongoCollection<MongoTowDriver> _towDriverCollection;

        public FindTowDriverByIdQuery()
        {
            MongoClient client = new MongoClient(Environment.GetEnvironmentVariable("CONNECTION_URI_READ_MODELS"));
            IMongoDatabase database = client.GetDatabase(Environment.GetEnvironmentVariable("DATABASE_NAME_READ_MODELS"));
            _towDriverCollection = database.GetCollection<MongoTowDriver>("tow-drivers");
        }

        public async Task<Result<FindTowDriverByIdResponse>> Execute(FindTowDriverByIdDto query)
        {
            var filter = Builders<MongoTowDriver>.Filter.Eq(towDriver => towDriver.TowDriverId, query.Id);
            var res = await _towDriverCollection.Find(filter).FirstOrDefaultAsync();

            if (res == null) return Result<FindTowDriverByIdResponse>.MakeError(new TowDriversNotFound());

            return Result<FindTowDriverByIdResponse>.MakeSuccess(
                new FindTowDriverByIdResponse(
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