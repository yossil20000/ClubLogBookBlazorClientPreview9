using System;
using System.Collections.Generic;
using System.Linq;
using ClubLogBook.Core.Entities;
using System.Linq.Expressions;

namespace ClubLogBook.Application.Specifications
{
	public sealed class ReservationSpecification : BaseSpecification<AircraftReservation>
	{
		public ReservationSpecification(int skip, int take, string tailNumber, string idNumber)
		{
			Expression<Func<AircraftReservation, bool>> criteria = a => true;

			if (tailNumber != "" && idNumber != "")
				criteria = a => a.TailNumber == tailNumber && a.IdNumber == idNumber;
			if (tailNumber == null && idNumber != "")
				criteria = a => a.IdNumber == idNumber;
			if (tailNumber != null && idNumber == null)
				criteria = a => a.TailNumber == tailNumber;
			SetCriteria(criteria);
			ApplyPaging(skip, take);
			ApplyOrderBy(ar => ar.DateFrom);
		}
		public ReservationSpecification(string tailNumber, string idNumber)
		{
			Expression<Func<AircraftReservation, bool>> criteria = a => true;

			if (tailNumber != "" && idNumber != "")
				criteria = a => a.TailNumber == tailNumber && a.IdNumber == idNumber;
			if (tailNumber == null && idNumber != "")
				criteria = a => a.IdNumber == idNumber;
			if (tailNumber != null && idNumber == null)
				criteria = a => a.TailNumber == tailNumber;
			SetCriteria(criteria);
		}
	}
}
