using System;
using System.Collections.Generic;
using System.Linq;
using ClubLogBook.Core.Entities;

namespace ClubLogBook.Core.Specifications
{
	public sealed class AircraftLogbookSpecification : BaseSpecification<AircraftLogBook>
	{
		public AircraftLogbookSpecification(string tailNumber) : base(b => b.TaiNumber == tailNumber)
		{
			AddInclude($"{nameof(AircraftLogBook.Flights)}.{nameof(Flight.Aircraft)}");
			AddInclude($"{nameof(AircraftLogBook.Flights)}.{nameof(Flight.Pilot)}");
			//AddInclude(b => b.Members);
		}
		public AircraftLogbookSpecification(int skip,int take, string tailNumber) : base(b => b.TaiNumber == tailNumber)
		{
			AddInclude($"{nameof(AircraftLogBook.Flights)}.{nameof(Flight.Aircraft)}");
			AddInclude($"{nameof(AircraftLogBook.Flights)}.{nameof(Flight.Pilot)}");
			//AddInclude(b => b.Members);
			ApplyPaging(skip, take);
		}
	}
}
