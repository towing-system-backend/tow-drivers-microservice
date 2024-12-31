using Application.Core;

namespace TowDrivers.Domain
{
    public class TowDriver : AggregateRoot<TowDriverId>
    {
        private new TowDriverId _towDriverId;
        private TowDriverName _towDriverName;
        private TowDriverEmail _towDriverEmail;
        private TowDriverDrivingLicense _towDriverDrivingLicense;
        private TowDriverMedicalCertificate _towDriverMedicalCertificate;
        private TowDriverIdentificationNumber _towDriverIdentificationNumber;
        private TowDriverLocation? _towDriverLocation;
        private TowDriverStatus _towDriverStatus;

        public TowDriver(TowDriverId towDriverId) : base(towDriverId)
        {
            _towDriverId = towDriverId;
        }
        public override void ValidateState()
        {
            if (
                _towDriverId == null ||
                _towDriverName == null ||
                _towDriverEmail == null ||
                _towDriverDrivingLicense == null ||
                _towDriverMedicalCertificate == null ||
                _towDriverIdentificationNumber == null ||
                _towDriverStatus == null)
            {
                throw new InvalidTowDriverException();
            }
        }
        public TowDriverId GetTowDriverId() => Id;
        public TowDriverName GetDriverName() => _towDriverName;
        public TowDriverEmail GetDriverEmail() => _towDriverEmail;
        public TowDriverDrivingLicense GetTowDriverDrivingLicense() => _towDriverDrivingLicense;
        public TowDriverMedicalCertificate GetTowDriverMedicalCertificate() => _towDriverMedicalCertificate;
        public TowDriverIdentificationNumber GetDriverIdentificationNumber() => _towDriverIdentificationNumber;
        public TowDriverLocation? GetDriverLocation() => _towDriverLocation;
        public TowDriverStatus GetTowDriverStatus() => _towDriverStatus;

        public static TowDriver Create(
            TowDriverId towDriverId,
            TowDriverName towDriverName,
            TowDriverEmail towDriverEmail,
            TowDriverDrivingLicense towDriverDrivingLicense,
            TowDriverMedicalCertificate towDriverMedicalCertificate,
            TowDriverIdentificationNumber towDriverIdentificationNumber,
            TowDriverLocation towDriverLocation,
            TowDriverStatus towDriverStatus,
            bool fromPersistence = false)
        {
            if (fromPersistence)
            {
                return new TowDriver(towDriverId)
                {
                    _towDriverId = towDriverId,
                    _towDriverName = towDriverName,
                    _towDriverEmail = towDriverEmail,
                    _towDriverDrivingLicense = towDriverDrivingLicense,
                    _towDriverMedicalCertificate = towDriverMedicalCertificate,
                    _towDriverIdentificationNumber = towDriverIdentificationNumber,
                    _towDriverLocation = towDriverLocation,
                    _towDriverStatus = towDriverStatus,
                };
            }

            var towDriver = new TowDriver(towDriverId);
            towDriver.Apply(
                TowDriverCreated.CreateEvent(
                    towDriverId,
                    towDriverName,
                    towDriverEmail,
                    towDriverDrivingLicense,
                    towDriverMedicalCertificate,
                    towDriverIdentificationNumber,
                    towDriverLocation,
                    towDriverStatus
                )
            );
            return towDriver;
        }

        private void OnTowDriverCreatedEvent(TowDriverCreated @event)
        {
            _towDriverId = new TowDriverId(@event.TowDriverId);
            _towDriverName = new TowDriverName(@event.TowDriverName);
            _towDriverEmail = new TowDriverEmail(@event.TowDriverEmail);
            _towDriverDrivingLicense = new TowDriverDrivingLicense(
                @event.LicenseOwnerName,
                @event.LicenseIssueDate,
                @event.LicenseExpirationDate
            );
            _towDriverMedicalCertificate = new TowDriverMedicalCertificate(
                @event.MedicalCertificateOwnerName,
                @event.MedicaCertificateAge,
                @event.MedicalCertificateIssueDate,
                @event.MedicalCertificateExpirationDate
            );
            _towDriverIdentificationNumber = new TowDriverIdentificationNumber(@event.TowDriverIdentificationNumber);
            _towDriverLocation = new TowDriverLocation(@event.TowDriverLocation!);
            _towDriverStatus = new TowDriverStatus(@event.TowDriverStatus);
        }

    }
}
