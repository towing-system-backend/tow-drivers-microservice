using MongoDB.Driver;
using Application.Core;
using TowDriver.Application;

namespace TowDriver.Infrastructure
{
    public class FindTowDriversNameQuery 
    {
        private readonly IMongoCollection<MongoTowDriver> _userCollection;
        public FindTowDriversNameQuery()
        {
            MongoClient client = new MongoClient(Environment.GetEnvironmentVariable("CONNECTION_URI_READ_MODELS"));
            IMongoDatabase database = client.GetDatabase(Environment.GetEnvironmentVariable("DATABASE_NAME_READ_MODELS"));
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