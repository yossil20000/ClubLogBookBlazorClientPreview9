using System;
using System.Collections.Generic;
using System.Linq;
using ClubLogBook.Core.Entities;

namespace ClubLogBook.Application.Specifications
{
	public class PilotWithSpeciification : BaseSpecification<Pilot>
	{
		public PilotWithSpeciification(int skip, int take ,int? pilotId,string pilotName, string pilotIdNumber)
			:base(i => (i.FirstName.Contains(pilotName) || i.LastName.Contains(pilotName)) && i.IdNumber.Contains(pilotIdNumber)  || i.Id == pilotId )
		{
			ApplyPaging(skip, take);
			
		}
	}
}
