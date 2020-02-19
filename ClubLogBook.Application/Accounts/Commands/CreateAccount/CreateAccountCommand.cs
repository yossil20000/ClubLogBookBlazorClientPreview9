using ClubLogBook.Application.Common.Interfaces;
using ClubLogBook.Core.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ClubLogBook.Application.Accounts.Commands.CreateAccount
{
	public class CreateAccountCommand : IRequest<Account>
	{
		public int MemeberId { get; set; } = 0;
		public string MemberInfo { get; set; } = "";
		public string Description { get; set; } = "";
		public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand,Account>
		{
			private readonly IApplicationDbContext _context;
			
			public CreateAccountCommandHandler(IApplicationDbContext context)
			{
				_context = context;
			}

			public async Task<Account> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
			{
				
				var entity = _context.Set<Account>().Where(val => val.MemeberId == request.MemeberId).FirstOrDefault();
				if (entity != null)
					return entity;
				 entity = new Account
				{
					MemeberId = request.MemeberId,
					MemberInfo = request.MemberInfo


				};

				_context.Set<Account>().Add(entity);

				await _context.SaveChangesAsync(cancellationToken);

				return entity;
			}
		}
	}
}
