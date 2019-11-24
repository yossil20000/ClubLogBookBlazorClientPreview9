using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubLogBook.Application.ViewModels;
namespace ClubLogBook.Application.Interfaces
{
	public interface IClubContactsViewModelService
	{
		Task<IEnumerable<ClubContactsViewModel>> GetOrCreateClubContact(string clubName, bool include = true);
		Task<bool> UpdateOrCreateClubContactMember(string clubName, ClubContactsViewModel clubContactsViewModel);
		Task<IEnumerable<ClubContactsViewModel>> GetPilotsNotInAnyClub();
		//Task<bool> UpdateOrCreateClubContactMember(string clubName, ClubContactsViewModel clubContactUpdateViewModel);
		Task<IEnumerable<ClubContactsViewModel>> GetAllPilot();
	}
}
