using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubLogBook.Core.Entities;
using ClubLogBook.Core.Interfaces;
using ClubLogBook.Core.Specifications;

namespace ClubLogBook.Core.Services
{
	public class LogbookService : ILogbookService
	{
		private readonly IAsyncRepository<LogBook> _logbookRepository;
		private readonly IAsyncRepository<FlightRecord> _flightRecordRepository;
		private readonly IAppLogger<LogbookService> _logger;
		public LogbookService(IAsyncRepository<LogBook> logbookRepository, IAsyncRepository<FlightRecord> flightRecordRepository, IAppLogger<LogbookService> logger)
		{
			_logbookRepository = logbookRepository;
			_flightRecordRepository = flightRecordRepository;
			_logger = logger;
		}
		
		

		public async Task<LogBook> GetLogbookByPilotAsync(int pilotId)
		{
			var logbookSpec = new LogbookWithItemsSpecification(pilotId);
			return (await _logbookRepository.ListAsync(logbookSpec)).FirstOrDefault();
		}
		
		public async Task<IEnumerable<FlightRecord>> GetRecordByAircrafAsync(int pilotId, int aircraftId)
		{
			//return await _flightRecordRepository?.GetByIdAsync(id);
			var logbookSpec = new LogbookWithItemsSpecification(pilotId, aircraftId);
			LogBook log = (_logbookRepository.ListAsync(logbookSpec)).Result.FirstOrDefault();
			IEnumerable<FlightRecord> f =  log.FlightRecords.Where(fr => fr.Aircraft.Id == aircraftId);
			return f.AsEnumerable<FlightRecord>();
		}
	}
}
