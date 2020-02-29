using ClubLogBook.Core.Common;
using ClubLogBook.Core.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace ClubLogBook.Core.Entities
{
	interface IAircraftReservation : IAuditableEntity
	{
		
		
		
		DateTime DateFrom { get; set; }
	
		DateTime DateTo { get; set; }

		string IdNumber { get; set; }

		string TailNumber { get; set; }


	}
	public class AircraftReservation : AuditableEntity, IAircraftReservation
	{
		public AircraftReservation() { }
	
		
		[Required]
		public DateTime DateFrom { get; set; }
		[Required]
		public DateTime DateTo { get; set; }

		public string IdNumber { get; set; }
	
		[MaxLength(10)]
		public string TailNumber { get; set; }
		
		[MaxLength(100)]
		public string ReservationInfo { get; set; } = "";
		[Required]
		public int AircraftId { get; set; }
		[Required]
		public int PilotId { get; set; }
		public static bool operator >(AircraftReservation f1, AircraftReservation f2)
		{
			return f1?.DateFrom > f2?.DateTo;
		}

		public static bool operator >=(AircraftReservation f1, AircraftReservation f2)
		{
			return f1?.DateFrom >= f2?.DateTo;
		}
		public static bool operator <=(AircraftReservation f1, AircraftReservation f2)
		{
			return f1?.DateTo <= f2?.DateFrom;
		}
		public static bool operator <(AircraftReservation f1, AircraftReservation f2)
		{
			return f1?.DateTo < f2?.DateFrom;
		}
		public static bool operator ==(AircraftReservation f1, AircraftReservation f2)
		{

			return f1?.DateTo == f2?.DateTo && f1?.DateFrom == f2?.DateFrom;
		}
		public static bool operator !=(AircraftReservation f1, AircraftReservation f2)
		{
			return !(f1 == f2);
		}
		public bool Intersect(AircraftReservation other)
		{
			if (other == null)
				return false;
			if (this.GetType() != other.GetType()) return false;
			if (other == this)
				return true;
			if (other >= this || other <= this)
				return false;
			return true;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;
			if (this.GetType() != obj.GetType()) return false;

			AircraftReservation other = (AircraftReservation)obj;
			if ((other.DateFrom >= this.DateFrom && other.DateFrom < this.DateTo || 
				other.DateTo <= this.DateTo && other.DateTo > this.DateFrom)
				&& other?.AircraftId == this?.AircraftId)
			{
				return true;
			}
			else
			{
				return false;
			}

		}


	}
}
