using System;
using System.Collections.Generic;
using System.Text;
using ClubLogBook.Core.Interfaces;
namespace ClubLogBook.Core.Entities
{

	
	public class Invoice : BaseEntity, IInvoice
	{
		public Invoice() { }

		public decimal Amount { get; set; } = 0;
		public string Description { get; set; } = "";
		public InvoiceType InvoiceType { get; set; } = InvoiceType.Flight;
		public InvoiceState InvoiceState { get; set; } = InvoiceState.Create;
		public int InvoiceReferance { get; set; } = 0;
		
		public override string ToString()
		{
			return string.Format($"id:{Id},Amount:{Amount},InvoiceType:{InvoiceType},State:{InvoiceState},Desc:{Description}");
		}
	}
}
