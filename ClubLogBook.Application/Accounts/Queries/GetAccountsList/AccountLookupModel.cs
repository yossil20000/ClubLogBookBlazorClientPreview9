using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ClubLogBook.Core.Entities;
using ClubLogBook.Application.Interfaces.Mapping;
namespace ClubLogBook.Application.Accounts.Queries.GetAccountsList
{
	public class AccountLookupModel : IHaveCustomMapping
	{
		public int Id { get; set; }
		public int MemeberId { get; set; } = 0;
		public string MemberInfo { get; set; } = "";
		public Decimal FlightBalance { get; set; } = 0;
		public Decimal CashBalance { get; set; } = 0;
		public string Description { get; set; } = "";

		public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
		public void CreateMappings(Profile configuration)
		{
			configuration.CreateMap<Account, AccountLookupModel>();
		}
	}
}
