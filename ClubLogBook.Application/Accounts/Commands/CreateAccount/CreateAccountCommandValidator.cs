using FluentValidation;
using ClubLogBook.Application.Common.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using ClubLogBook.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClubLogBook.Application.Accounts.Commands.CreateAccount
{
	public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
	{
		private readonly IApplicationDbContext _context;
		public CreateAccountCommandValidator(IApplicationDbContext context)
		{
			_context = context;
			RuleFor(v => v.MemeberId)
				   .NotEmpty().WithMessage("MemeberId is required.")
				   .GreaterThan(0).WithMessage("MemeberId Must Be > 0")
				   .MustAsync(BeUniqueMemberId).WithMessage("The specified MemeberId: already exists.");
		}

		public async Task<bool> BeUniqueMemberId(int memeberId, CancellationToken cancellationToken)
		{
			return await _context.Set<Account>().AllAsync(l => l.MemeberId != memeberId);
		}
	}
}
