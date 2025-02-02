﻿using Application.Core;

namespace TowDriver.Domain
{
    public class TowDriver : AggregateRoot<TowDriverId>
    {
        private new TowDriverId Id;
        private new SupplierCompanyId _supplierCompanyId;
        private TowDriverName _towDriverName;
        private TowDriverEmail _towDriverEmail;
        private TowDriverDrivingLicense _towDriverDrivingLicense;
        private TowDriverMedicalCertificate _towDriverMedicalCertificate;
        private TowDriverIdentificationNumber _towDriverIdentificationNumber;
        private TowDriverLocation? _towDriverLocation;
        private TowDriverStatus _towDriverStatus;
        private TowDriverTowAssigned _towAssigned;

        public TowDriver(TowDriverId towDriverId) : base(towDriverId)
        {
            Id = towDriverId;
        }
        public override void ValidateState()
        {
            if (
                Id == null ||
                _supplierCompanyId == null ||
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
        public SupplierCompanyId GetSupplierCompanyId() => _supplierCompanyId;
        public TowDriverName GetTowDriverName() => _towDriverName;
        public TowDriverEmail GetTowDriverEmail() => _towDriverEmail;
        public TowDriverDrivingLicense GetTowDriverDrivingLicense() => _towDriverDrivingLicense;
        public TowDriverMedicalCertificate GetTowDriverMedicalCertificate() => _towDriverMedicalCertificate;
        public TowDriverIdentificationNumber GetTowDriverIdentificationNumber() => _towDriverIdentificationNumber;
        public TowDriverLocation? GetTowDriverLocation() => _towDriverLocation;
        public TowDriverStatus GetTowDriverStatus() => _towDriverStatus;

        public TowDriverTowAssigned GetTowDriverTowAssigned() => _towAssigned;

        public static TowDriver Create(
            TowDriverId towDriverId,
            SupplierCompanyId supplierCompanyId,
            TowDriverName towDriverName,
            TowDriverEmail towDriverEmail,
            TowDriverDrivingLicense towDriverDrivingLicense,
            TowDriverMedicalCertificate towDriverMedicalCertificate,
            TowDriverIdentificationNumber towDriverIdentificationNumber,
            TowDriverLocation towDriverLocation,
            TowDriverStatus towDriverStatus,
            TowDriverTowAssigned towAssigned,
            bool fromPersistence = false)
        {
            if (fromPersistence)
            {
                return new TowDriver(towDriverId)
                {
                    Id = towDriverId,
                    _supplierCompanyId = supplierCompanyId,
                    _towDriverName = towDriverName,
                    _towDriverEmail = towDriverEmail,
                    _towDriverDrivingLicense = towDriverDrivingLicense,
                    _towDriverMedicalCertificate = towDriverMedicalCertificate,
                    _towDriverIdentificationNumber = towDriverIdentificationNumber,
                    _towDriverLocation = towDriverLocation,
                    _towDriverStatus = towDriverStatus,
                    _towAssigned = towAssigned,
                };
            }

            var towDriver = new TowDriver(towDriverId);
            towDriver.Apply(
                TowDriverCreated.CreateEvent(
                    towDriverId,
                    supplierCompanyId,
                    towDriverName,
                    towDriverEmail,
                    towDriverDrivingLicense,
                    towDriverMedicalCertificate,
                    towDriverIdentificationNumber,
                    towDriverLocation,
                    towDriverStatus,
                    towAssigned
                )
            );
            return towDriver;
        }

        public void UpdateSupplierCompanyId(SupplierCompanyId id)
        {
            Apply(SupplierCompanyIdUpdated.CreateEvent(Id, id));
        }

        public void UpdateDriverName(TowDriverName towDriverName)
        {
           Apply(TowDriverNameUpdated.CreateEvent(Id, towDriverName));
        }
        
        public void UpdateDriverEmail(TowDriverEmail towDriverEmail)
        {
            Apply(TowDriverEmailUpdated.CreateEvent(Id, towDriverEmail));
        }
        
        public void UpdateDriverDrivingLicense(TowDriverDrivingLicense towDriverDrivingLicense)
        {
            Apply(TowDriverDrivingLicenseUpdated.CreateEvent(Id, towDriverDrivingLicense));
        }
        
        public void UpdateDriverMedicalCertificate(TowDriverMedicalCertificate towDriverMedicalCertificate)
        {
            Apply(TowDriverMedicalCertificateUpdated.CreateEvent(Id, towDriverMedicalCertificate));
        }
        
        public void UpdateDriverIdentificationNumber(TowDriverIdentificationNumber towDriverIdentificationNumber)
        {
            Apply(TowDriverIdentificationNumberUpdated.CreateEvent(Id, towDriverIdentificationNumber));
        }
        
        public void UpdateDriverLocation(TowDriverLocation towDriverLocation)
        {
            Apply(TowDriverLocationUpdated.CreateEvent(Id, towDriverLocation));
        }
        
        public void UpdateDriverStatus(TowDriverStatus towDriverStatus)
        {
            Apply(TowDriverStatusUpdated.CreateEvent(Id, towDriverStatus));
        }

        public void UpdateDriverTowAssigned(TowDriverTowAssigned towDriverTowAssigned) 
        {
            Apply(TowDriverTowAssignedUpdated.CreateEvent(Id, towDriverTowAssigned));
        }

        private void OnTowDriverCreatedEvent(TowDriverCreated context)
        {
            Id = new TowDriverId(context.TowDriverId);
            _supplierCompanyId = new SupplierCompanyId(context.SupplierCompanyId);
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
            _towAssigned = new TowDriverTowAssigned(context.TowDriverTowAssigned);
        }

        private void OnTowDriverNameUpdatedEvent(TowDriverNameUpdated context)
        {
            _towDriverName = new TowDriverName(context.TowDriverName);
        }

        private void OnTowDriverEmailUpdatedEvent(TowDriverEmailUpdated context)
        {
            _towDriverEmail = new TowDriverEmail(context.TowDriverEmail);
        }

        private void OnTowDriverDrivingLicenseUpdatedEvent(TowDriverDrivingLicenseUpdated context)
        {
            _towDriverDrivingLicense = new TowDriverDrivingLicense(
                context.LicenseOwnerName,
                context.LicenseIssueDate,
                context.LicenseExpirationDate
            );
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

        private void OnTowDriverIdentificationNumberUpdatedEvent(TowDriverIdentificationNumberUpdated context)
        {
            _towDriverIdentificationNumber = new TowDriverIdentificationNumber(context.TowDriverIdentificationNumber);
        }

        private void OnTowDriverLocationUpdatedEvent(TowDriverLocationUpdated context)
        {
            _towDriverLocation = new TowDriverLocation(context.TowDriverLocation);
        }

        private void OnTowDriverStatusUpdatedEvent(TowDriverStatusUpdated context)
        {
            _towDriverStatus = new TowDriverStatus(context.TowDriverStatus);
        }

        private void OnTowDriverTowAssignedUpdatedEvent(TowDriverTowAssignedUpdated context) 
        {
            _towAssigned = new TowDriverTowAssigned(context.TowDriverTowAssigned);
        }

        private void OnSupplierCompanyIdUpdatedEvent(SupplierCompanyIdUpdated context) 
        {
            _supplierCompanyId = new SupplierCompanyId(context.SupplierCompanyId);
        }
    }
}
