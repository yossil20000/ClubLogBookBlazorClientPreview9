using System;
using System.Collections.Generic;
using System.Text;

namespace ClubLogBook.Core.Interfaces
{
	public interface IInvoice : IBasicEntity,IAggregateRoot
	{

		
		decimal Amount { get; set; }
		string Description { get; set; }
		InvoiceType InvoiceType { get; set; }
		InvoiceState InvoiceState { get; set; }
		int InvoiceReferance { get; set; }

	}
}
