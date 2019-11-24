using System;
using System.Collections.Generic;
using System.Linq;
using ClubLogBook.Core.Entities;
using System.Linq.Expressions;
namespace ClubLogBook.Core.Specifications
{
	public class FlighWithSpecification : BaseSpecification<Flight>
	{
		public FlighWithSpecification(int skip,int take,int? aircraftId, int? pilotId) 
		{
			Expression<Func<Flight, bool>> criteria = a => true;
			
			if (aircraftId != null && pilotId != null)
				criteria = a => a.Aircraft.Id == aircraftId && a.Pilot.Id == pilotId;
			if (aircraftId == null && pilotId != null)
				criteria = a =>  a.Pilot.Id == pilotId;
			if (aircraftId != null && pilotId == null)
				criteria = a => a.Aircraft.Id == aircraftId ;
			SetCriteria(criteria);
			ApplyPaging(skip, take);
			ApplyOrderBy(ar => ar.EngineStart);
		}
		public FlighWithSpecification(int? aircraftId, int? pilotId) 
		{
			Expression<Func<Flight, bool>> criteria = a => true;

			if (aircraftId != null && pilotId != null)
				criteria = a => a.Aircraft.Id == aircraftId && a.Pilot.Id == pilotId;
			if (aircraftId == null && pilotId != null)
				criteria = a => a.Pilot.Id == pilotId;
			if (aircraftId != null && pilotId == null)
				criteria = a => a.Aircraft.Id == aircraftId;
			SetCriteria(criteria);
		}
	}
}
