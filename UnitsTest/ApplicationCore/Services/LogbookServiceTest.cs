using System;
using System.Collections.Generic;
using NUnit.Framework;
using ClubLogBook.Core.Entities;
using ClubLogBook.Infrastructure.Data;
using ClubLogBook.Infrastructure.Persistence;
using UnitsTest.ApplicationInfrastructure;
using System.Threading.Tasks;
using ClubLogBook.Application.Services;

namespace UnitsTest.ApplicationCore.Services
{

	public class LogbookServiceTest
	{
		public LogbookRepository logbookRepository;
		
		public LogbookService logbookService;
		public ApplicationDbContext _context;
		[Test]
		public async Task LBRepositoryTest()
		{
			ImportDataTest import = new ImportDataTest();
			import.InitContext();
			
			try
			{
				_context = import._context;
				logbookRepository = new LogbookRepository(_context);

				logbookService = new LogbookService(logbookRepository, null,null);
				IEnumerable<FlightRecord> fr = await logbookService.GetRecordByAircrafAsync(12, 8);
				foreach (var f in fr)
				{
					System.Diagnostics.Debug.WriteLine(f.ToString());
				}

				LogBook logBook = await logbookRepository.GetByIdWithItemsAsync(1);
				LogBook logBook1 = await logbookRepository.GetFlightRecordsByPilotIdAsync(12);
				LogBook a = await logbookService.GetLogbookByPilotAsync(12);
				
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
		}
	}
}
