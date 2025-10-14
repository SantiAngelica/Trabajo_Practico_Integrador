using Domain.Entities;

namespace Application.Interfaces;

public interface IAuthSecurity
{
    string GeneraToken(User user);
}
