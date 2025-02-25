﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ClubLogBook.Core.Interfaces;
using ClubLogBook.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace ClubLogBook.Core.Entities
{
	public interface IAirCraftModel : IAuditableEntity
	{

		
		
		
		string TrainingDeviceKind { get; set; }
		string Manufacturer { get; set; }
		string Model { get; set; }
		string TypeDesignation { get; set; }
		string ModelFullName { get; set; }
		DateTime? LastAnnual { get; set; }
		DateTime? LastVOR { get; set; }
		DateTime? LastAltimeter { get; set; }
		DateTime? LastTransponder { get; set; }
		DateTime? LastELT { get; set; }
		DateTime? LastPitotStatic { get; set; }
		DateTime? NextAnnual { get; set; }
		DateTime? NextVOR { get; set; }
		DateTime? NextAltimeter { get; set; }
		DateTime? NextTransponder { get; set; }
		DateTime? NextELT { get; set; }
		DateTime? NextPitotStatic { get; set; }
		Decimal? Last100 { get; set; }
		DateTime? OilChange { get; set; }
		Decimal? EngineTime { get; set; }
		DateTime? RegistrationRenewalDate { get; set; }
		string FrequentlyUsed { get; set; }
		string Notes { get; set; }
		string PrivateNotes { get; set; }
		int FlightCount { get; set; }
		Decimal Hours { get; set; }
		DateTime? FirstFlight { get; set; }
		DateTime? LastFlight { get; set; }

		

	}
	public class AirCraftModel : AuditableEntity, IAggregateRoot
	{
		public AirCraftModel()
		{
			//this.Aircraft = new HashSet<Aircraft>();
			LastAnnual = DateTime.Now.AddYears(-5);
			LastVOR = DateTime.Now.AddYears(-5);
			LastAltimeter = DateTime.Now.AddYears(-5);
			LastTransponder = DateTime.Now.AddYears(-5);
			LastELT = DateTime.Now.AddYears(-5);
			LastPitotStatic = DateTime.Now.AddYears(-5);
			NextAnnual = DateTime.Now.AddYears(-5);
			NextVOR = DateTime.Now.AddYears(-5);
			NextAltimeter = DateTime.Now.AddYears(-5);
			NextTransponder = DateTime.Now.AddYears(-5);
			NextELT = DateTime.Now.AddYears(-5);
			NextPitotStatic = DateTime.Now.AddYears(-5);
		}
		[MaxLength(100)]
		public string TrainingDeviceKind { get; set; } = "";
		[MaxLength(100)]
		public string Manufacturer { get; set; } = "";
		[MaxLength(25)]
		public string Model { get; set; } = "";
		[MaxLength(25)]
		public string TypeDesignation { get; set; } = "";
		[MaxLength(100)]
		public string ModelFullName { get; set; } = "";
		public DateTime? LastAnnual { get; set; }
		public DateTime? LastVOR { get; set; }
		public DateTime? LastAltimeter { get; set; }
		public DateTime? LastTransponder { get; set; }
		public DateTime? LastELT { get; set; }
		public DateTime? LastPitotStatic { get; set; }
		public DateTime? NextAnnual { get; set; }
		public DateTime? NextVOR { get; set; }
		public DateTime? NextAltimeter { get; set; }
		public DateTime? NextTransponder { get; set; }
		public DateTime? NextELT { get; set; }
		public DateTime? NextPitotStatic { get; set; }
		public Decimal? Last100 { get; set; } = -1;
		public DateTime? OilChange { get; set; } = DateTime.Now.AddYears(-5); 
		public Decimal? EngineTime { get; set; } = -1;
		public DateTime? RegistrationRenewalDate { get; set; } = DateTime.Now.AddYears(-5);
		[MaxLength(25)]
		public string FrequentlyUsed { get; set; } = "";
		[MaxLength(100)]
		public string PublicNotes { get; set; } = "";
		[MaxLength(100)]
		public string PrivateNotes { get; set; } = "";
		public int FlightCount { get; set; } = -1;
		public Decimal Hours { get; set; } = -1;
		public DateTime? FirstFlight { get; set; } = DateTime.Now.AddYears(-5);
		public DateTime? LastFlight { get; set; } = DateTime.Now.AddYears(-5);
		[MaxLength(100)]
		public string Notes { get; set; } = "";

	}
}
