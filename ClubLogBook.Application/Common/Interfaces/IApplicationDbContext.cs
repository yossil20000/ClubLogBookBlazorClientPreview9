using ClubLogBook.Core.Entities;
using Microsoft.EntityFrameworkCore;

using System.Threading;
using System.Threading.Tasks;

namespace ClubLogBook.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<T> Set<T>() where T : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
