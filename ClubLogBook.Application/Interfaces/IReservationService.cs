using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ClubLogBook.Core.Entities;

namespace ClubLogBook.Application.Interfaces
{
	public interface IReservationService
	{
		Task<IEnumerable<AircraftReservation>> GetAircraftReservation(string tailNumber);
		Task<IEnumerable<AircraftReservation>> GetPilotReservation(string idNumber);
		Task<IEnumerable<AircraftReservation>> GetReservation();
		Task<AircraftReservation> AddReservation(AircraftReservation aircraftReservation);
		Task EditReservation(AircraftReservation aircraftReservation);
		Task DeleteReservation(int  id);
		Task<AircraftReservation> GetReservation(int id);
		Task<bool> IsValid(AircraftReservation aircraftReservation);

	}
}
