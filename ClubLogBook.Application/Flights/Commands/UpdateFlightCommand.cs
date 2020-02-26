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
using FluentValidation;
namespace ClubLogBook.Application.Flights.Commands
{
	public class UpdateFlightCommand : IRequest<int>
	{
		public ClubFlightViewModel ClubFlightViewModel { get; set; }
		public class UpdateFlightCommandHandler : IRequestHandler<UpdateFlightCommand, int>
		{
			private readonly IApplicationDbContext _context;
			private readonly IMapper _mapper;
			public UpdateFlightCommandHandler(IApplicationDbContext context, IMapper mapper)
			{
				_context = context;
				_mapper = mapper;
			}
			public async Task<int> Handle(UpdateFlightCommand request, CancellationToken cancellationToken)
			{

				Flight flight = _context.Set<Flight>().Where(f => f.Id == request.ClubFlightViewModel.Id).FirstOrDefault();
				if (flight != null)
				{
					flight.Routh = request.ClubFlightViewModel.Routh;
					flight.Date = request.ClubFlightViewModel.Date;
					flight.EngineStart = request.ClubFlightViewModel.EngineStart;
					flight.EngineEnd = request.ClubFlightViewModel.EngineEnd;
					flight.HobbsStart = request.ClubFlightViewModel.HobbsStart;
					flight.HobbsEnd = request.ClubFlightViewModel.HobbsEnd;
					return await _context.SaveChangesAsync(cancellationToken);
				}
				else
					return -1;
			}
		}
	}

	public class UpdateFlightCommandValidator : AbstractValidator<UpdateFlightCommand>
	{

	}
}
