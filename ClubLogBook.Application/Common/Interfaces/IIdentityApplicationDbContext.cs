using Microsoft.EntityFrameworkCore;

using System.Threading;
using System.Threading.Tasks;

namespace ClubLogBook.Application.Common.Interfaces
{
	public interface IIdentityApplicationDbContext
	{
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}
