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
	public class FlightProperty
	{
		public FlightProperty() { }
		
		public int FlightPropertyId { get; set; }
		public int IntValue { get; set; }
		public Decimal DecimalValue { get; set; }
		public DateTime DateValue { get; set; }
		public string StringValue { get; set; }
		public virtual CustomPropertyType CustomPropertyType { get; set; }
		public virtual FlightRecord FlightRecord { get; set; }
	}
}
