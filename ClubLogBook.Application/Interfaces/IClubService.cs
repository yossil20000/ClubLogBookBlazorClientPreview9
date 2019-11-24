using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ClubLogBook.Core.Entities;
namespace ClubLogBook.Application.Interfaces
{
	public interface IClubService
	{
		Task<IReadOnlyCollection<Club>> GetClubs();
		Task<IReadOnlyCollection<Club>> GetClubById(int id);
		Task<ICollection<Pilot>> GetClubMembers(string clubName);
		Task<ICollection<Aircraft>> GetClubAircrafts(string clubName);
		Task<Pilot> GetPilotById(int id);
		Task<ICollection<Flight>> GetClubAircraftFlight(string clubName,int aircraftId);
		Task AddContactToClub(string clubName, Pilot pilot);
		Task AddPilotToClub(string clubName, int pilotId);
		Task RemoveContactFromClub(string clubName, int pilotId);
		Task DeleteFlight(int id);
		Task AddFlight(string clubName, Flight flight, int aircraftId, int pilotId);
		Task UpdateFlight(Flight flight);
		
		
	}
}
