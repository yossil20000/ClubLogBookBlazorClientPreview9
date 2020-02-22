using System.Linq;
using System.Collections.Generic;
using System.Text;
using MediatR;
using ClubLogBook.Application.Accounts.Queries.GetAccountsList;
using System.Threading.Tasks;
using System.Threading;
using ClubLogBook.Application.Common.Interfaces;
using ClubLogBook.Core.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using ClubLogBook.Application.ViewModels;
using ClubLogBook.Core.Interfaces;

namespace ClubLogBook.Application.Flights.Queries
{
	public class ClubFlightListViewModel
	{
		public List<ClubFlightViewModel> clubFlightListViewModel { get; set; }
	}
	public class InvoiceToFlightComaprer : IEqualityComparer<Invoice>
	{
		public bool Equals(Invoice x, Invoice y)
		{
			return x.InvoiceReferance == y.InvoiceReferance && x.InvoiceType == InvoiceType.Flight;

		}

		public int GetHashCode(Invoice obj)
		{
			return obj.GetHashCode();
		}
	}
	public class GetAllFlightsQuery : IRequest<FlightRecordIndexViewModel>
	{

		public class GetAllFlightsQueryHandler : IRequestHandler<GetAllFlightsQuery, FlightRecordIndexViewModel>
		{
			private readonly IApplicationDbContext _context;
			private readonly IMapper _mapper;
			public GetAllFlightsQueryHandler(IApplicationDbContext context, IMapper mapper) => (_mapper, _context) = (mapper, context);


			public async Task<FlightRecordIndexViewModel> Handle(GetAllFlightsQuery request, CancellationToken cancellationToken)
			{
				InvoiceToFlightComaprer comparer = new InvoiceToFlightComaprer();
				FlightRecordIndexViewModel flightRecordIndexViewModel = new FlightRecordIndexViewModel();


				var flights = _context.Set<Flight>().ToList();
				
				ClubFlightViewModel clubFlightViewModel = new ClubFlightViewModel();
				flightRecordIndexViewModel.FlightRecords = _mapper.Map<List<Flight>, List<ClubFlightViewModel>>(flights);
				return flightRecordIndexViewModel;
			}
		}

		public class GetFlightsWithoutInvoiceQueryValidator : AbstractValidator<GetAllFlightsQuery>
		{

		}

	}
}
