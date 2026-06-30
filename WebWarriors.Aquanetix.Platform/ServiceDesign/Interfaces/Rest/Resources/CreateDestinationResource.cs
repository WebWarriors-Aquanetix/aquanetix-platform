namespace WebWarriors.Aquanetix.Platform.ServiceDesign.Interfaces.Rest.Resources;

public record CreateDestinationResource(
    string Name,
    string Address,
    string Description);
