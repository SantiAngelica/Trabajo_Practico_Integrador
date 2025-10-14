using Domain.Entities;

namespace Domain.Interfaces;

public interface IGameRepository : IRepositoryBase<Game>
{
    Task<IReadOnlyList<Game>> GetByPropertyId(string propertyId);

    Task<Game?> GetByReservationId(string reservationId);
}
