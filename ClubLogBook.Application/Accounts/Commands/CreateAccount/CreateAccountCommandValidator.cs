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
				   .NotEmpty().WithMessage("Title is required.")
				   .GreaterThan(0).WithMessage("Title must not exceed 200 characters.")
				   .MustAsync(BeUniqueMemberId).WithMessage("The specified title already exists.");
		}

		public async Task<bool> BeUniqueMemberId(int id, CancellationToken cancellationToken)
		{
			return await _context.Set<Account>().AllAsync(l => l.Id != id);
		}
	}
}
