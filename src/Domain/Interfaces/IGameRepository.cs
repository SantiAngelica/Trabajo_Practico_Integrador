using Domain.Entities;

namespace Domain.Interfaces;

public interface IGameRepository : IRepositoryBase<Game>
{
    Task<IReadOnlyList<Game>> GetByPropertyId(int propertyId);
    Task<IReadOnlyList<Game>> GetByUserCreatorId(int userId);
}
