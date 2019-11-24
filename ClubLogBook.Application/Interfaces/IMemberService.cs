using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ClubLogBook.Core.Entities;


namespace ClubLogBook.Application.Interfaces
{
	public interface IMemberService
	{
		Task<bool> IsExist(Pilot pilot);
		Task<Pilot> AddPilot(Pilot pilot);
		Task UpdatePilot(Pilot pilot);
		Task<Pilot> GetPilotById(int id);
		Task DeletePilotById(int id);
		Task<IEnumerable<Pilot>> GetAllPilotNotInClub();
		Task<IEnumerable<Pilot>> GetAllPilot();
		Task UpdatePilotUserId(int id, Guid userId);
		Task RemoveUserId(Guid userId);


	}
}
