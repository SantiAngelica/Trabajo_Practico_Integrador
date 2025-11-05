using Domain.Entities;
using Domain.Enum;

namespace Domain.Interfaces;

public interface IGameRepository : IRepositoryBase<Game>
{
    Task<IReadOnlyList<Game>> GetByPropertyId(int propertyId, States reservationState);
    Task<IReadOnlyList<Game>> GetByUserCreatorId(int userId);
}
