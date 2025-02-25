﻿using ClubLogBook.Core.Entities;
using System;
using AutoMapper;
using ClubLogBook.Application.Interfaces.Mapping;
using System.Text;
using System.ComponentModel;

namespace ClubLogBook.Application.Models
{
	public class AirCraftModelModel : IHaveCustomMapping
	{
		public int AnnualPeriodCheck { get; } = 12;
		public int VORPeriodCheck { get; } = 30;
		public int AltimeterPeriodCheck { get; } = 24;
		public int TransponderPeriodCheck { get; } = 24;
		public int ELTPeriodCheck { get; } = 12;
		public int PitotStaticPeriodCheck { get; } = 24;
		public int Id { get; set; } = 0;
		public string TrainingDeviceKind { get; set; } = "";
		public string Manufacturer { get; set; } = "";
		public string Model { get; set; } = "";
		public string TypeDesignation { get; set; } = "";
		public string ModelFullName { get; set; } = "";
		private DateTime lastAnnual = DateTime.Now.AddYears(-5);
		public DateTime LastAnnual
		{
			get { return lastAnnual; }
			set
			{
				if (value != lastAnnual)
				{
					lastAnnual = value; NextAnnual = lastAnnual.AddMonths(AnnualPeriodCheck);
				}
			}
		}
		private DateTime lastVOR  = DateTime.Now.AddYears(-5);
		public DateTime LastVOR
		{
			get { return lastVOR; }
			set
			{
				if (value != lastVOR)
				{
					lastVOR = value; NextVOR = lastVOR.AddDays(VORPeriodCheck);
				}
			}
		}

		private DateTime lastAltimeter = DateTime.Now.AddYears(-5);
		public DateTime LastAltimeter
		{
			get { return lastAltimeter; }
			set
			{
				if (value != lastAltimeter)
				{
					lastAltimeter = value; NextAltimeter = lastAltimeter.AddMonths(AltimeterPeriodCheck);
				}
			}
		}
		private DateTime lastTransponder = DateTime.Now.AddYears(-5);
		public DateTime LastTransponder
		{
			get { return lastTransponder; }
			set
			{
				if (value != lastTransponder)
				{
					lastTransponder = value; NextTransponder = lastTransponder.AddMonths(TransponderPeriodCheck);
				}
			}
		}
		private DateTime lastELT = DateTime.Now.AddYears(-5);
		public DateTime LastELT
		{
			get { return lastELT; }
			set
			{
				if (value != lastELT)
				{
					lastELT = value; NextELT = lastELT.AddMonths(ELTPeriodCheck);
				}
			}
		}
		private DateTime lastPitotStatic = DateTime.Now.AddYears(-5);
		public DateTime LastPitotStatic
		{
			get { return lastPitotStatic; }
			set
			{
				if (value != lastPitotStatic)
				{
					lastPitotStatic = value; NextPitotStatic = lastPitotStatic.AddMonths(24);
				}
			}
		}
		public DateTime NextAnnual { get; set; }
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
			configuration.CreateMap<AirCraftModel, AirCraftModelModel>();
		}
		
	}
	public class AircraftModel : IHaveCustomMapping
	{
		public int Id { get; set; } = 0;
		public string TailNumber { get; set; } = "";
		public bool? Complex { get; set; } = false;
		public bool? HighPerformance { get; set; } = false;
		public bool? ConstantSpeedProp { get; set; } = false;
		public bool? TailWheel { get; set; } = false;
		public bool? Retractable { get; set; } = false;
		public bool? Turbine { get; set; } = false;
		public bool? Jet { get; set; } = false;
		public bool? Flaps { get; set; } = false;
		public byte[] Photo { get; set; } = null;
		public AircraftStateModel AircraftState { get; set; } = AircraftStateModel.OutOfService;
		public AircraftCategoryModel AircraftCategory { get; set; } = AircraftCategoryModel.Airplane;
		public AircraftClassModel AircraftClass { get; set; } = AircraftClassModel.SingleEngineLand;
		public AirCraftModelModel AirCraftModel { get; set; } = new AirCraftModelModel();
		public AircraftModel(Aircraft aircraft)
		{
			Id = aircraft.Id;
			TailNumber = aircraft.TailNumber;
		}
		public AircraftModel()
		{
			
		}

		public void CreateMappings(Profile configuration)
		{
			configuration.CreateMap<Aircraft, AircraftModel>();
			configuration.CreateMap<AircraftModel,Aircraft>();
		}
		public AircraftStatusModel Get()
		{
			return new AircraftStatusModel((AirCraftModel.NextAnnual - DateTime.Now).Days, (AirCraftModel.NextVOR - DateTime.Now).Days,
				(AirCraftModel.NextELT - DateTime.Now).Days, (AirCraftModel.NextPitotStatic - DateTime.Now).Days,
				(AirCraftModel.NextAltimeter - DateTime.Now).Days ,(AirCraftModel.NextTransponder - DateTime.Now).Days,
				100 - (AirCraftModel.EngineTime - AirCraftModel.Hours));
			
		}
	}

	public enum AircraftStateModel
	{
		Service,
		Maintainace,
		OutOfService
	}
	public enum AircraftCategoryModel : int
	{

		LigherThenAir = 0,
		Rotorcraft = 1,
		Airplane = 2,
		Glider = 3
	}
	public enum AircraftClassModel : int
	{
		SingleEngineLand = 0,
		SingleEngineSea = 1,
		MultiEngineLand = 2,
		MultiEngineSea = 3
	}

	public class AircraftStatusModel
	{
		public AircraftStatusModel(int annualDays,int vorDays,int eltDays,int pitotStaticDays,int altimeterDays, int transponderDays, decimal hr100Days)
		{
			AnnualInDays = annualDays;VorInDays = vorDays;EltInDays = eltDays;PitotStaticInDays = pitotStaticDays;TransponderInDays = transponderDays;HR100InDays = hr100Days;
		}
		public int AnnualInDays { get; } = 45;
		public int VorInDays { get; }
		public int EltInDays { get; }
		public int PitotStaticInDays { get; }

		public int AltimeterInDays { get; }
		public int TransponderInDays { get; }
		public decimal HR100InDays { get; }
	}
}
