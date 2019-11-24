using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubLogBook.Core.Entities;
using ClubLogBook.Application.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace ClubLogBook.Infrastructure.Data
{
	public class FlightRepository : EFRepository<Flight>, IFlightRepository
	{
		public FlightRepository(ClubLogbookContext dbcontex) : base(dbcontex)
		{
		}

		public async Task<bool>  DeleteFlight(int flightId)
		{
			Flight record;
			//await DeleteAsync(record);
			//var record = _dbContex.Flights.Find(flightId);
			record = _dbContex.Flights.Where(i => i.Id == flightId).SingleOrDefault();
			if(record != null)
			{
				
				_dbContex.Flights.Remove(record);
				await _dbContex.SaveChangesAsync();
				return true;
			}
			return false;
		}
	}
}
