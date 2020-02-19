using FluentValidation;
using ClubLogBook.Application.Common.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using ClubLogBook.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClubLogBook.Application.Accounts.Commands.UpdateAccount
{
	public class UpdateAccountInfoCommandValidator : AbstractValidator<UpdateAccountInfoCommand>
	{
		private readonly IApplicationDbContext _context;
		public UpdateAccountInfoCommandValidator(IApplicationDbContext context)
		{
			_context = context;
			RuleFor(v => v.MemeberId).GreaterThan(0).WithMessage("MemeberId Must Be > 0");
			RuleFor(v => v.MemberInfo).NotEmpty().WithMessage("MemberInfo Mus Contain Info");
		}
	}
}
