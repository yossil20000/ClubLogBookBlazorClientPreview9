using System;
using System.Collections.Generic;
using System.Text;
using ClubLogBook.Application.ViewModels;
namespace ClubLogBook.Application.AircraftManager.Queries
{
	public class AircraftListViewModel
	{
		public IList<AircraftViewModel> AircraftList { get; set; }
	}
}
