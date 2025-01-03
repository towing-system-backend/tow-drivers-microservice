using System.ComponentModel.DataAnnotations;

namespace tow_drivers_microservice.Src.Infrastructure.Controllers.Dtos
{
    public record FindTowDriverByEmailDto
    (
         string Email
    );
}
