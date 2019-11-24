using System.Threading;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace ClubLogBook.Application.Interfaces
{
	public interface IClubContext
	{
		DbSet<T> Set<T>() where T : class;
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}
