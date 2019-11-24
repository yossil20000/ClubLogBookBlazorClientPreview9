using System;
using System.Collections.Generic;
using System.Text;

namespace ClubLogBook.Core.Entities
{
	public class PilotClub
	{
		public int PilotId { get; set; }
		public Pilot Pilot { get; set; }
		public int ClubId { get; set; }
		public Club Club { get; set; }
	}
}
