using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using ClubLogBook.Application.ViewModels;
using System.Threading.Tasks;
using System.Threading;
using ClubLogBook.Core.Entities;
using ClubLogBook.Application.Interfaces;
using AutoMapper;
using System.Text.RegularExpressions;
namespace ClubLogBook.Application.AircraftManager.Commands
{
	public class CreateAircraftCommandHandler : IRequestHandler<CreateAircraftCommand, Unit>
	{
		private readonly IClubContext context;
		private readonly IMapper mapper;
		public CreateAircraftCommandHandler(IClubContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		public async Task<Unit> Handle(CreateAircraftCommand request, CancellationToken cancellationToken)
		{
			
			
			Aircraft aircraft = new Aircraft();
			mapper.Map(request.aircraftViewModel, aircraft);
			string newString = Regex.Replace(aircraft.TailNumber, "[^0-9a-zA-Z]", "");
			aircraft.TailNumber = newString.ToUpper();

			 await context.Set<Aircraft>().AddAsync(aircraft);
			await context.SaveChangesAsync(cancellationToken);
			mapper.Map(aircraft, request.aircraftViewModel);
			return Unit.Value;
		}
	}
}
