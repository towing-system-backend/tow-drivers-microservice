using System.ComponentModel.DataAnnotations;

namespace tow_drivers_microservice.Src.Infrastructure.Controllers.Dtos
{
    public record UpdateTowDriverStatusDto
    {
        [Required]
        [StringLength(128)]
        public string towDriverId { get; init; }

        [Required]
        [StringLength(128)]
        public string towDriverStatus { get; init; }
    }
}
