using ClubLogBook.Core.Common;
using ClubLogBook.Core.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace ClubLogBook.Core.Entities
{
	
	public class AircraftPrice : AuditableEntity, IAircraftPrice
	{
		public AircraftPrice()
		{
			PerHour = 400;
			PerMonth = 450;
		}
		
		public Decimal PerMonth { get; set; }
		public Decimal PerHour { get; set; }
		[Required]
		[MaxLength(10)]
		public string TailNumber { get; set; } = "";
		
		[MaxLength(100)]
		public string Description { get; set; } = "";
		public override string ToString()
		{

			return $"TailNumber:{TailNumber},PerMonth:{PerMonth},PerHour:{PerHour},Description:{Description}";

		}
	}
}
