using Domain.Entities;

namespace Domain.Interfaces;

public interface IParticipationRepository : IRepositoryBase<Participation>
{
    Task<List<Participation>> GetByUserId(int userId);

    Task<IReadOnlyList<Participation>> GetAceptedByUserId(int userId);

    Task<Participation?> GetByUserIdAndGameId(int userId, int gameId);
}
