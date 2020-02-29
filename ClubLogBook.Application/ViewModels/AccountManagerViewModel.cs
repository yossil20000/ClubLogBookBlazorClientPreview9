using System;
using System.Collections.Generic;


namespace ClubLogBook.Application.ViewModels
{
	public enum InvoiceTypeViewModel
	{
		Flight,
		FlightCredit,
		ClubFee,
		General


	}
	public enum InvoiceStateViewModel
	{
		Initial,
		Create,
		Deleted,
		InTransaction,
		Lock
	}
	public enum TransactionTypeVieModel
	{
		Credit,
		Debit
	}
	public class InvoiceViewModel /*: IBasicEntity,IAggregateRoot*/
	{
		public InvoiceViewModel() { }
		
		public int Id { get; set; }
		public decimal Amount { get; set; } = 0;
		public string Description { get; set; } = "";
		public InvoiceTypeViewModel InvoiceType { get; set; } = InvoiceTypeViewModel.Flight;
		public InvoiceStateViewModel InvoiceState { get; set; } = InvoiceStateViewModel.Create;
		public int InvoiceReferance { get; set; } = 0;

		public override string ToString()
		{
			return string.Format($"id:{Id},Amount:{Amount},InvoiceType:{InvoiceType},State:{InvoiceState},Desc:{Description}");
		}
	}
	public class AircraftPriceViewModel 
	{
		public AircraftPriceViewModel()
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
	public class TransactionViewModel 
	{
		public TransactionViewModel() { }
		public int Id { get; set; }
		public DateTime Date { get; set; } = DateTime.Now;
		public TransactionTypeVieModel TransactionType { get; set; } = TransactionTypeVieModel.Credit;

		public Decimal Cash { get; set; } = 0;
		public Decimal Flight { get; set; } = 0;
		public InvoiceViewModel Invoice { get; set; } = new InvoiceViewModel();
		public string Description { get; set; } = "";
		public override string ToString()
		{
			return string.Format(
				$"id:{Id},Date:{Date},Date:{Date},Case:{Cash},Flight:{Flight},Desc:{Description}");
		}
	}
	public class AccountViewModel/*: IBasicEntity, IAccount<TransactionViewModel>*/
	{
		public int Id { get; set; }
		public int MemeberId { get; set; } = 0;
		public string MemberInfo { get; set; } = "";
		public Decimal FlightBalance { get; set; } = 0;
		public Decimal CashBalance { get; set; } = 0;
		public string Description { get; set; } = "";

		public ICollection<TransactionViewModel> Transactions { get; set; } = new List<TransactionViewModel>();
		
		
	}
	
}
