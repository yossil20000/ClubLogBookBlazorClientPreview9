
using System;
using System.Collections.Generic;
using System.Text;
using ClubLogBook.Core.Interfaces;

namespace ClubLogBook.Core.Entities
{
	public class EMAIL : BaseEntity
	{
		public EMAIL() { Id = 0; }
		public string EMail {get;set;} 
		public ContactType Type { get; set; }
		
	}
}
