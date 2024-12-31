﻿using Application.Core;

namespace TowDrivers.Domain
{
    public class TowDriverDrivingLicense : IValueObject<TowDriverDrivingLicense>
    {
        private readonly string _ownerName;
        private readonly DateTime _issueDate;
        private readonly DateTime _expirationDate;

        public TowDriverDrivingLicense(string ownerName, DateTime issueDate, DateTime expirationDate)
        {
            if (string.IsNullOrWhiteSpace(ownerName) || ownerName.Length < 2 || ownerName.Length > 15)
            {
                throw new InvalidTowDriverNameException();
            }

            if (issueDate >= expirationDate)
            {
                throw new InvalidTowDriverDrivingLicenseDatesException();
            }
            _ownerName = ownerName;
            _issueDate = issueDate;
            _expirationDate = expirationDate;
        }

        public string GetOwnerName() => _ownerName;
        public DateTime GetIssueDate() => _issueDate;
        public DateTime GetExpirationDate() => _expirationDate;

        public bool Equals(TowDriverDrivingLicense other)
        {
            return _ownerName == other._ownerName &&
                   _issueDate == other._issueDate &&
                   _expirationDate == other._expirationDate;
        }
    }
}
