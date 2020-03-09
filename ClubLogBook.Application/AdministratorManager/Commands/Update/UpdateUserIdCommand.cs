using ClubLogBook.Application.Common.Interfaces;
using ClubLogBook.Application.Models;
using ClubLogBook.Core.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClubLogBook.Application.AdministratorManager.Commands
{
	public class UpdateUserIdCommand : IRequest<int>
	{
		public string UserId { get; set; }
		public int PilotId { get; set; }
		public UpdateUserIdCommand(int pilotId, string userId)
		{
			PilotId = pilotId;
			UserId = userId;
		}
		public class UpdateUserIdCommandHandler : IRequestHandler<UpdateUserIdCommand, int>
		{
			private readonly IApplicationDbContext _context;
			public UpdateUserIdCommandHandler(IApplicationDbContext context)
			{
				_context = context;
			}
			public async Task<int> Handle(UpdateUserIdCommand request, CancellationToken cancellationToken)
			{
				var pilot = _context.Set<Pilot>().Where(pilot => pilot.Id == request.PilotId).FirstOrDefault();
				if(pilot != null)
				{
					pilot.UserId = request.UserId;
					_context.Set<Pilot>().Update(pilot);
					return await _context.SaveChangesAsync(cancellationToken);
				}
				return -1;
			}
		}
	}
	public class UpdateUserIdCommandValidator : AbstractValidator<UpdateUserIdCommand>
	{
		public UpdateUserIdCommandValidator()
		{
			RuleFor(x => x.PilotId).GreaterThan(0).WithMessage("Pilot Id Mus Be Greater Then 0");
			RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId Must Be Not Empty");
		}
	}
}
