using ClubLogBook.Application.Common.Exceptions;
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
	public class DeleteFlightCommand : IRequest<bool>
	{
		public int Id { get; set; }
		public DeleteFlightCommand(int id) => Id = id;
		public class DeleteFlightCommandHandler : IRequestHandler<DeleteFlightCommand, bool>
		{
			private readonly IApplicationDbContext _context;
			
			public DeleteFlightCommandHandler(IApplicationDbContext context)
			{
				_context = context;
			}
			public async Task<bool> Handle(DeleteFlightCommand request, CancellationToken cancellationToken)
			{

				Flight flight = _context.Set<Flight>().Where(f => f.Id == request.Id).FirstOrDefault();
				if (flight != null)
				{
					_context.Set<Flight>().Remove(flight);
					return await _context.SaveChangesAsync(cancellationToken) == 1 ? true : false;
				}
				else
					return false;
			}
		}
	}

	public class DeleteFlightCommandValidator : AbstractValidator<DeleteFlightCommand>
	{

	}
}
