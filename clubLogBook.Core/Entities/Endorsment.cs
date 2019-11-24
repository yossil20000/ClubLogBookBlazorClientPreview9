using System;
using System.Collections.Generic;
using System.Text;
using ClubLogBook.Core.Interfaces;

namespace ClubLogBook.Core.Entities
{
	public interface IEndorsment : IBasicEntity
	{
		
		DateTime GivenDate { get; set; }
		DateTime ExpiredTime { get; set; }
	}
	public class Endorsment : BaseEntity, IEndorsment
	{
		public Endorsment() { }
		
		
		public DateTime GivenDate { get; set; }
		public DateTime ExpiredTime { get; set; }
	}
}
