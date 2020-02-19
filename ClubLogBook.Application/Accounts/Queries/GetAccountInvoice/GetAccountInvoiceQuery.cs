using System.Collections.Generic;
using System;
using ClubLogBook.Core.Interfaces;
using ClubLogBook.Application.Interfaces.Mapping;
using AutoMapper;
using MediatR;
using ClubLogBook.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using ClubLogBook.Core.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using System.Linq;

namespace ClubLogBook.Application.Accounts.Queries.GetAccountInvoice
{
	public class AccountInvoiceModel : IHaveCustomMapping
	{
		public int Id { get; set; } = 0;
		public int MemeberId { get; set; } = 0;
		public Decimal Amount { get; set; } = 0;
		public string Description { get; set; } = "";
		public InvoiceType InvoiceType { get; set; } = InvoiceType.Flight;
		public InvoiceState InvoiceState { get; set; } = InvoiceState.Create;
		public int InvoiceReferance { get; set; } = 0;

		public void CreateMappings(Profile configuration)
		{
			configuration.CreateMap<Invoice, AccountInvoiceModel>();
			configuration.CreateMap<AccountInvoiceModel, Invoice>();
		}
	}
	public class AccountInvoiceViewModel 
	{
		public IList<AccountInvoiceModel> Invoices { get; set; }
	}
	public class GetAccountInvoiceQuery : IRequest<AccountInvoiceViewModel>
	{
		
		public class Handler : IRequestHandler<GetAccountInvoiceQuery, AccountInvoiceViewModel>
		{
			private readonly IApplicationDbContext context;
			private readonly IMapper mapper;
			public Handler(IApplicationDbContext context, IMapper mapper)
			{
				this.mapper = mapper;
				this.context = context;
			}
			public async Task<AccountInvoiceViewModel> Handle(GetAccountInvoiceQuery request, CancellationToken cancellationToken)
			{
				
				var memberInvoices = context.Set<Invoice>().Where(r => r.InvoiceState == InvoiceState.Create);
				return new AccountInvoiceViewModel
				{
					Invoices = await memberInvoices.ProjectTo<AccountInvoiceModel>(mapper.ConfigurationProvider).ToListAsync(cancellationToken)
				};
			}
		}
	}
}
