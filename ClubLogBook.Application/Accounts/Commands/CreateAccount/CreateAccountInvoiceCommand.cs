using ClubLogBook.Application.Common.Interfaces;
using ClubLogBook.Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using ClubLogBook.Application.Accounts.Queries.GetAccountInvoice;
using ClubLogBook.Core.Interfaces;
using AutoMapper;
namespace ClubLogBook.Application.Accounts.Commands.CreateAccount
{
	public class CreateAccountInvoiceCommand :IRequest<int>
	{
		public AccountInvoiceModel accountInvoiceModel { get; set; } = new AccountInvoiceModel();
		public class CreateAccountInvoiceCommandHandler : IRequestHandler<CreateAccountInvoiceCommand, int>
		{
			IApplicationDbContext _context;
			IMapper _mapper;
			public CreateAccountInvoiceCommandHandler(IApplicationDbContext context, IMapper mapper)
			{
				_context = context;
				_mapper = mapper;
			}
			public async Task<int> Handle(CreateAccountInvoiceCommand request, CancellationToken cancellationToken)
			{
				var entity = _mapper.Map<Invoice>(request.accountInvoiceModel);
				int result = 0;
				if (entity != null)
				{
					_context.Set<Invoice>().Update(entity);
					result = await _context.SaveChangesAsync(cancellationToken);
				}
				return result;
			}
		}

	}
	public class CreateAccountInvoiceCommandValidator : AbstractValidator<CreateAccountInvoiceCommand>
	{
		private readonly IApplicationDbContext _context;
		public CreateAccountInvoiceCommandValidator(IApplicationDbContext context)
		{
			_context = context;
			RuleFor(v => v.accountInvoiceModel.Id).Equal(0).WithMessage("Id Must Equeal 0");
			RuleFor(v => v.accountInvoiceModel.MemeberId).GreaterThan(0).WithMessage("MemberId Must Be > 0");
			RuleFor(v => v.accountInvoiceModel.InvoiceState).MustAsync(BeCreate).WithMessage("The specified MemeberId: already exists.");
		}
		public async Task<bool> BeCreate(InvoiceState invoiceState, CancellationToken cancellationToken)
		{
			return   invoiceState == InvoiceState.Create;
		}
	}
}
