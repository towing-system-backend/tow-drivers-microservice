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

        private void OnTowDriverCreatedEvent(TowDriverCreated context)
        {
            _towDriverId = new TowDriverId(context.TowDriverId);
            _towDriverName = new TowDriverName(context.TowDriverName);
            _towDriverEmail = new TowDriverEmail(context.TowDriverEmail);
            _towDriverDrivingLicense = new TowDriverDrivingLicense(
                context.LicenseOwnerName,
                context.LicenseIssueDate,
                context.LicenseExpirationDate
            );
            _towDriverMedicalCertificate = new TowDriverMedicalCertificate(
                context.MedicalCertificateOwnerName,
                context.MedicalCertificateAge,
                context.MedicalCertificateIssueDate,
                context.MedicalCertificateExpirationDate
            );
            _towDriverIdentificationNumber = new TowDriverIdentificationNumber(context.TowDriverIdentificationNumber);
            _towDriverLocation = new TowDriverLocation(context.TowDriverLocation!);
            _towDriverStatus = new TowDriverStatus(context.TowDriverStatus);
        }

        public void UpdateDriverName(TowDriverName towDriverName)
        {
           Apply(TowDriverNameUpdated.CreateEvent(_towDriverId, towDriverName));
        }

        private void OnTowDriverNameUpdatedEvent(TowDriverNameUpdated context)
        {
            _towDriverName = new TowDriverName(context.TowDriverName);
        }

        public void UpdateDriverEmail(TowDriverEmail towDriverEmail)
        {
            Apply(TowDriverEmailUpdated.CreateEvent(_towDriverId, towDriverEmail));
        }

        private void OnTowDriverEmailUpdatedEvent(TowDriverEmailUpdated context)
        {
            _towDriverEmail = new TowDriverEmail(context.TowDriverEmail);
        }

        public void UpdateDriverDrivingLicense(TowDriverDrivingLicense towDriverDrivingLicense)
        {
            Apply(TowDriverDrivingLicenseUpdated.CreateEvent(_towDriverId, towDriverDrivingLicense));
        }

        private void OnTowDriverDrivingLicenseUpdatedEvent(TowDriverDrivingLicenseUpdated context)
        {
            _towDriverDrivingLicense = new TowDriverDrivingLicense(
                context.LicenseOwnerName,
                context.LicenseIssueDate,
                context.LicenseExpirationDate
            );
        }

        public void UpdateDriverMedicalCertificate(TowDriverMedicalCertificate towDriverMedicalCertificate)
        {
            Apply(TowDriverMedicalCertificateUpdated.CreateEvent(_towDriverId, towDriverMedicalCertificate));
        }

        private void OnTowDriverMedicalCertificateUpdatedEvent(TowDriverMedicalCertificateUpdated context)
        {
            _towDriverMedicalCertificate = new TowDriverMedicalCertificate(
                context.MedicalCertificateOwnerName,
                context.MedicalCertificateOwnerAge,
                context.MedicalCertificateIssueDate,
                context.MedicalCertificateExpirationDate
            );
        }

        public void UpdateDriverIdentificationNumber(TowDriverIdentificationNumber towDriverIdentificationNumber)
        {
            Apply(TowDriverIdentificationNumberUpdated.CreateEvent(_towDriverId, towDriverIdentificationNumber));
        }

        private void OnTowDriverIdentificationNumberUpdatedEvent(TowDriverIdentificationNumberUpdated context)
        {
            _towDriverIdentificationNumber = new TowDriverIdentificationNumber(context.TowDriverIdentificationNumber);
        }

        public void UpdateDriverLocation(TowDriverLocation towDriverLocation)
        {
            Apply(TowDriverLocationUpdated.CreateEvent(_towDriverId, towDriverLocation));
        }

        private void OnTowDriverLocationUpdatedEvent(TowDriverLocationUpdated context)
        {
            _towDriverLocation = new TowDriverLocation(context.TowDriverLocation);
        }

        public void UpdateDriverStatus(TowDriverStatus towDriverStatus)
        {
            Apply(TowDriverStatusUpdated.CreateEvent(_towDriverId, towDriverStatus));
        }

        private void OnTowDriverStatusUpdatedEvent(TowDriverStatusUpdated context)
        {
            _towDriverStatus = new TowDriverStatus(context.TowDriverStatus);
        }

    }
}
