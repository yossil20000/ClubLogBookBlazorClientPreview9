using System;
using System.Collections.Generic;
using System.Text;

namespace ClubLogBook.Core.Interfaces
{
	public interface IMember
	{
	
		string IdNumber { get; set; }
		string FirstName { get; set; }
		string LastName { get; set; }
		string MiddleName { get; set; }
		Gender Gender { get; set; }
		DateTime DateOfBirth { get; set; }
		byte[] Photo { get; set; }
		Decimal Height { get; set; }
		Decimal Weight { get; set; }
	}
}
