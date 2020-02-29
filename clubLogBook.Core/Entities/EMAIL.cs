
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ClubLogBook.Core.Common;
using ClubLogBook.Core.Interfaces;

namespace ClubLogBook.Core.Entities
{
	public class EMAIL : AuditableEntity
	{
		public EMAIL() { Id = 0; }
		[Required]
		[MaxLength(100)]
		public string EMail {get;set; } = "";
		public ContactType Type { get; set; } = ContactType.HOME;
		
	}
}
