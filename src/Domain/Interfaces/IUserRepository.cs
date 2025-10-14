using Domain.Entities;
using Domain.Enum;

namespace Domain.Interfaces;

public interface IUserRepository : IRepositoryBase<User>
{
    Task<User?> GetByEmail(string email);

    Task<bool> UpdateUserRol(string id, RolesEnum newRol);
}
