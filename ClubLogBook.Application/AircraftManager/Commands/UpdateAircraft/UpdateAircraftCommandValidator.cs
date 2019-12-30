using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ClubLogBook.Application.Common.Interfaces;
using System.Threading;
using ClubLogBook.Core.Entities;

namespace ClubLogBook.Application.AircraftManager.Commands.UpdateAircraft
{
	public class UpdateAircraftCommandValidator : AbstractValidator<UpdateAircraftCommand>
	{
		IApplicationDbContext _context;
		public UpdateAircraftCommandValidator(IApplicationDbContext context)
		{
			_context = context;
			RuleFor(x => x.AircraftViewModel.Id).GreaterThan(0).WithMessage("Can't Update Aircraft With Id = 0");
			RuleFor(x => x.AircraftViewModel.Id).MustAsync(MustBeExistId).WithMessage("Update Aircraft Id Must Be Exist");
		}
		public async Task<bool> MustBeExistId(int id, CancellationToken ct)
		{
			return await _context.Set<Aircraft>().AllAsync(l => l.Id == id);
			
		}
	}
}
