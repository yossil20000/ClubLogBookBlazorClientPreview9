﻿using ClubLogBook.Application.Common.Exceptions;
using ClubLogBook.Application.Common.Interfaces;
using ClubLogBook.Application.Models;
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
	public class CreateFlightCommand : IRequest<int>
	{
		public ClubFlightModel ClubFlightModel { get; set; }
		public int ClubId { get; set; } 
		public class CreateFlightCommandHandler : IRequestHandler<CreateFlightCommand, int>
		{
			private readonly IApplicationDbContext _context;
			private readonly IMapper _mapper;
			public CreateFlightCommandHandler(IApplicationDbContext context, IMapper mapper)
			{
				_context = context;
				_mapper = mapper;
			}
			public async Task<int> Handle(CreateFlightCommand request, CancellationToken cancellationToken)
			{

				Flight flight = new Flight(request.ClubFlightModel.Date, request.ClubFlightModel.Routh, request.ClubFlightModel.EngineStart, request.ClubFlightModel.EngineEnd);
				flight.HobbsStart = request.ClubFlightModel.HobbsStart;
				flight.HobbsEnd = request.ClubFlightModel.HobbsEnd;
				//flight = _mapper.Map<ClubFlightViewModel, Flight>(request.ClubFlightViewModel);
				var club = _context.Set<Club>().Find(request.ClubId);
				var pilot = _context.Set<Pilot>().Find(request.ClubFlightModel.Pilot.Id);
				var aircraft = _context.Set<Aircraft>().Find(request.ClubFlightModel.Aircraft.Id);
				//int clubId = club1.FirstOrDefault().Id;
				//Club club = _clubRepository.GetAllByIdAsync(clubId);
				//Club club = await _clubRepository.GetByIdAsync(clubId);
				//club = await _clubRepository.ListAllAsync();

				
				flight.Pilot = pilot;
				flight.Aircraft = aircraft;
				var logbookflights = _context.Set<AircraftLogBook>().Where(lb => lb.TaiNumber == aircraft.TailNumber).FirstOrDefault();

				var exist = logbookflights.Flights.Where(f => f.Equals(flight)).FirstOrDefault();
				if (exist != null)
					return -1;

				
				if (logbookflights != null)
				{
					logbookflights.Add(flight);
					
					return await _context.SaveChangesAsync(cancellationToken);
				}
				else
					return -1;
			}
		}
	}

	public class CreateFlightCommandValidator : AbstractValidator<CreateFlightCommand>
	{

	}
}
