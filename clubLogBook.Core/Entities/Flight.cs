using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ClubLogBook.Core.Interfaces;
using ClubLogBook.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace ClubLogBook.Core.Entities
{
	public interface IFlight : IAuditableEntity
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
	public class Flight : AuditableEntity
	{
		public Flight()
		{
			Pilot = new Pilot();
			Aircraft = new Aircraft();
		}
		public Flight(DateTime date, string routh, Decimal es, Decimal ee)
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
		[Required]
		public DateTime Date { get; set; } = DateTime.Now;
		[Required]
		virtual public Aircraft Aircraft { get; set; }
		[Required]
		virtual public Pilot Pilot { get; set; }
		[MaxLength(100)]
		public string Routh { get; set; } = "";
		[Required]
		public Decimal EngineStart { get; set; } = 0;
		[Required]

		public Decimal EngineEnd { get; set; } = 0;
		[Required]
		public Decimal HobbsStart { get; set; } = 0;
		[Required]
		public Decimal HobbsEnd { get; set; } = 0;
		[Timestamp]
		public byte[] RowVersion { get; set; }
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

	}
}
