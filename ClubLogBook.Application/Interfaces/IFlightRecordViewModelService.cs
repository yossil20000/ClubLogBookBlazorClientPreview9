using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubLogBook.Application.Models;
namespace ClubLogBook.Application.Interfaces
{
	public interface IRecordViewModelService<T> where T : class
	{
		Task<T> GetRecord(int pageIndex, int itemsPage, int? airplaneId, int? pilotId);
		Task<List< AirplaneSelectModel>> GetAirplans(int? clubId);
		Task<IEnumerable< ClubSelectModel> > GetClubs(int? clubId);
		Task<List<PilotSelectModel>> GetPilots(int? clubId);
	}
}
