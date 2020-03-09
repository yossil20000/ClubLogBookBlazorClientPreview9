using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ClubLogBook.Application.Models
{
	
	public class FlightRecordIndexModel
	{
		public List<ClubFlightModel> FlightRecords { get; set; }
		public FilterModel FilterModel { get; set; } = new FilterModel();
		public PaginationInfoModel PaginationInfo { get; set; } = new PaginationInfoModel();
		
	}
	public class RecordsViewModel<T> where T : class
	{
		public IEnumerable<T> Records { get; set; }
		public FilterModel FilterViewModel { get; set; } = new FilterModel();
		public PaginationInfoModel PaginationInfo { get; set; } = new PaginationInfoModel();
	}
	public class FilterModel
	{

		public List<AirplaneSelectModel> AirplaneSelects { get; set; } = new List<AirplaneSelectModel>();
		public IEnumerable<ClubSelectModel> ClubSelects { get; set; } = new List<ClubSelectModel>();
		public List<PilotSelectModel> PilotSelects { get; set; } = new List<PilotSelectModel>();
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
