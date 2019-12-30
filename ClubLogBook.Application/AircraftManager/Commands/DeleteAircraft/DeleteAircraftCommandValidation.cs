using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using System.Threading.Tasks;
using System.Threading;
using ClubLogBook.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using ClubLogBook.Core.Entities;

namespace ClubLogBook.Application.AircraftManager.Commands.DeleteAircraft
{
	public class DeleteAircraftCommandValidator : AbstractValidator<DeleteAircraftCommand>
	{
		private readonly IApplicationDbContext _context;
		public DeleteAircraftCommandValidator(IApplicationDbContext context)
		{
			_context = context;
			RuleFor(x => x.Id).GreaterThan(0).WithMessage("Cant Delete Aircraft With Id=0");
			RuleFor(x => x.Id).MustAsync(MustBeExistId).WithMessage("Deleted Aircraft Id Must Be Exist");
		}
		public async Task<bool> MustBeExistId(int id, CancellationToken ct)
		{
			return await _context.Set<Aircraft>().AllAsync(l => l.Id == id);

		}
	}
}
