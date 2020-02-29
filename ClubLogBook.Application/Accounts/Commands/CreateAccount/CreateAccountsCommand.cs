using ClubLogBook.Application.Common.Interfaces;
using ClubLogBook.Core.Entities;
using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ClubLogBook.Application.Accounts.Commands.CreateAccount
{
	public class CreateAccountsCommand : IRequest<int>
	{
		
		public class CreateAccountCommandHandler : IRequestHandler<CreateAccountsCommand, int>
		{
			private readonly IApplicationDbContext _context;

			public CreateAccountCommandHandler(IApplicationDbContext context)
			{
				_context = context;
			}

			public async Task<int> Handle(CreateAccountsCommand request, CancellationToken cancellationToken)
			{

				//var entityPilots = _context.Set<Pilot>();
				//if (entity != null)
				//	return entity;
				//entity = new Account
				//{
				//	MemeberId = request.MemeberId,
				//	MemberInfo = request.MemberInfo


				//};

				//_context.Set<Account>().Add(entity);

				//await _context.SaveChangesAsync(cancellationToken);

				return 1;
			}
		}
	}

	
}
