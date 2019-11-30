using System;
using System.Collections.Generic;

using System.Text;
using ClubLogBook.Core.Common;
using ClubLogBook.Core.Interfaces;

namespace ClubLogBook.Core.Entities
{
	public interface ILogBook<TPilot, TFlightRecord> : IAuditableEntity , IAggregateRoot
	{
		
		
		int PilotId { get; set; }

		TPilot Pilot { get; set; }
		IReadOnlyCollection<TFlightRecord> FlightRecords { get;  }


	}
	public class LogBook : AuditableEntity
	{
		private HashSet<FlightRecord> _flightRecords = new HashSet<FlightRecord>();
		public LogBook()
		{
			
		}
	
		public void Add(FlightRecord flightRecord)
		{
			_flightRecords.Add(flightRecord);
		}
		public int PilotId { get; set; }
		public virtual Pilot Pilot { get; set; }
		public virtual IReadOnlyCollection<FlightRecord> FlightRecords => _flightRecords;

		public StringBuilder Dumpall()
		{
			StringBuilder sb = new StringBuilder();
			//sb.Append(Pilot.PropertyToString());
			//sb.Append(this.PropertyToString());
			return sb;
		}
	}
}
