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
		public void CreateMappings(Profile configuration)
		{
			configuration.CreateMap<Account, AccountLookupModel>();
		}
	}
}
