using Domain.Entities;

namespace Domain.Interfaces;

public interface IParticipationRepository : IRepositoryBase<Participation>
{
    Task<List<Participation>> GetByUserId(string userId);

    Task<IReadOnlyList<Participation>> GetAceptedByUserId(string userId);
}
