using System;
using System.ComponentModel.DataAnnotations;
namespace ClubLogBook.Application.ViewModels
{
	public class FlightReservationViewModel
	{
		public int Id { get; set; }
		public FlightReservationViewModel() { DateTo = DateTime.Now.AddHours(1); DateFrom = DateTime.Now; ExtructTime(); }


		[Required(ErrorMessage = "Required")]
		[DataType(DataType.DateTime)]
		private DateTime dateFrom;
		public DateTime DateFrom { get { return dateFrom; } set { dateFrom = value; } }
		private DateTime dateTo;
		[Required(ErrorMessage = "Required")]
		[DataType(DataType.DateTime)]
		//[DateGreaterThan(otherPropertyName = "DateFrom", ErrorMessage = "")]
		public DateTime DateTo { get { return dateTo; } set { dateTo = value; } }
		[Required]
		[StringLength(5, MinimumLength = 1, ErrorMessage = "Required")]
		public string IdNumber { get; set; }

		public string UserInfo { get; set; } = "";
		[Required]
		[StringLength(6, MinimumLength = 1, ErrorMessage = "Required")]
		public string TailNumber { get; set; } = "";
		private DateTime timeFrom;
		public DateTime TimeFrom { get { return timeFrom; } set { timeFrom = value; } }
		private DateTime timeTo;
		public DateTime TimeTo { get { return timeTo; } set { timeTo = value; } }
		public Guid UserId { get; set; }
		public int PilotId { get; set; }
		public int AircraftId {get;set;}
		public int ClubId { get; set; }
		public void CombineTime()
		{
			
			dateFrom = DateFrom.Date.Add(timeFrom.TimeOfDay);
			dateTo = DateTo.Date.Add(timeTo.TimeOfDay);
		}
		public void ExtructTime()
		{

			timeFrom = TimeFrom.Date.Add(dateFrom.TimeOfDay);
			timeTo = TimeTo.Date.Add(dateTo.TimeOfDay);
		}
	}
}
