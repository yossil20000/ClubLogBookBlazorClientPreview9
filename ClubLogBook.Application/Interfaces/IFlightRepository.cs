using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClubLogBook.Core.Entities;

namespace ClubLogBook.Application.Interfaces
{
	public interface IFlightRepository : IAsyncRepository<Flight>
	{
		Task<bool> DeleteFlight(int flightId);
	}
}
