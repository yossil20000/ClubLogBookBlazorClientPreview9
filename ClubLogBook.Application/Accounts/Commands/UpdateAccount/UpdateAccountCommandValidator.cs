using FluentValidation;
using ClubLogBook.Application.Common.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using ClubLogBook.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClubLogBook.Application.Accounts.Commands.UpdateAccount
{
	public class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
	{
		private readonly IApplicationDbContext _context;
		public UpdateAccountCommandValidator(IApplicationDbContext context)
		{
			_context = context;
			RuleFor(v => v.MemeberId).GreaterThan(0).WithMessage("MemeberId Must Be > 0");
			RuleFor(v => v.MemberInfo).NotEmpty().WithMessage("MemberInfo Mus Contain Info");
		}
	}
}
