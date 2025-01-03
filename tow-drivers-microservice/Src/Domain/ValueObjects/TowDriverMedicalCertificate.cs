using Application.Core;

namespace TowDrivers.Domain
{
    public class TowDriverMedicalCertificate : IValueObject<TowDriverMedicalCertificate>
    {
        private readonly string _ownerName;
        private readonly int _ownerAge;
        private readonly DateOnly _issueDate;
        private readonly DateOnly _expirationDate;

        public TowDriverMedicalCertificate(string ownerName, int ownerAge, DateOnly issueDate, DateOnly expirationDate)
        {
            if (string.IsNullOrWhiteSpace(ownerName) || ownerName.Length < 2 || ownerName.Length > 15)
            {
                throw new InvalidTowDriverNameException();
            }

            if (ownerAge <= 15)
            {
                throw new InvalidTowDriverOwnerAgeException();
            }

            if (issueDate >= expirationDate)
            {
                throw new InvalidTowDriverMedicalCertificateDatesException();
            }
            _ownerName = ownerName;
            _ownerAge = ownerAge;
            _issueDate = issueDate;
            _expirationDate = expirationDate;
        }
        public string GetOwnerName() => _ownerName;
        public int GetOwnerAge() => _ownerAge;
        public DateOnly GetIssueDate() => _issueDate;
        public DateOnly GetExpirationDate() => _expirationDate;

        public bool Equals(TowDriverMedicalCertificate other)
        {
            return _ownerName == other._ownerName &&
                   _ownerAge == other._ownerAge &&
                   _issueDate == other._issueDate &&
                   _expirationDate == other._expirationDate;
        }
    }
}
