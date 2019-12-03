using System.Threading.Tasks;
using ClubLogBook.Core.Entities;
using ClubLogBook.Application.Interfaces;
using ClubLogBook.Infrastructure.Persistence;

namespace ClubLogBook.Infrastructure.Data
{
	public class ReservationRepository : EFRepository<AircraftReservation>, IReservationRepository
	{
		public ReservationRepository(ApplicationDbContext dbcontex) : base(dbcontex)
		{
			
		}
		public async Task UpdateReservation(AircraftReservation aircraftReservation)
		{
			_dbContex.AircraftReservations.Update(aircraftReservation);
			await _dbContex.SaveChangesAsync();
		}
	}
}
