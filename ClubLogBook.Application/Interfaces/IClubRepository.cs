using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClubLogBook.Core.Entities;
namespace ClubLogBook.Application.Interfaces
{
	public interface IClubRepository : IAsyncRepository<Club>
	{
		Club GetAllByIdAsync(int id);
		Task<Club> GetAllByIdAsync(ISpecification<Club> spec);
		
	}
}

