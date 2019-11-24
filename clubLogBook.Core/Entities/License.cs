using System;
using System.Collections.Generic;
using System.Text;
using ClubLogBook.Core.Interfaces;
namespace ClubLogBook.Core.Entities
{
	public interface ILicense : IBasicEntity
	{
		
		int LicenseNumber { get; set; }
	}
	public class License :  BaseEntity ,ILicense
	{
		public License() { }
		
		
		public int LicenseNumber { get; set; }
	}
}
