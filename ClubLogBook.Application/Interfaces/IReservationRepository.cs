using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClubLogBook.Core.Entities;

namespace ClubLogBook.Application.Interfaces
{
	public interface IReservationRepository : IAsyncRepository<AircraftReservation>
	{
		Task UpdateReservation(AircraftReservation aircraftReservation);
	}
}
