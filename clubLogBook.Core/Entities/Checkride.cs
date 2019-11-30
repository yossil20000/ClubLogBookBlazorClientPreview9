using System;
using System.Collections.Generic;
using System.Text;
using ClubLogBook.Core.Common;
using ClubLogBook.Core.Interfaces;

namespace ClubLogBook.Core.Entities
{
	public interface ICheckride : IAuditableEntity
	{
		
		DateTime Date { get; set; }

	}
	public class Checkride : AuditableEntity, ICheckride
	{
		public Checkride() { }
		
		
		public DateTime Date { get; set; }

	}
}
