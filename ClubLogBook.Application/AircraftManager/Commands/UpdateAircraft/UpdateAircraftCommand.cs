using ClubLogBook.Application.Common.Exceptions;
using ClubLogBook.Application.Common.Interfaces;
using ClubLogBook.Application.ViewModels;
using ClubLogBook.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using System.Text.RegularExpressions;

namespace ClubLogBook.Application.AircraftManager.Commands.UpdateAircraft
{
	public class UpdateAircraftCommand : IRequest
	{
		public AircraftViewModel AircraftViewModel { get; set; }
		public UpdateAircraftCommand(AircraftViewModel aircraftViewModel)
		{
			AircraftViewModel = aircraftViewModel;
		}
		public class UpdateAircraftCommandHandler : IRequestHandler<UpdateAircraftCommand,Unit>
		{
			private readonly IApplicationDbContext _context;
			private readonly IMapper _mapper;
			
			public UpdateAircraftCommandHandler(IApplicationDbContext context, IMapper mapper)
			{
				_mapper = mapper;
				_context = context;
			}
			public async Task<Unit> Handle(UpdateAircraftCommand request, CancellationToken cancellationToken)
			{
				Aircraft aircraft = new Aircraft();
				_mapper.Map(request.AircraftViewModel, aircraft);
				string newString = Regex.Replace(aircraft.TailNumber, "[^0-9a-zA-Z]", "");
				aircraft.TailNumber = newString.ToUpper();
				_context.Set<Aircraft>().Update(aircraft);
				

				await _context.SaveChangesAsync(cancellationToken);
				
				return Unit.Value;
			}
		}
	}
	
}
