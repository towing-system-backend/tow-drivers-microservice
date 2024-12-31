namespace tow_drivers_microservice.Src.Application.Commands.UpdateTowDriverStatus.Types
{
    public record UpdateTowDriverStatusCommand(string towDriverId, string status);
}
