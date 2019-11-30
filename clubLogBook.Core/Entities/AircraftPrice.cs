using ClubLogBook.Core.Common;
using ClubLogBook.Core.Interfaces;
using System;
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
		public string TailNumber { get; set; } = "";
		public string Description { get; set; } = "";
		public override string ToString()
		{

			return $"TailNumber:{TailNumber},PerMonth:{PerMonth},PerHour:{PerHour},Description:{Description}";

		}
	}
}
