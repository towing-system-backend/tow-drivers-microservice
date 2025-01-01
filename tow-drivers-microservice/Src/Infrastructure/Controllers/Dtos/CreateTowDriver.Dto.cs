using System.ComponentModel.DataAnnotations;

namespace TowDrivers.Infrastructure
{
    public record CreateTowDriverDto
    {
        [Required][StringLength(50)]
        public string towDriverName { get; init; }

        [Required][EmailAddress]
        public string towDriverEmail { get; init; }

        [Required][StringLength(50)]
        public string licenseOwnerName { get; init; }

        [Required][DataType(DataType.Date)]
        public DateOnly licenseIssueDate { get; init; }
        
        [Required][DataType(DataType.Date)]
        public DateOnly licenseExpirationDate { get; init; }


        [Required][StringLength(50)]
        public string medicalCertificateOwnerName { get; init; }

        [Required][Range(18, 100)]
        public int medicalCertificateAge {  get; init; }

        [Required][DataType(DataType.Date)]
        public DateOnly medicalCertificateIssueDate { get; init; }

        [Required][DataType(DataType.Date)]
        public DateOnly medicalCertificateExpirationDate { get; init; }

        [Required]
        [Range(1000000, 99999999)]
        public int towDriverIdentificationNumber { get; init; }
    }
}
