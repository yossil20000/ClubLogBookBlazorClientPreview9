﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ClubLogBook.Core.Common;
using ClubLogBook.Core.Interfaces;
namespace ClubLogBook.Core.Entities
{
	
	public class Transaction : AuditableEntity, ITransaction<Invoice>
	{
		public Transaction() { }
		[Required]
		public DateTime Date { get; set; } = DateTime.Now;
		public TransactionType TransactionType { get; set; } = TransactionType.Credit;

		public Decimal Cash { get; set; } = 0;
		public Decimal Flight { get; set; } = 0;
		public virtual Invoice Invoice { get; set; } = new Invoice();
		[MaxLength(200)]
		public string Description { get; set; } = string.Empty;

		[Timestamp]
		public byte[] RowVersion { get; set; }
		public override string ToString()
		{
			return string.Format(
				$"id:{Id},Date:{Date},Date:{Date},Case:{Cash},Flight:{Flight},Desc:{Description}");
		}
	}
}
