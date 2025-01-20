using MongoDB.Driver;
using Application.Core;
using TowDriver.Application;

namespace TowDriver.Infrastructure
{
    public class FindTowDriversNameQuery : IService<string, List<FindTowDriversNameResponse>>
    {
        private readonly IMongoCollection<MongoTowDriver> _userCollection;
        public FindTowDriversNameQuery()
        {
            MongoClient client = new MongoClient(Environment.GetEnvironmentVariable("CONNECTION_URI"));
            IMongoDatabase database = client.GetDatabase(Environment.GetEnvironmentVariable("DATABASE_NAME"));
            _userCollection = database.GetCollection<MongoTowDriver>("tow-drivers");
        }

        public async Task<Result<List<FindTowDriversNameResponse>>> Execute(string param)
        {
            var filter = Builders<MongoTowDriver>.Filter.Empty;
            var res = await _userCollection.Find(filter).ToListAsync();

            if (res == null) return Result<List<FindTowDriversNameResponse>>.MakeError(new TowDriverNotFound());

            var towDrivers = res.Select(
                towDriver => new FindTowDriversNameResponse(
                    towDriver.TowDriverId,
                    towDriver.Name
                )
            ).ToList();

            return Result<List<FindTowDriversNameResponse>>.MakeSuccess(towDrivers);
        }
    }
}