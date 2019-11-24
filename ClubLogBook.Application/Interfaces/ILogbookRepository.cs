using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClubLogBook.Core.Entities;

namespace ClubLogBook.Application.Interfaces
{
	public interface ILogbookRepository : IAsyncRepository<LogBook>
	{
		Task<LogBook> GetByIdWithItemsAsync(int id);
		Task<LogBook> GetFlightRecordsByPilotIdAsync(int pilotId);
		
	}
}
