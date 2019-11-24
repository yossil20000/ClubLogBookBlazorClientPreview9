using System;
using System.Collections.Generic;
using System.Text;
using ClubLogBook.Core.Entities;
namespace ClubLogBook.Core.Interfaces
{
	public enum TransactionType
	{
		Credit,
		Debit
	}
	public interface ITransaction<T> : IBasicEntity,IAggregateRoot
	{

		DateTime Date { get; set; }
		TransactionType TransactionType { get; set; }

		Decimal Cash { get; set; }
		Decimal Flight { get; set; }
		T Invoice { get; set; }
		string Description { get; set; }
	}
}
