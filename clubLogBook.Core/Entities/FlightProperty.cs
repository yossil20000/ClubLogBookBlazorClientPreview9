using ClubLogBook.Core.Common;
using System;

namespace ClubLogBook.Core.Entities
{
	public interface IFlightProperty<TCustomPropertyType, TFlightRecord>
	{
		int FlightPropertyId { get; set; }
		int IntValue { get; set; }
		Decimal DecimalValue { get; set; }
		DateTime DateValue { get; set; }
		string StringValue { get; set; }
		TCustomPropertyType CustomPropertyType { get; set; }
		TFlightRecord FlightRecord { get; set; }
	}
	public class FlightProperty : AuditableEntity
	{
		public FlightProperty() { }

		public int IntValue { get; set; } = 0;
		public Decimal DecimalValue { get; set; } = 0;
		public DateTime DateValue { get; set; } =DateTime.Now;
		public string StringValue { get; set; } = "";
		public virtual CustomPropertyType CustomPropertyType { get; set; }
		public virtual FlightRecord FlightRecord { get; set; }
	}
}
