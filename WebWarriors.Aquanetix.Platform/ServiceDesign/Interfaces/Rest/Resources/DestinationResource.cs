namespace WebWarriors.Aquanetix.Platform.ServiceDesign.Interfaces.Rest.Resources;

public record DestinationResource(
    int    Id,
    string Name,
    string Address,
    string Description);
