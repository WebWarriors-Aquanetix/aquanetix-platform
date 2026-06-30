namespace WebWarriors.Aquanetix.Platform.ServiceDesign.Domain.Model;

public enum ServiceDesignError
{
    None,
    WaterBatchNotFound,
    InvalidVolume,
    InvalidCertificationCode,
    DestinationSectorNotFound,
    OperationCancelled,
    DatabaseError,
    InternalServerError,
    DestinationNotFound,
    DestinationNameAlreadyExists,
    DestinationInUse
}