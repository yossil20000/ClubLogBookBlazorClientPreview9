using System;
using System.Collections.Generic;

using ClubLogBook.Core.Interfaces;
namespace ClubLogBook.Core.Entities
{
	
	public class Account : BaseEntity, IAccount<Transaction>
	{
		public Account() { }
		public Account(int memberId)
		{
			MemeberId = memberId;
			
		}

		public int MemeberId { get; set; } = 0;
		public string MemberInfo { get; set; } = "";
		public Decimal FlightBalance { get; set; } = 0;
		public Decimal CashBalance { get; set; } = 0;
		public string Description { get; set; } = "";

		public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
		

	}
}
