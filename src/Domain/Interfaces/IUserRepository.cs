using Infrastructure;

namespace Domain.Interfaces;


public interface IUserRepository
{
    Task<IReadOnlyList<User>> Get();
}