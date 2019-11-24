using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClubLogBook.Core.Entities;

namespace ClubLogBook.Application.Interfaces
{
	public interface IAircraftManagerService
	{
		Task<Aircraft> AddAircraft(Aircraft aircraft);
		Task<bool> RemoveAircraft(int id);
		Task<bool> UpdateAircraft(Aircraft aircraft);
		Task<List<Aircraft>> GetAllAircraftsInClub(string clubName);
		Task<List<Aircraft>> GetAllAircraftsNotInClub();
		Task<bool> UpdateAircraftPrice(AircraftPrice aircraftPrice);
		Task<List<AircraftPrice>> GetAircraftPrices();


	}
}
