using System;
using System.Collections.Generic;


namespace ClubLogBook.Application.Models
{
	public enum InvoiceTypeModel
	{
		Flight,
		FlightCredit,
		ClubFee,
		General


	}
	public enum InvoiceStateModel
	{
		Initial,
		Create,
		Deleted,
		InTransaction,
		Lock
	}
	public enum TransactionTypeModel
	{
		Credit,
		Debit
	}
	public class InvoiceModel /*: IBasicEntity,IAggregateRoot*/
	{
		public InvoiceModel() { }
		
		public int Id { get; set; }
		public decimal Amount { get; set; } = 0;
		public string Description { get; set; } = "";
		public InvoiceTypeModel InvoiceType { get; set; } = InvoiceTypeModel.Flight;
		public InvoiceStateModel InvoiceState { get; set; } = InvoiceStateModel.Create;
		public int InvoiceReferance { get; set; } = 0;

		public override string ToString()
		{
			return string.Format($"id:{Id},Amount:{Amount},InvoiceType:{InvoiceType},State:{InvoiceState},Desc:{Description}");
		}
	}
	public class AircraftPriceModel 
	{
		public AircraftPriceModel()
		{
			PerHour = 400;
			PerMonth = 450;
		}
		public  int Id { get; set; }
		public Decimal PerMonth { get; set; }
		public Decimal PerHour { get; set; }
		public string TailNumber { get; set; } = "";
		public string Description { get; set; } = "";
		public override string ToString()
		{

			return $"TailNumber:{TailNumber},PerMonth:{PerMonth},PerHour:{PerHour},Description:{Description}";

		}
	}
	public class TransactionModel 
	{
		public TransactionModel() { }
		public int Id { get; set; }
		public DateTime Date { get; set; } = DateTime.Now;
		public TransactionTypeModel TransactionType { get; set; } = TransactionTypeModel.Credit;

		public Decimal Cash { get; set; } = 0;
		public Decimal Flight { get; set; } = 0;
		public InvoiceModel Invoice { get; set; } = new InvoiceModel();
		public string Description { get; set; } = "";
		public override string ToString()
		{
			return string.Format(
				$"id:{Id},Date:{Date},Date:{Date},Case:{Cash},Flight:{Flight},Desc:{Description}");
		}
	}
	public class AccountModel/*: IBasicEntity, IAccount<TransactionViewModel>*/
	{
		public int Id { get; set; }
		public int MemeberId { get; set; } = 0;
		public string MemberInfo { get; set; } = "";
		public Decimal FlightBalance { get; set; } = 0;
		public Decimal CashBalance { get; set; } = 0;
		public string Description { get; set; } = "";

		public ICollection<TransactionModel> Transactions { get; set; } = new List<TransactionModel>();
		
		
	}
	
}
