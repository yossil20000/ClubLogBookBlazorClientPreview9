using ClubLogBook.Application.Common.Interfaces;
using ClubLogBook.Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using ClubLogBook.Application.Accounts.Queries.GetAccountInvoice;
using ClubLogBook.Core.Interfaces;
using AutoMapper;
using System;
using ClubLogBook.Application.Interfaces.Mapping;

namespace ClubLogBook.Application.Accounts.Commands.UpdateAccount
{
	public class AccountTransactionModel :  IHaveCustomMapping
	{
		public int AccountId { get; set; } = 0;
		public int TransactionId { get; set; } = 0;
		public DateTime Date { get; set; } = DateTime.Now;
		public TransactionType TransactionType { get; set; } = TransactionType.Credit;

		public Decimal Cash { get; set; } = 0;
		public Decimal Flight { get; set; } = 0;
		public virtual Invoice Invoice { get; set; } = new Invoice();
		public string Description { get; set; } = string.Empty;
		public bool ProcessInvoice()
		{
			
			Invoice.InvoiceState = InvoiceState.InTransaction;
			if (Invoice.InvoiceType == InvoiceType.Flight)
				Flight = Invoice.Amount;
			else
				Cash = Invoice.Amount;
			return true;

		}

		public void CreateMappings(Profile configuration)
		{
			configuration.CreateMap<AccountTransactionModel, Transaction>();
			configuration.CreateMap<Transaction , AccountTransactionModel > ();
		}
	}
	public class UpdateAccountTransactionCommand : IRequest<bool>
	{
		public AccountTransactionModel AccountTransactionModel { get; set; } = new AccountTransactionModel();

		public class UpdateAccountTransactionCommandHandler : IRequestHandler<UpdateAccountTransactionCommand, bool>
		{
			private readonly IApplicationDbContext _context;
			private readonly IMapper _mapper;
			public UpdateAccountTransactionCommandHandler(IApplicationDbContext context, IMapper mapper)
			{ _context = context; _mapper = mapper; }
			public async Task<bool> Handle(UpdateAccountTransactionCommand request, CancellationToken cancellationToken)
			{
				var entity = _mapper.Map<AccountTransactionModel, Transaction>(request.AccountTransactionModel);
				if (entity == null)
					return false;
				
				var entityAccount = _context.Set<Account>().Find(request.AccountTransactionModel.AccountId);
				
				if(entityAccount != null && entity != null)
				{
					entityAccount.Transactions.Add(entity);
					_context.Set<Account>().Update(entityAccount);
				}
				
				int result = await _context.SaveChangesAsync(cancellationToken);
				return result > 0 ? true : false;
			}
		}
	}
	public class UpdateAccountTransactionCommandValidator : AbstractValidator<UpdateAccountTransactionCommand>
	{
		public UpdateAccountTransactionCommandValidator()
		{
			RuleFor(v => v.AccountTransactionModel.AccountId).GreaterThan(0).WithMessage("AccountId Must Be > 0");
			

		}
	}
}
