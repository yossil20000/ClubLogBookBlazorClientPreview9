using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubLogBook.Core.Entities;
using ClubLogBook.Core.Common;
using ClubLogBook.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClubLogBook.Infrastructure.Data
{
	public class AircraftLogBookRepository : EFRepository<AircraftLogBook>, IAircraftLogBookRepository
	{
		public AircraftLogBookRepository(ClubLogbookContext dbcontex) : base(dbcontex)
		{
		}

		

		public AircraftLogBook GetByTailNumber(string tailNumber)
		{
			return _dbContex.AircraftLogBooks.Where(o => o.TaiNumber == tailNumber).SingleOrDefault();
		}
		public async Task<int> AddFlight(Flight flight)
		{
			var lb = _dbContex.AircraftLogBooks.Where(i => i.TaiNumber == flight.Aircraft.TailNumber);
			var lbone = lb.SingleOrDefault();
			lbone.Add(flight);
			_dbContex.AircraftLogBooks.Update(lbone);
			return await _dbContex.SaveChangesAsync();

		}
		public async Task<int> UpdateFlight(Flight flight)
		{
			//var lb = _dbContex.AircraftLogBooks.Where(i => i.TaiNumber == flight.Aircraft.TailNumber);
			//var lbone = lb.SingleOrDefault();
			
			_dbContex.Flights.Update(flight);
			return await _dbContex.SaveChangesAsync();

		}
	}
}
