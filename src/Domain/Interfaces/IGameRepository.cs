using Domain.Entities;

namespace Domain.Interfaces;

public interface IGameRepository : IRepositoryBase<Game>
{
    Task<IReadOnlyList<Game>> GetByPropertyId(string propertyId);
    Task<IReadOnlyList<Game>> GetByUserCreatorId(string userId);
}
