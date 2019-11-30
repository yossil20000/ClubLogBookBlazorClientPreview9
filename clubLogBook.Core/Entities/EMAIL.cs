
using System;
using System.Collections.Generic;
using System.Text;
using ClubLogBook.Core.Common;
using ClubLogBook.Core.Interfaces;

namespace ClubLogBook.Core.Entities
{
	public class EMAIL : AuditableEntity
	{
		public EMAIL() { Id = 0; }
		public string EMail {get;set;} 
		public ContactType Type { get; set; }
		
	}
}
