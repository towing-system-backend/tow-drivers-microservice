using System.ComponentModel.DataAnnotations;

namespace tow_drivers_microservice.Src.Infrastructure.Controllers.Dtos
{
    public record UpdateTowDriverLocationDto
    {
        [Required]
        [StringLength(128)]
        public string towDriverId { get; init; }

        [Required]
        [StringLength(256)]
        public string towDriverLocation { get; init; }
    }
}
