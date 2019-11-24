﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubLogBook.Application.ViewModels;
namespace ClubLogBook.Application.Interfaces
{
	public interface IRecordViewModelService<T> where T : class
	{
		Task<T> GetRecord(int pageIndex, int itemsPage, int? airplaneId, int? pilotId);
		Task<IEnumerable< AirplaneSelectViewModel>> GetAirplans(int? clubId);
		Task<IEnumerable< ClubSelectViewModel> > GetClubs(int? clubId);
		Task<IEnumerable<PilotSelectViewModel>> GetPilots(int? clubId);
	}
}
