using Domain.Entities;
using Domain.Enum;

namespace Domain.Interfaces;

public interface IReservationRepository : IRepositoryBase<Reservation>
{
    Task<Reservation?> GetExistingReservation(DateOnly date, int fieldId, int scheduleId);

    Task<IReadOnlyList<Reservation?>> GetReservationsByPropertyId(int propertyId, DateOnly date);
}
