using AutoMapper;
using ClubLogBook.Application.Interfaces.Mapping;
using ClubLogBook.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;
namespace ClubLogBook.Application.Models
{
	public class FlightReservationCreateModel 
	{
		public FlightReservationCreateModel()
		{
			ExtructTime();
		}
		[Required]
		
		public int PilotId { get; private set; } = 0;
		
		public  int AircraftId { get; private set; } = 0;
		
		[Required(ErrorMessage = "Required")]
		[DataType(DataType.DateTime)]
		private DateTime dateFrom = DateTime.Now; 
		public DateTime DateFrom { get { return dateFrom; } set { dateFrom = value; } }
		private DateTime dateTo = DateTime.Now.AddHours(1);
		
		[Required(ErrorMessage = "Required")]
		[DataType(DataType.DateTime)]
		public DateTime DateTo { get { return dateTo; } set { dateTo = value; } }

		private DateTime timeFrom = DateTime.Now;
		public DateTime TimeFrom { get { return timeFrom; } set { timeFrom = value; } }
		private DateTime timeTo = DateTime.Now.AddHours(1);
		public DateTime TimeTo { get { return timeTo; } set { timeTo = value; } }
		private string _pilotSelect = "";
		[Required]
		[MinLength(1)]
		public string PilotSelect { get { return _pilotSelect; } set { _pilotSelect = value; PilotId = Int32.Parse(_pilotSelect); } }
		private string _aircraftSelect = "";
		[Required]
		[MinLength(1)]
		public string AircraftSelect { get { return _aircraftSelect; } set { _aircraftSelect = value; AircraftId = Int32.Parse(_aircraftSelect); } }
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
	public class FlightReservationModel : IHaveCustomMapping
	{
		public int Id { get; set; }
		public FlightReservationModel() { DateTo = DateTime.Now.AddHours(1); DateFrom = DateTime.Now; ExtructTime(); }


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
		[StringLength(10, MinimumLength = 1, ErrorMessage = "Required")]
		public string IdNumber { get; set; }
		public string FirstName { get; set; } = "";
		public string LastName { get; set; } = "";
		public string UserInfo { get; set; } = "";
		[Required]
		[StringLength(10, MinimumLength = 1, ErrorMessage = "Required")]
		public string TailNumber { get; set; } = "";
		private DateTime timeFrom;
		public DateTime TimeFrom { get { return timeFrom; } set { timeFrom = value; } }
		private DateTime timeTo;
		public DateTime TimeTo { get { return timeTo; } set { timeTo = value; } }
		public string UserId { get; set; }
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
		public string GetFormatedName()
		{
			return $"{FirstName} {LastName}";
		}
		public void CreateMappings(Profile configuration)
		{
			configuration.CreateMap<AircraftReservation, FlightReservationModel>();
			configuration.CreateMap<FlightReservationModel, AircraftReservation>();
		}
	}
}
