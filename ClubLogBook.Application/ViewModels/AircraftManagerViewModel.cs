using ClubLogBook.Core.Entities;
using System;
using AutoMapper;
using ClubLogBook.Application.Interfaces.Mapping;
using System.Text;

namespace ClubLogBook.Application.ViewModels
{
	public class AirCraftModelViewModel : IHaveCustomMapping
	{
		public int AnnualPeriodCheck { get; } = 12;
		public int VORPeriodCheck { get; } = 30;
		public int AltimeterPeriodCheck { get; } = 24;
		public int TransponderPeriodCheck { get; } = 24;
		public int ELTPeriodCheck { get; } = 12;
		public int PitotStaticPeriodCheck { get; } = 24;
		public int Id { get; set; } = 0;
		public string TrainingDeviceKind {get;set;}
		public string Manufacturer {get;set;}
		public string Model {get;set;}
		public string TypeDesignation {get;set;}
		public string ModelFullName {get;set;}
		public DateTime LastAnnual { get; set; } = DateTime.Now.AddMonths(-24);
		public DateTime LastVOR {get;set;} =  DateTime.Now.AddMonths(-24);
		public DateTime LastAltimeter {get;set;} = DateTime.Now.AddMonths(-24);
		public DateTime LastTransponder {get;set;} = DateTime.Now.AddMonths(-24);
		public DateTime LastELT {get;set;} = DateTime.Now.AddMonths(-24);
		public DateTime LastPitotStatic {get;set;} = DateTime.Now.AddMonths(-24);
		public DateTime NextAnnual { get { return LastAnnual.AddMonths(12); } }
		public DateTime NextVOR {get;set;} = DateTime.Now;
		public DateTime NextAltimeter {get;set;} = DateTime.Now;
		public DateTime NextTransponder {get;set;} = DateTime.Now;
		public DateTime NextELT {get;set;} = DateTime.Now;
		public DateTime NextPitotStatic {get;set;} = DateTime.Now;
		public decimal Last100 { get; set; } = 0;
		public DateTime OilChange {get;set;} = DateTime.Now.AddMonths(-24);
		public decimal EngineTime { get; set; } = 0;
		public DateTime RegistrationRenewalDate {get;set;} = DateTime.Now.AddMonths(-24);
		public string FrequentlyUsed { get; set; } = "";
		public string Notes { get; set; } = "";
		public string PrivateNotes { get; set; } = "";
		public int FlightCount { get; set; } = 0;
		public decimal Hours { get; set; } = 0;
		public DateTime FirstFlight {get;set;} =  DateTime.Now;
		public DateTime LastFlight {get;set;} =  DateTime.Now;

		public void CreateMappings(Profile configuration)
		{
			configuration.CreateMap<AirCraftModel, AirCraftModelViewModel>();
		}
		
	}
	public class AircraftViewModel : IHaveCustomMapping
	{
		public int Id {get;set;}
		public string TailNumber {get;set;}
		public bool? Complex {get;set;}
		public bool? HighPerformance {get;set;}
		public bool? ConstantSpeedProp {get;set;}
		public bool? TailWheel {get;set;}
		public bool? Retractable {get;set;}
		public bool? Turbine {get;set;}
		public bool? Jet {get;set;}
		public bool? Flaps {get;set;}
		public byte[] Photo {get;set;}
		public AircraftStateViewModel AircraftState {get;set;}
		public AircraftCategoryViewModel AircraftCategory {get;set;}
		public AircraftClassViewModel AircraftClass {get;set;}
		public AirCraftModelViewModel AirCraftModel { get; set; } = new AirCraftModelViewModel();
		public AircraftViewModel(Aircraft aircraft)
		{
			Id = aircraft.Id;
			TailNumber = aircraft.TailNumber;
		}
		public AircraftViewModel()
		{
			
		}

		public void CreateMappings(Profile configuration)
		{
			configuration.CreateMap<Aircraft, AircraftViewModel>();
		}
		public AircraftStatusViewModel Get()
		{
			return new AircraftStatusViewModel((AirCraftModel.NextAnnual - AirCraftModel.LastAnnual).Days, (AirCraftModel.NextVOR - AirCraftModel.LastVOR).Days,
				(AirCraftModel.NextELT - AirCraftModel.LastELT).Days, (AirCraftModel.NextPitotStatic - AirCraftModel.LastPitotStatic).Days,
				(AirCraftModel.NextAltimeter - AirCraftModel.LastAltimeter).Days ,(AirCraftModel.NextTransponder - AirCraftModel.LastTransponder).Days,
				100 - (AirCraftModel.EngineTime - AirCraftModel.Hours));
			
		}
	}

	public enum AircraftStateViewModel
	{
		Service,
		Maintainace,
		OutOfService
	}
	public enum AircraftCategoryViewModel : int
	{

		LigherThenAir = 0,
		Rotorcraft = 1,
		Airplane = 2,
		Glider = 3
	}
	public enum AircraftClassViewModel : int
	{
		SingleEngineLand = 0,
		SingleEngineSea = 1,
		MultiEngineLand = 2,
		MultiEngineSea = 3
	}

	public class AircraftStatusViewModel
	{
		public AircraftStatusViewModel(int annualDays,int vorDays,int eltDays,int pitotStaticDays,int altimeterDays, int transponderDays, decimal hr100Days)
		{
			AnnualInDays = annualDays;VorInDays = vorDays;EltInDays = eltDays;PitotStaticInDays = pitotStaticDays;TransponderInDays = transponderDays;HR100InDays = hr100Days;
		}
		public int AnnualInDays { get; }
		public int VorInDays { get; }
		public int EltInDays { get; }
		public int PitotStaticInDays { get; }

		public int AltimeterInDays { get; }
		public int TransponderInDays { get; }
		public decimal HR100InDays { get; }
	}
}
