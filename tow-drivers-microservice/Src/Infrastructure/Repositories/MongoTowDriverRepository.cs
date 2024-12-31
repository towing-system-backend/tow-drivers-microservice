﻿using Application.Core;
using MassTransit.Transports;
using MongoDB.Driver;
using TowDrivers.Domain;
using IOptional = Application.Core.Optional<TowDrivers.Domain.TowDriver>;

namespace TowDrivers.Infrastructure
{
    public class MongoTowDriverRepository : ITowDriverRepository
    {
        private readonly IMongoCollection<MongoTowDriver> _towDriverCollection;
        public MongoTowDriverRepository()
        {
            MongoClient client = new MongoClient(Environment.GetEnvironmentVariable("CONNECTION_URI"));
            IMongoDatabase database = client.GetDatabase(Environment.GetEnvironmentVariable("DATABASE_NAME"));
            _towDriverCollection = database.GetCollection<MongoTowDriver>("tow-drivers");
        }
        

        public async Task<IOptional> FindByEmail(string email)
        {
            var filter = Builders<MongoTowDriver>.Filter.Eq(towDriver => towDriver.Email, email);
            var res = await _towDriverCollection.Find(filter).FirstOrDefaultAsync();

            if (res == null) return IOptional.Empty();

            return IOptional.Of(
            Domain.TowDriver.Create(
                new TowDriverId(res.TowDriverId),
                new TowDriverName(res.Name),
                new TowDriverEmail(res.Email),
                new TowDriverDrivingLicense(
                    res.DrivingLiceseOwnerName,
                    res.DrivingLicenseIssueDate,
                    res.DrivingLicenseExpirationDate
                ),
                new TowDriverMedicalCertificate(
                    res.MedicalCertificateOwnerName,
                    res.MedicalCertificateOwnerAge,
                    res.MedicalCertificateIssueDate,
                    res.MedicalCertificateExpirationDate
                ),
                new TowDriverIdentificationNumber(res.IdentificationNumber),
                new TowDriverLocation(res.Location!),
                new TowDriverStatus(res.Status!)
            ));
        }

        public async Task<IOptional> FindById(string towDriverId)
        {
            var filter = Builders<MongoTowDriver>.Filter.Eq(towDriver => towDriver.TowDriverId, towDriverId);
            var res = await _towDriverCollection.Find(filter).FirstOrDefaultAsync();

            if (res == null) return IOptional.Empty();

            return IOptional.Of(
            Domain.TowDriver.Create(
                new TowDriverId(res.TowDriverId),
                new TowDriverName(res.Name),
                new TowDriverEmail(res.Email),
                new TowDriverDrivingLicense(
                    res.DrivingLiceseOwnerName,
                    res.DrivingLicenseIssueDate,
                    res.DrivingLicenseExpirationDate
                ),
                new TowDriverMedicalCertificate(
                    res.MedicalCertificateOwnerName,
                    res.MedicalCertificateOwnerAge,
                    res.MedicalCertificateIssueDate,
                    res.MedicalCertificateExpirationDate
                ),
                new TowDriverIdentificationNumber(res.IdentificationNumber),
                new TowDriverLocation(res.Location!),
                new TowDriverStatus(res.Status!)
            ));
        }

        public async Task Remove(string towDriverId)
        {
            var filter = Builders<MongoTowDriver>.Filter.Eq(driver => driver.TowDriverId, towDriverId);
            var result = await _towDriverCollection.DeleteOneAsync(filter);
        }

        public async Task Save(Domain.TowDriver towDriver)
        {
            var filter = Builders<MongoTowDriver>.Filter
                .Eq(driver => driver.TowDriverId, towDriver.GetTowDriverId().GetValue());


            var update = Builders<MongoTowDriver>.Update
                .Set(driver => driver.Name, towDriver.GetDriverName().GetValue())
                .Set(driver => driver.Email, towDriver.GetDriverEmail().GetValue())
                .Set(driver => driver.DrivingLiceseOwnerName, towDriver.GetTowDriverDrivingLicense().GetOwnerName())
                .Set(driver => driver.DrivingLicenseIssueDate, towDriver.GetTowDriverDrivingLicense().GetIssueDate())
                .Set(driver => driver.DrivingLicenseExpirationDate, towDriver.GetTowDriverDrivingLicense().GetExpirationDate())
                .Set(driver => driver.MedicalCertificateOwnerName, towDriver.GetTowDriverMedicalCertificate().GetOwnerName())
                .Set(driver => driver.MedicalCertificateOwnerAge, towDriver.GetTowDriverMedicalCertificate().GetOwnerAge())
                .Set(driver => driver.MedicalCertificateIssueDate, towDriver.GetTowDriverMedicalCertificate().GetIssueDate())
                .Set(driver => driver.MedicalCertificateExpirationDate, towDriver.GetTowDriverMedicalCertificate().GetExpirationDate())
                .Set(driver => driver.IdentificationNumber, towDriver.GetDriverIdentificationNumber().GetValue())
                .Set(driver => driver.Location, towDriver.GetDriverLocation()?.GetValue())
                .Set(driver => driver.Status, towDriver.GetTowDriverStatus().GetValue() ?? "Inactive");


            await _towDriverCollection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
        }


    }
}
