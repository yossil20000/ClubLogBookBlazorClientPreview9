using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubLogBook.Core.Entities;
using ClubLogBook.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClubLogBook.Infrastructure.Data
{
	public class ReservationRepository : EFRepository<AircraftReservation>, IReservationRepository
	{
		public ReservationRepository(ClubLogbookContext dbcontex) : base(dbcontex)
		{
			
		}
		public async Task UpdateReservation(AircraftReservation aircraftReservation)
		{
			_dbContex.AircraftReservations.Update(aircraftReservation);
			await _dbContex.SaveChangesAsync();
		}
	}
}
