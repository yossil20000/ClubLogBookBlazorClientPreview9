using System;
using System.Collections.Generic;
using System.Text;
using ClubLogBook.Core.Entities;
namespace ClubLogBook.Core.Interfaces
{
	public enum InvoiceType
	{
		Flight,
		FlightCredit,
		ClubFee,
		General,


	}
	public enum InvoiceState
	{

		Create,
		Deleted,
		InTransaction,
		Lock
	}
	public interface IAccount<TTransaction> : IAggregateRoot, IBasicEntity
	{


		
		int MemeberId { get; set; }
		Decimal FlightBalance { get; set; }
		Decimal CashBalance { get; set; }


		ICollection<TTransaction> Transactions { get; set; }



	}
}
