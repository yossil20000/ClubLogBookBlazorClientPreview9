using System;
using System.Collections.Generic;
using System.Text;

namespace ClubLogBook.Core.Interfaces
{
	public interface IAuditableEntity 
	{
		string CreatedBy { get; set; }

		DateTime Created { get; set; }

		string LastModifiedBy { get; set; }

		DateTime? LastModified { get; set; }
	}
}
