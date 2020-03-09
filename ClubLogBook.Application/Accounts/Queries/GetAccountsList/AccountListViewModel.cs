using System;
using System.Collections.Generic;
using System.Text;

using AutoMapper;
using ClubLogBook.Core.Entities;
namespace ClubLogBook.Application.Accounts.Queries.GetAccountsList
{
	public class AccountListModel 
	{
		public IList<AccountLookupModel> Accounts { get; set; }
	}
}
