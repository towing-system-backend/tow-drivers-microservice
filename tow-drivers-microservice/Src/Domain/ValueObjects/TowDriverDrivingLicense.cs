using Application.Core;

namespace TowDriver.Domain
{
    public class TowDriverDrivingLicense : IValueObject<TowDriverDrivingLicense>
    {
        private readonly string _ownerName;
        private readonly DateOnly _issueDate;
        private readonly DateOnly _expirationDate;

        public TowDriverDrivingLicense(string ownerName, DateOnly issueDate, DateOnly expirationDate)
        {
            if (string.IsNullOrWhiteSpace(ownerName) || ownerName.Length < 2 || ownerName.Length > 64)
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
        public DateOnly GetIssueDate() => _issueDate;
        public DateOnly GetExpirationDate() => _expirationDate;
        public bool Equals(TowDriverDrivingLicense other) => _ownerName == other._ownerName && _issueDate == other._issueDate && _expirationDate == other._expirationDate;
    }
}
