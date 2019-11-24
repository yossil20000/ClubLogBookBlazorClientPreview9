using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClubLogBook.Core.Entities;

namespace ClubLogBook.Application.Interfaces
{
	public interface IAircraftRepository : IAsyncRepository<Aircraft>
	{
		Task<List<Aircraft>> GetAllInClub(string clubName);
		Task<List<Aircraft>> GetAllNotInClub(string clubName);
		Task<List<AircraftPrice>> GetAircraftPrices();
	}
}
