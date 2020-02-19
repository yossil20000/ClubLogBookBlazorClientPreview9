using ClubLogBook.Application.Common.Interfaces;
using ClubLogBook.Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using ClubLogBook.Application.Accounts.Queries.GetAccountInvoice;
using ClubLogBook.Core.Interfaces;
using AutoMapper;
namespace ClubLogBook.Application.Accounts.Commands.DeleteAccount
{
	public class DeleteAccountInvoiceCommand :  IRequest<bool>
	{
		public int Id { get; set; }
		public class DeleteAccountInvoiceCommandHandler : IRequestHandler<DeleteAccountInvoiceCommand, bool>
		{
			IApplicationDbContext _context;
			public DeleteAccountInvoiceCommandHandler(IApplicationDbContext context) => _context = context;

			public async Task<bool> Handle(DeleteAccountInvoiceCommand request, CancellationToken cancellationToken)
			{
				var entity = _context.Set<Invoice>().Find(request.Id);
				_context.Set<Invoice>().Remove(entity);
				var result =  await _context.SaveChangesAsync(cancellationToken);
				return result > 0 ? true : false;
			}
		}
	}
	public class DeleteAccountInvoiceCommandValidator : AbstractValidator<DeleteAccountInvoiceCommand>
	{
		public DeleteAccountInvoiceCommandValidator()
		{
			RuleFor(v => v.Id).GreaterThan(0).WithMessage("Id Must Be > 0");
		}
	}
}
