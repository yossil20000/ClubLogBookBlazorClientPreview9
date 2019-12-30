using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClubLogBook.Application.Common.Interfaces;
using ClubLogBook.Core.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
namespace ClubLogBook.Application.AircraftManager.Commands.CreateAircraft
{
	public class CreateAircraftCommandValidator :AbstractValidator<CreateAircraftCommand>
	{
		IApplicationDbContext _context;
		public CreateAircraftCommandValidator(IApplicationDbContext context)
		{
			_context = context;
			RuleFor(x => x.aircraftViewModel.Id).Equals(0);
			RuleFor(x => x.aircraftViewModel.TailNumber).NotEmpty();
			RuleFor(x => x.aircraftViewModel.TailNumber).MustAsync(MustBeUniqTailNumber).WithMessage("TailNumber Already Exist");
		}
		public async Task<bool> MustBeUniqTailNumber(string tailNumber, CancellationToken ct)
		{
			return await _context.Set<Aircraft>().AllAsync(l => l.TailNumber != tailNumber);
			var entity = _context.Set<Aircraft>().Where(x => x.TailNumber == tailNumber).FirstOrDefault();
			return entity == null ? true : false;
		}
	}
}
