using System;
using System.Collections.Generic;

using System.Text;
using ClubLogBook.Core.Common;
using ClubLogBook.Core.Interfaces;
namespace ClubLogBook.Core.Entities
{
	public enum AircraftState
	{
		Service,
		Maintainace,
		OutOfService
	}
	public enum AircraftCategory : int
	{
		
		LigherThenAir = 0,
		Rotorcraft = 1,
		Airplane = 2,
		Glider = 3
	}
	public enum AircraftClass : int
	{
		SingleEngineLand = 0,
		SingleEngineSea = 1,
		MultiEngineLand = 2,
		MultiEngineSea = 3
	}
	public interface IAircraft<TAirCraftModel> : IAuditableEntity , IAggregateRoot
	{
		
		string TailNumber { get; set; }
		bool? Complex { get; set; }
		bool? HighPerformance { get; set; }
		bool? ConstantSpeedProp { get; set; }
		bool? TailWheel { get; set; }
		bool? Retractable { get; set; }
		bool? Turbine { get; set; }
		bool? Jet { get; set; }
		bool? Flaps { get; set; }
		byte[] Photo { get; set; }
		AircraftState AircraftState { get; set; }
		AircraftCategory AircraftCategory { get; set; }
		AircraftClass AircraftClass { get; set; }
	
		TAirCraftModel AirCraftModel { get; set; }

		// virtual FlightRecord FlightRecord { get; set; }

	}

	public class Aircraft : AuditableEntity, IAircraft< AirCraftModel> 
	{

		public Aircraft()
		{
			TailNumber = "";
			Complex = false;
			HighPerformance = false;
			ConstantSpeedProp = false;
			TailWheel = false;
			Retractable = false;
			Turbine = false;
			Jet = false;
			Flaps = false;
			Photo = new byte[0];
			AircraftState = AircraftState.Service;
			AircraftCategory = AircraftCategory.Airplane;
			AircraftClass = AircraftClass.SingleEngineLand;
			AirCraftModel = new AirCraftModel();
		}
		
		
		public string TailNumber { get; set; }
		public bool? Complex { get; set; }
		public bool? HighPerformance { get; set; }
		public bool? ConstantSpeedProp { get; set; }
		public bool? TailWheel { get; set; }
		public bool? Retractable { get; set; }
		public bool? Turbine { get; set; }
		public bool? Jet { get; set; }
		public bool? Flaps { get; set; }
		public byte[] Photo { get; set; }
		public AircraftState AircraftState { get; set; }
		public AircraftCategory AircraftCategory { get; set; }
		public AircraftClass AircraftClass { get; set; }


		public int AirCraftModelId { get; set; } = 0;
		public virtual AirCraftModel AirCraftModel { get; set; } 


		//public virtual FlightRecord FlightRecord { get; set; }
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append($"id:{Id} TailNumber:{TailNumber}");
			sb.AppendLine();
			sb.Append($"Complex:{Complex?.ToString()} State:{AircraftState} Category:{AircraftCategory} Class:{AircraftClass}");
			sb.AppendLine();
			sb.Append($"ModelId{AirCraftModelId} Description:{AirCraftModel?.Manufacturer} , {AirCraftModel?.ModelFullName}");
			return sb.ToString();

		}
	}
}
