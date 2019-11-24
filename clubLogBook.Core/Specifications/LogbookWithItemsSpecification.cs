using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ClubLogBook.Core.Entities;

namespace ClubLogBook.Core.Specifications
{
	public sealed class LogbookWithItemsSpecification : BaseSpecification<LogBook>
	{
		public LogbookWithItemsSpecification(Expression<Func<LogBook, bool>> criteria) : base(criteria)
		{
		}
		public LogbookWithItemsSpecification(int pilotId) : base(b => b.PilotId == pilotId)
		{
			AddInclude(i => i.FlightRecords);
			AddInclude(i => i.Pilot);
		}
		public LogbookWithItemsSpecification(int pilotId,int aircraftId) : base(b => b.PilotId == pilotId )
		{
			AddInclude(i => i.FlightRecords);
			AddInclude($"{nameof(LogBook.FlightRecords)}.{nameof(FlightRecord.Aircraft)}.{nameof(Aircraft.AirCraftModel)}");
		}
	}
}
