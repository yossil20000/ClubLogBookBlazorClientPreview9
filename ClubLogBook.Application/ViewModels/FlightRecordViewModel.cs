﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ClubLogBook.Application.ViewModels
{

	public class FlightRecordIndexViewModel
	{
		public List<ClubFlightViewModel> FlightRecords { get; set; }
		public FilterViewModel FilterViewModel { get; set; } = new FilterViewModel();
		public PaginationInfoViewModel PaginationInfo { get; set; } = new PaginationInfoViewModel();
		public void MarkNonValidFlight()
		{
			FlightRecords.OrderBy(x => x.EngineStart).ThenBy(x => x.EngineEnd).Select(x => x.EngineEnd - x )
		}
	}
	public class RecordsViewModel<T> where T : class
	{
		public IEnumerable<T> Records { get; set; }
		public FilterViewModel FilterViewModel { get; set; } = new FilterViewModel();
		public PaginationInfoViewModel PaginationInfo { get; set; } = new PaginationInfoViewModel();
	}
	public class FilterViewModel
	{

		public List<AirplaneSelectViewModel> AirplaneSelects { get; set; } = new List<AirplaneSelectViewModel>();
		public IEnumerable<ClubSelectViewModel> ClubSelects { get; set; } = new List<ClubSelectViewModel>();
		public List<PilotSelectViewModel> PilotSelects { get; set; } = new List<PilotSelectViewModel>();
		public int? AirplaneFilterApplied { get; set; } = 0;
		public int? ClubFilterApplied { get; set; } = 0;
		public int? PilotFilterApplied { get; set; } = 0;
		public FilterDateViewModel FilterDateViewModel { get; set; } = new FilterDateViewModel();

	}
	public class FilterDateViewModel
	{
		public DateTime FilterDateFrom { get; set; } = DateTime.Now;
		public DateTime FilterDateTo { get; set; } =  DateTime.Now.AddDays(1);
	}
}
