using System.Collections.Generic;

namespace ClubLogBook.Application.ViewModels
{

	public class FlightRecordIndexViewModel
	{
		public IEnumerable<ClubFlightViewModel> FlightRecords { get; set; }
		public FilterViewModel FilterViewModel { get; set; }
		public PaginationInfoViewModel PaginationInfo { get; set; }
	}
	public class RecordsViewModel<T> where T : class
	{
		public IEnumerable<T> Records { get; set; }
		public FilterViewModel FilterViewModel { get; set; } = new FilterViewModel();
		public PaginationInfoViewModel PaginationInfo { get; set; } = new PaginationInfoViewModel();
	}
	public class FilterViewModel
	{

		public IEnumerable<AirplaneSelectViewModel> AirplaneSelects { get; set; } = new List<AirplaneSelectViewModel>();
		public IEnumerable<ClubSelectViewModel> ClubSelects { get; set; } = new List<ClubSelectViewModel>();
		public IEnumerable<PilotSelectViewModel> PilotSelects { get; set; } = new List<PilotSelectViewModel>();
		public int? AirplaneFilterApplied { get; set; }
		public int? ClubFilterApplied { get; set; }
		public int? PilotFilterApplied { get; set; }
		
	}
}
