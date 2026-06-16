using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.Shared.Domain.Repositories;

namespace WebWarriors.Aquanetix.Platform.Devices.Domain.Repositories;

public interface IDeviceRepository : IBaseRepository<Device>
{
    // FindByIdAsync y ListAsync ya se heredan de IBaseRepository<Device>.
    // Solo agregarías métodos específicos si el agregado Device los requiriera.
}