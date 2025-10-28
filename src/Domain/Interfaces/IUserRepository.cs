using Domain.Entities;
using Domain.Enum;

namespace Domain.Interfaces;

public interface IUserRepository : IRepositoryBase<User>
{
    Task<User?> GetByEmail(string email);

    Task<User?> GetWithParticipations(string id);
}
