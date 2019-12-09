using System;
using System.Collections.Generic;
using System.Text;
using ClubLogBook.Core.Common;
using ClubLogBook.Core.Interfaces;
namespace ClubLogBook.Core.Entities
{
	public interface IAircraftLogBook<TFlight> : IAuditableEntity, IAggregateRoot
	{

		
		
		string TaiNumber { get; set; }
		ICollection<TFlight> Flights { get;  }
	}
	public class AircraftLogBook : AuditableEntity, IAircraftLogBook<Flight>
	{
		private HashSet<Flight> flights = new HashSet<Flight>();
		public AircraftLogBook()
		{
			
		}
		public void Add(Flight flight)
		{
			flights.Add(flight);
		}
		public int AircraftLogBookId { get; set; }
		public string TaiNumber { get; set; } = "";
		public virtual ICollection<Flight> Flights => flights;
	}
}
