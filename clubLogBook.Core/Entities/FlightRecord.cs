using System;
using System.Collections.Generic;
using System.Text;
using ClubLogBook.Core.Common;
using ClubLogBook.Core.Interfaces;
namespace ClubLogBook.Core.Entities
{
	public interface IFlightRecord<TAircraft> : IAuditableEntity ,IAggregateRoot
	{


		
		System.DateTime Date { get; set; }
		string From { get; set; }
		string Routh { get; set; }
		string To { get; set; }
		Decimal TotalTime { get; set; }
		Decimal PIC { get; set; }
		Decimal SIC { get; set; }
		Decimal Dual { get; set; }
		Decimal Solo { get; set; }
		Decimal CFI { get; set; }

		Decimal XCountry { get; set; }
		Decimal Night { get; set; }
		Decimal Instrument { get; set; }
		Decimal Simulated { get; set; }
		Decimal GroundSimulator { get; set; }

		short Approaches { get; set; }

		short Holds { get; set; }
		short Landings { get; set; }
		short NightLandings { get; set; }
		Decimal AerobaticsTime { get; set; }
		Decimal AgriculturalTime { get; set; }
		Decimal BannerTowingTime { get; set; }
		Decimal BushTime { get; set; }
		Decimal HobbsStart { get; set; }
		Decimal HobbsEnd { get; set; }
		Decimal EngineStart { get; set; }
		Decimal EngineEnd { get; set; }



		
		string FlightComments { get; set; }
		// virtual FlightProperty FlightPropertie { get; set; }
		// virtual Student Student { get; set; }
		TAircraft Aircraft { get; set; }
	}
	public class FlightRecord : AuditableEntity
	{
		public FlightRecord()
		{
			Date = DateTime.Now;
			From = "LLHA";
			To = "LLHA";
			Routh = "";
		}
		
		public System.DateTime Date { get; set; }
		public string From { get; set; }
		public string Routh { get; set; }
		public string To { get; set; }
		public Decimal TotalTime { get; set; }
		public Decimal PIC { get; set; }
		public Decimal SIC { get; set; }
		public Decimal Dual { get; set; }
		public Decimal Solo { get; set; }
		public Decimal CFI { get; set; }

		public Decimal XCountry { get; set; }
		public Decimal Night { get; set; }
		public Decimal Instrument { get; set; }
		public Decimal Simulated { get; set; }
		public Decimal GroundSimulator { get; set; }

		public short Approaches { get; set; }

		public short Holds { get; set; }
		public short Landings { get; set; }
		public short NightLandings { get; set; }
		public Decimal AerobaticsTime { get; set; }
		public Decimal AgriculturalTime { get; set; }
		public Decimal BannerTowingTime { get; set; }
		public Decimal BushTime { get; set; }
		public Decimal HobbsStart { get; set; }
		public Decimal HobbsEnd { get; set; }
		public Decimal EngineStart { get; set; }
		public Decimal EngineEnd { get; set; }
		public string FlightComments { get; set; }
		//public virtual FlightProperty FlightPropertie { get; set; }
		//public virtual Student Student { get; set; }
		public virtual Aircraft Aircraft { get; set; }
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append($"AircrafId:{Id} Date:{Date} Start:{EngineStart} End:{EngineEnd}");
			return sb.ToString();
		}
	}
}
