using System;
using System.Collections.Generic;
using System.Text;
using ClubLogBook.Core.Interfaces;

namespace ClubLogBook.Core.Entities
{
	public interface ICheckride : IBasicEntity
	{
		
		DateTime Date { get; set; }

	}
	public class Checkride : BaseEntity, ICheckride
	{
		public Checkride() { }
		
		
		public DateTime Date { get; set; }

	}
}
