using ClubLogBook.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClubLogBook.Application.Extenttions
{
	public static class EntityExtentions
	{
		public static bool IsFlightReservationValid(this List<AircraftReservation> reservations, AircraftReservation reservation)
		{
			var exist = reservations.Where(f => f.Intersect(reservation)).FirstOrDefault();
			if (exist != null && exist.Id != reservation.Id)
				return false;
			return true;
		}
	}
}
