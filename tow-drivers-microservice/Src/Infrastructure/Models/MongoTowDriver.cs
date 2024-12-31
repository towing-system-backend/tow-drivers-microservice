using MongoDB.Bson.Serialization.Attributes;

namespace TowDrivers.Infrastructure
{
    public class MongoTowDriver
    {
        [BsonId]
        public string TowDriverId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public MongoTowDriverDrivingLicense DrivingLicense { get; set; }
        public MongoTowDriverMedicalCertificate MedicalCertificate { get; set; }
        public string IdentificationNumber { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
    }

    public class MongoTowDriverDrivingLicense
    {
        public string OwnerName { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }

    public class MongoTowDriverMedicalCertificate
    {
        public string OwnerName { get; set; }
        public int OwnerAge { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
