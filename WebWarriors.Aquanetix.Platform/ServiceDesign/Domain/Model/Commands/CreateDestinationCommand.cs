namespace WebWarriors.Aquanetix.Platform.ServiceDesign.Domain.Model.Commands;

public record CreateDestinationCommand(
    string Name,
    string Address,
    string Description);
