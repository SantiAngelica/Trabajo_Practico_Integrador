using Infrastructure;

namespace Domain.Interfaces;


public interface IUserRepository
{
    Task<IReadOnlyList<User>> Get();
    Task<User> GetById(int Id);
    Task<bool> Delete(int Id);

    Task<bool> UpdateUserRol(int id, string newRol);
}