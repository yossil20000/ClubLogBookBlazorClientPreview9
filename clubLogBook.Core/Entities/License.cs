using System;
using System.Collections.Generic;
using System.Text;
using ClubLogBook.Core.Common;
using ClubLogBook.Core.Interfaces;
namespace ClubLogBook.Core.Entities
{
	public interface ILicense : IAuditableEntity
	{
		
		int LicenseNumber { get; set; }
	}
	public class License : AuditableEntity, ILicense
	{
		public License() { }
		
		
		public int LicenseNumber { get; set; }
	}
}
