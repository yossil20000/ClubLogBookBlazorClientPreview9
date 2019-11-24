using System;
using System.Collections.Generic;
using System.Text;

namespace ClubLogBook.Core.Interfaces
{
	public interface IAircraftPrice : IBasicEntity, IAggregateRoot
	{


		Decimal PerMonth { get; set; }
		Decimal PerHour { get; set; }
		string TailNumber { get; set; }
	}
}
