using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ClubLogBook.Core.Common;
using ClubLogBook.Core.Interfaces;
namespace ClubLogBook.Core.Entities
{

	
	public class Invoice : AuditableEntity, IInvoice
	{
		public Invoice() { }
		[Required]
		public Decimal Amount { get; set; } = 0;
		[MaxLength(100)]
		public string Description { get; set; } = "";
		public InvoiceType InvoiceType { get; set; } = InvoiceType.Flight;
		public InvoiceState InvoiceState { get; set; } = InvoiceState.Create;
		public int InvoiceReferance { get; set; } = 0;
		[Timestamp]
		public byte[] RowVersion { get; set; }

		public override string ToString()
		{
			return string.Format($"id:{Id},Amount:{Amount},InvoiceType:{InvoiceType},State:{InvoiceState},Desc:{Description}");
		}
	}
}
