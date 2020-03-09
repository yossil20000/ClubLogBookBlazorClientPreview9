using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubLogBook.Application.Models;
namespace ClubLogBook.Application.Interfaces
{
	public interface IClubContactsViewModelService
	{
		Task<IEnumerable<ClubContactsModel>> GetOrCreateClubContact(string clubName, bool include = true);
		Task<bool> UpdateOrCreateClubContactMember(string clubName, ClubContactsModel clubContactsViewModel);
		Task<IEnumerable<ClubContactsModel>> GetPilotsNotInAnyClub();
		//Task<bool> UpdateOrCreateClubContactMember(string clubName, ClubContactsViewModel clubContactUpdateViewModel);
		Task<List<ClubContactsModel>> GetAllPilot();
	}
}
