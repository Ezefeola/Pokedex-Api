using Domain.Users;
using Domain.Users.ValueObjects;

namespace Application.Contracts.Repositories;
public interface IUserRepository : IGenericRepository<User, UserId>
{
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}
