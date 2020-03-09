using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using ClubLogBook.Core.Entities;
using Microsoft.EntityFrameworkCore;
using ClubLogBook.Application.Common.Interfaces;
using System.Collections.Generic;

namespace ClubLogBook.Application.Accounts.Queries.GetAccountsList
{
	public class GetAccountsListQuery : IRequest<AccountListModel>
	{

		public class Handler : IRequestHandler<GetAccountsListQuery, AccountListModel>
		{
			private readonly IApplicationDbContext context;
			private readonly IMapper mapper;
			public Handler(IApplicationDbContext context,IMapper mapper)
			{
				this.mapper = mapper;
				this.context = context;
			}
			public async Task<AccountListModel> Handle(GetAccountsListQuery request, CancellationToken cancellationToken)
			{
				return new AccountListModel 
				{
					Accounts = await context.Set<Account>().ProjectTo<AccountLookupModel>(mapper.ConfigurationProvider).ToListAsync(cancellationToken) 
				};
			}
		}
	}
	
}
