using System;
using System.Collections.Generic;
using System.Text;
using ClubLogBook.Core.Entities;
namespace ClubLogBook.Infrastructure.Data.Import
{
	public class AirplanemodelImportcs : AirCraftModel
	{
		public int AircraftID { get; set; }
		public string TailNumber { get; set; }
		public string CategoryClass { get; set; }
	}
}
