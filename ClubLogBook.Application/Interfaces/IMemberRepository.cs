using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClubLogBook.Core.Entities;

namespace ClubLogBook.Application.Interfaces
{
	public interface IMemberRepository : IAsyncRepository<Pilot>
	{
		Task<bool> IsExist(Pilot pilot);
		Task<int> AddPilot(Pilot pilot);
		Task<Pilot> GetPilot(int id);
		Task DeletePilot(int id);
		Task<IEnumerable<Pilot>> GetAllPilotNotInClub();
		Task<IEnumerable<Pilot>> GetAllPilots();
	}
}
