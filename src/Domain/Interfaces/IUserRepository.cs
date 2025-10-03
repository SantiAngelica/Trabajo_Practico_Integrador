using Domain.Entities;
using Domain.Enum;

namespace Domain.Interfaces;

public interface IUserRepository
{
    Task<User> Create(User user);
    Task<IReadOnlyList<User>> Get();
    Task<User?> GetById(string Id);
    Task<bool> Delete(string Id);

    Task<bool> UpdateUserRol(string id, RolesEnum newRol);

    Task<User?> UpdateUser(string id, User updateUser);
}
