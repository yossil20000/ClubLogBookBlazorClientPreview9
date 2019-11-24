using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using ClubLogBook.Application.Accounts.Queries.GetAccountsList;
using ClubLogBook.Application.Interfaces;

namespace ClubLogBook.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IAppLogger<AccountController> logger;
        public AccountController(IMediator mediator, IAppLogger<AccountController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;

        }
        // GET: api/Account
        [HttpGet]
        public async Task<AccountListViewModel> AccountsListQueryGet()
        {
            CancellationToken ct = new CancellationToken();
            AccountListViewModel list = new AccountListViewModel();
            list.Accounts = new List<AccountLookupModel>();
            list.Accounts.Add(new AccountLookupModel() { Id = 1 });
            try
            {
                list  = await mediator.Send(new GetAccountsListQuery(), ct);
            }
            catch(Exception ex)
            {
                logger.LogWorning(ex.Message);
            }
            return list;
        }

       

        // POST: api/Account
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Account/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
