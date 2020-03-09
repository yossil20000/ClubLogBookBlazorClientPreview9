using AutoMapper;
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

namespace ClubLogBook.Application.AdministratorManager.Queries
{
	public class GetPilotByUserIdQuery : IRequest<PilotSelectModel>
	{
		public string UserId { get; set; }
		public GetPilotByUserIdQuery(string userId)
		{
			UserId = userId;
		}

	}
	public class GetPilotByUserIdQueryHandler : IRequestHandler<GetPilotByUserIdQuery, PilotSelectModel>
	{
		IApplicationDbContext _context;
		IMapper _mapper;
		public GetPilotByUserIdQueryHandler(IApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public Task<PilotSelectModel> Handle(GetPilotByUserIdQuery request, CancellationToken cancellationToken)
		{
			var pilot = _context.Set<Pilot>().Where(i => i.UserId == request.UserId).FirstOrDefault();
			PilotSelectModel pilotSelectModel = new PilotSelectModel();
			if(pilot != null)
			{
				pilotSelectModel = _mapper.Map<Pilot, PilotSelectModel>(pilot);
			}
			return Task.FromResult(pilotSelectModel);
		}
	}
	public class GetPilotByUserIdQueryValidator : AbstractValidator<GetPilotByUserIdQuery>
	{
		public GetPilotByUserIdQueryValidator()
		{
			RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId Must Be Not Empty");
		}
	}
}
