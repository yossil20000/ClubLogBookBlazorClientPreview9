using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using ClubLogBook.Core.Entities;
using ClubLogBook.Application.Interfaces;
using ClubLogBook.Infrastructure.Persistence;


namespace ClubLogBook.Infrastructure.Data
{
	public class LogbookRepository : EFRepository<LogBook>, ILogbookRepository
	{
		public LogbookRepository(ApplicationDbContext dbcontex) : base(dbcontex)
		{

		}
		public Task<LogBook> GetByIdWithItemsAsync(int id)
		{
			return _dbContex.LogBooks.Include(fr => fr.FlightRecords).FirstOrDefaultAsync(i => i.Id == id);
		}
		public Task<LogBook> GetFlightRecordsByPilotIdAsync(int pilotId)
		{
			return _dbContex.LogBooks.Include(fr => fr.FlightRecords).FirstOrDefaultAsync(i => i.PilotId == pilotId);
		}
		
	}
}
