using AutoMapper;
using ClubLogBook.Application.Common.Models;
using ClubLogBook.Application.Interfaces.Mapping;
using ClubLogBook.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;
namespace ClubLogBook.Application.Models
{
	public class FlightReservationCreateModel 
	{
		public FlightReservationCreateModel(FlightReservationModel reservationModel)
		{
			DateFrom = reservationModel.DateFrom;
			DateTo = reservationModel.DateTo;
		}
		public FlightReservationCreateModel()
		{
			DateTime now = DateTime.Now;

			DateFrom = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
			DateTo = DateFrom;
		}
		[Required]
		
		public int PilotId { get; private set; } = 0;
		[Required]
		public  int AircraftId { get; private set; } = 0;
		public DateTime DateFrom { get; set; } 
		
		public DateTime DateTo { get; set; } 

		private string _pilotSelect = "";
		[Required]
		[MinLength(1)]
		public string PilotSelect { get { return _pilotSelect; } set { _pilotSelect = value; PilotId = Int32.Parse(_pilotSelect); } }
		private string _aircraftSelect = "";
		[Required]
		[MinLength(1)]
		public string AircraftSelect { get { return _aircraftSelect; } set { _aircraftSelect = value; AircraftId = Int32.Parse(_aircraftSelect); } }
		
		public string ReturnResult { get; set; } = "";
		
	}
	public class FlightReservationModel : IHaveCustomMapping
	{
		public int Id { get; set; }
		public FlightReservationModel() { DateTo = DateTime.Now.AddHours(1); DateFrom = DateTime.Now; }


		[Required(ErrorMessage = "Required")]
		[DataType(DataType.DateTime)]
		
		public DateTime DateFrom { get; set; } = DateTime.Now;

		[Required(ErrorMessage = "Required")]
		[DataType(DataType.DateTime)]
		//[DateGreaterThan(otherPropertyName = "DateFrom", ErrorMessage = "")]
		public DateTime DateTo { get; set; } = DateTime.Now;
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
		
		public string UserId { get; set; }
		public int PilotId { get; set; }
		public int AircraftId {get;set;}
		public int ClubId { get; set; }
		
		public string GetFormatedName()
		{
			return $"{FirstName} {LastName}";
		}
		public void CreateMappings(Profile configuration)
		{
			configuration.CreateMap<AircraftReservation, FlightReservationModel>();
			configuration.CreateMap<FlightReservationModel, AircraftReservation>();
		}
		public string ReturnResult { get; set; } = "";
	}
}
