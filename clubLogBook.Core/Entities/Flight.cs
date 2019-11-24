using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ClubLogBook.Core.Interfaces;
namespace ClubLogBook.Core.Entities
{
	public interface IFlight : IBasicEntity
	{

		
		DateTime Date { get; set; }
		Aircraft Aircraft { get; set; }
		Pilot Pilot { get; set; }
		string Routh { get; set; }
		Decimal EngineStart { get; set; }
		Decimal EngineEnd { get; set; }
		Decimal HobbsStart { get; set; }
		Decimal HobbsEnd { get; set; }

	}
	public class Flight :BaseEntity
	{
		public Flight() {
			Pilot = new Pilot();
			Aircraft = new Aircraft();
		}

		//public Flight(FlightRecord fr, Aircraft aircraft, Pilot pilot)
		//{
		//	Date = fr.Date;
		//	Routh = fr.Routh == "" ? string.Format($"{fr.From},{fr.To}") : string.Format($"{fr.Routh}");
		//	EngineStart = fr.EngineStart == 0 ? fr.HobbsStart : fr.EngineStart;
		//	EngineEnd = fr.EngineEnd == 0 ? fr.HobbsEnd : fr.EngineEnd;
		//	HobbsStart = fr.HobbsStart;
		//	HobbsEnd = fr.HobbsEnd;
		//	Aircraft = aircraft;
		//	Pilot = pilot;

		//}
		public Flight(DateTime date, string routh, Decimal es,Decimal ee)
		{
			Date = date;
			Routh = routh;
			EngineStart = es;
			EngineEnd = ee;
			HobbsStart = es;
			HobbsEnd = ee;
			Pilot = new Pilot();
			Aircraft = new Aircraft();
			

		}

		//public int Id { get; set; }

		public DateTime Date { get; set; } = DateTime.Now;
		
		virtual public Aircraft Aircraft { get; set; }
		virtual public Pilot Pilot { get; set; }
		public string Routh { get; set; }
		
		public Decimal EngineStart { get; set; }
		
		public Decimal EngineEnd { get; set; }
		public Decimal HobbsStart { get; set; }
		public Decimal HobbsEnd { get; set; }
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append($"Id:{Id} Date:{Date} Routh:{Routh} EngienStart:{EngineStart} EngienEnd:{EngineEnd}");
			return sb.ToString();
		}
		public static bool operator >(Flight f1, Flight f2)
		{
			return f1?.EngineStart > f2?.EngineEnd;
		}

		public static bool operator >=(Flight f1, Flight f2)
		{
			return f1?.EngineStart >= f2?.EngineEnd;
		}
		public static bool operator <=(Flight f1, Flight f2)
		{
			return f1?.EngineEnd <= f2?.EngineStart;
		}
		public static bool operator <(Flight f1, Flight f2)
		{
			return f1?.EngineEnd < f2?.EngineStart;
		}
		public static bool operator ==(Flight f1, Flight f2)
		{

			return f1?.EngineEnd == f2?.EngineEnd && f1?.EngineStart == f2?.EngineStart;
		}
		public static bool operator !=(Flight f1, Flight f2)
		{
			return !(f1 == f2);
		}
		
		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;
			if (this.GetType() != obj.GetType()) return false;

			Flight other = (Flight)obj;
			if (other.EngineStart >= this.EngineStart && other.EngineStart < this.EngineEnd || other.EngineEnd <= this.EngineEnd && other.EngineEnd > this.EngineStart)
			{
				return true;
			}
			else
			{
				return false;
			}

		}

		public virtual object Clone()
		{
			return this;
		}

		protected virtual Flight Create()
		{
			return new Flight();
		}

		//public  bool Equals(Flight other)
		//{
		//	if (other.EngineStart >= this.EngineStart  && other.EngineStart <= this.EngineEnd || other.EngineEnd <= this.EngineEnd  &&  other.EngineEnd >= this.EngineStart)
		//	{
		//		return true;
		//	}
		//	else
		//	{
		//		return false;
		//	}
		//}
	}
	//public class FlightView : BaseEntity
	//{
	//	public static bool operator !=(FlightView f1, FlightView f2)
	//	{
	//		return !(f1 == f2);
	//	}
	//	public static bool operator ==(FlightView f1, FlightView f2)
	//	{

	//		return f1?.EngineEnd == f2?.EngineEnd && f1?.EngineStart == f2?.EngineStart;
	//	}
	//	public static bool operator >=(FlightView f1, FlightView f2)
	//	{
	//		return f1?.EngineStart >= f2?.EngineEnd;
	//	}
	//	public static bool operator <=(FlightView f1, FlightView f2)
	//	{
	//		return f1.EngineEnd <= f2.EngineStart;
	//	}
	//	public static bool operator <(FlightView f1, FlightView f2)
	//	{
	//		return f1.EngineEnd < f2.EngineStart;
	//	}
	//	public static bool operator >(FlightView f1, FlightView f2)
	//	{
	//		return f1.EngineStart > f2.EngineEnd;
	//	}
	//	//}
	//	public override string ToString()
	//	{
	//		StringBuilder sb = new StringBuilder();
	//		sb.Append($"Id:{Id} Date:{Date} Routh:{Routh} EngienStart:{EngineStart} EngienEnd:{EngineEnd}");
	//		return sb.ToString();
	//	}
	//	public override bool Equals(object obj)
	//	{
	//		if (obj == null)
	//			return false;
	//		if (this.GetType() != obj.GetType()) return false;

	//		FlightView other = (FlightView)obj;
	//		if (other.EngineStart >= this.EngineStart && other.EngineStart < this.EngineEnd || other.EngineEnd <= this.EngineEnd && other.EngineEnd > this.EngineStart)
	//		{
	//			return true;
	//		}
	//		else
	//		{
	//			return false;
	//		}

	//	}
	//	public FlightView()
	//	{
	//		Pilot = new Pilot();
	//		Aircraft = new Aircraft();
	//	}

	//	//public Flight(FlightRecord fr, Aircraft aircraft, Pilot pilot)
	//	//{
	//	//	Date = fr.Date;
	//	//	Routh = fr.Routh == "" ? string.Format($"{fr.From},{fr.To}") : string.Format($"{fr.Routh}");
	//	//	EngineStart = fr.EngineStart == 0 ? fr.HobbsStart : fr.EngineStart;
	//	//	EngineEnd = fr.EngineEnd == 0 ? fr.HobbsEnd : fr.EngineEnd;
	//	//	HobbsStart = fr.HobbsStart;
	//	//	HobbsEnd = fr.HobbsEnd;
	//	//	Aircraft = aircraft;
	//	//	Pilot = pilot;

	//	//}
	//	public FlightView(DateTime date, string routh, Decimal es, Decimal ee)
	//	{
	//		Date = date;
	//		Routh = routh;
	//		EngineStart = es;
	//		EngineEnd = ee;
	//		HobbsStart = es;
	//		HobbsEnd = ee;
	//		Pilot = new Pilot();
	//		Aircraft = new Aircraft();


	//	}

	//	//public int Id { get; set; }

	//	public DateTime Date { get; set; } = DateTime.Now;

	//	virtual public Aircraft Aircraft { get; set; }
	//	virtual public Pilot Pilot { get; set; }
	//	public string Routh { get; set; }

	//	public Decimal EngineStart { get; set; }

	//	public Decimal EngineEnd { get; set; }
	//	public Decimal HobbsStart { get; set; }
	//	public Decimal HobbsEnd { get; set; }
		
	//}
}
