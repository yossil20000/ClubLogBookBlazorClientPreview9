using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ClubLogBook.Core.Entities;

namespace ClubLogBook.Application.Interfaces
{
	public interface ILogbookService
	{
		//Task<FlightRecord> GetRecordByIdAsync(int id);
		Task <IEnumerable<FlightRecord>> GetRecordByAircrafAsync(int pilotId, int aircraftId);
		Task<LogBook> GetLogbookByPilotAsync(int pilotId);

	}
}
