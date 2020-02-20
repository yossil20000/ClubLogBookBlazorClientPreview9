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

namespace ClubLogBook.Application.Flights.Queries.GetFlightWithoutInvoice
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
	public class GetFlightsWithoutInvoiceQuery : IRequest<ClubFlightListViewModel>
	{
		
		public class GetFlightsWithoutInvoiceQueryHandler : IRequestHandler<GetFlightsWithoutInvoiceQuery, ClubFlightListViewModel>
		{
			private readonly IApplicationDbContext _context;
			private readonly IMapper _mapper;
			public GetFlightsWithoutInvoiceQueryHandler(IApplicationDbContext context, IMapper mapper) => (_mapper, _context) = (mapper, context);


			public async Task<ClubFlightListViewModel> Handle(GetFlightsWithoutInvoiceQuery request, CancellationToken cancellationToken)
			{
				InvoiceToFlightComaprer comparer = new InvoiceToFlightComaprer();
				ClubFlightListViewModel clubFlightListViewModel = new ClubFlightListViewModel();

				var query = from b in _context.Set<Flight>()
							from p in _context.Set<Invoice>().Where(p => b.Id != p.InvoiceReferance && p.InvoiceType != InvoiceType.Flight)
							select b;
				//var flights = (from f in _context.Set<Flight>()
				//			   join i in _context.Set<Invoice>() on
				//			   new {  IsFlight = true }
				//			   equals
				//			   new { IsFlight = !(i.InvoiceType == InvoiceType.Flight && i.InvoiceReferance > 0) }
				//			   select f);
				//var a = _context.Set<Flight>().Where(f =>  !(_context.Set<Invoice>().Select(i => i.InvoiceReferance).Contains(f.Id)));
				var flighInvoices = _context.Set<Invoice>().Where(i => i.InvoiceType == InvoiceType.Flight);
				var flightWithoutInvoice = _context.Set<Flight>().Include(p => p.Pilot).Where(f => !(flighInvoices.Select(i => i.InvoiceReferance).Contains(f.Id))).ToList();
				//var noExistList = (from n in _context.Set<Flight>()
				//				   join o in _context.Set<Invoice>() on n.Id equals o.InvoiceReferance
				//				   where o.InvoiceType == InvoiceType.Flight
				//				   select n).ToList();
				ClubFlightViewModel clubFlightViewModel = new ClubFlightViewModel();
				clubFlightListViewModel.clubFlightListViewModel = _mapper.Map<List<Flight>, List<ClubFlightViewModel>>(flightWithoutInvoice);
				return clubFlightListViewModel;
			}
		}

		public class GetFlightsWithoutInvoiceQueryValidator : AbstractValidator<GetFlightsWithoutInvoiceQuery>
		{

		}

	}
}
