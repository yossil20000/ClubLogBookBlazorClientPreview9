using System;
using System.Collections.Generic;

using ClubLogBook.Core.Interfaces;
using ClubLogBook.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace ClubLogBook.Core.Entities
{

	public class Account : AuditableEntity, IAccount<Transaction>
	{
		public Account() { }
		public Account(int memberId)
		{
			MemeberId = memberId;

		}
		[Required]
		[MaxLength(10)]
		public string AccoumtNumber { get; set; }
		public int MemeberId { get; set; } = 0;
		[MaxLength(200)]
		public string MemberInfo { get; set; } = "";
		public Decimal FlightBalance { get; set; } = 0;
		public Decimal CashBalance { get; set; } = 0;
		[MaxLength(200)]
		public string Description { get; set; } = "";
		public virtual Member Member {get;set;} 
		public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
		[Timestamp]
		public byte[] RowVersion { get; set; }

	}
}
