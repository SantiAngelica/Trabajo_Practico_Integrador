using Domain.Entities;
using Domain.Enum;

namespace Domain.Interfaces;

public interface IReservationRepository : IRepositoryBase<Reservation>
{
    Task<Reservation?> GetExistingReservation(DateOnly date, string fieldId, string scheduleId);

    Task<IReadOnlyList<Reservation?>> GetReservationsByPropertyId(string propertyId, DateOnly date);

}
