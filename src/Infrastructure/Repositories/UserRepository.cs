using Application.Contracts.Repositories;
using Domain.Users;
using Domain.Users.ValueObjects;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class UserRepository : GenericRepository<User, UserId>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await Query()
                     .FirstOrDefaultAsync(x => x.EmailAddress.Value == email, cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await Query()
                     .AsNoTracking()
                     .AnyAsync(x => x.EmailAddress.Value == email, cancellationToken);
    }
}