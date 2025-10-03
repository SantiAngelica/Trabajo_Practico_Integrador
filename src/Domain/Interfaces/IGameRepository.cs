using Domain.Entities;

namespace Domain.Interfaces;

public interface IGameRepository
{
    Task<IReadOnlyList<Game>> Get();
    Task<Game?> GetById(string id);
    Task<Game?> Add(Game game, Reservation reservation);
    Task<bool> Delete(string id);
}
