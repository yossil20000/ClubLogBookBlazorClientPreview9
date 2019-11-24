using ClubLogBook.Core.Entities;
using System;
using AutoMapper;
using ClubLogBook.Application.Interfaces.Mapping;
namespace ClubLogBook.Application.ViewModels
{
	public class AirCraftModelViewModel : IHaveCustomMapping
	{
		public int Id {get;set;}
		public string TrainingDeviceKind {get;set;}
		public string Manufacturer {get;set;}
		public string Model {get;set;}
		public string TypeDesignation {get;set;}
		public string ModelFullName {get;set;}
		public DateTime? LastAnnual {get;set;}
		public DateTime? LastVOR {get;set;}
		public DateTime? LastAltimeter {get;set;}
		public DateTime? LastTransponder {get;set;}
		public DateTime? LastELT {get;set;}
		public DateTime? LastPitotStatic {get;set;}
		public DateTime? NextAnnual {get;set;}
		public DateTime? NextVOR {get;set;}
		public DateTime? NextAltimeter {get;set;}
		public DateTime? NextTransponder {get;set;}
		public DateTime? NextELT {get;set;}
		public DateTime? NextPitotStatic {get;set;}
		public double? Last100 {get;set;}
		public DateTime? OilChange {get;set;}
		public double? EngineTime {get;set;}
		public DateTime? RegistrationRenewalDate {get;set;}
		public string FrequentlyUsed {get;set;}
		public string Notes {get;set;}
		public string PrivateNotes {get;set;}
		public int FlightCount {get;set;}
		public double Hours {get;set;}
		public DateTime? FirstFlight {get;set;}
		public DateTime? LastFlight {get;set;}

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
		public AirCraftModelViewModel AirCraftModel {get;set;}
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
	
}
