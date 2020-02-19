using FluentValidation;
using ClubLogBook.Application.Common.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using ClubLogBook.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClubLogBook.Application.Accounts.Commands.UpdateAccount
{
	public class UpdateAccountBalanceCommandValidator : AbstractValidator<UpdateAccountBalanceCommand>
	{
		private readonly IApplicationDbContext _context;
		public UpdateAccountBalanceCommandValidator(IApplicationDbContext context)
		{
			_context = context;
			RuleFor(v => v.MemeberId).GreaterThan(0).WithMessage("MemeberId Must Be > 0");
			
		}
	}
}
