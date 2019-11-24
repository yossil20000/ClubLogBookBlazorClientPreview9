using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClubLogBook.Core.Entities;

namespace ClubLogBook.Application.Interfaces
{
	public interface IAircraftLogBookRepository : IAsyncRepository<AircraftLogBook>
	{
		AircraftLogBook GetByTailNumber(string tailNumber);
		Task<int> AddFlight(Flight flight);
		Task<int> UpdateFlight(Flight flight);
	}
}
