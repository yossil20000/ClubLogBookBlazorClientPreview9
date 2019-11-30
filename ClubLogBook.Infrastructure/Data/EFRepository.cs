using System;
using System.Collections.Generic;
using System.Linq;
using ClubLogBook.Application.Interfaces;
using ClubLogBook.Core.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClubLogBook.Core.Common;

namespace ClubLogBook.Infrastructure.Data
{
	/// <summary>
	/// "There's some repetition here - couldn't we have some the sync methods call the async?"
	/// https://blogs.msdn.microsoft.com/pfxteam/2012/04/13/should-i-expose-synchronous-wrappers-for-asynchronous-methods/
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class EFRepository<T> :IAsyncRepository<T> where T : AuditableEntity
	{
		protected ClubLogbookContext _dbContex;
		public EFRepository(ClubLogbookContext dbcontex)
		{
			_dbContex = dbcontex;
		}
		public  Task<bool> IdExist(int id)
		{
			try
			{
				var exist = _dbContex.Set<T>().Find(id);
				if(exist !=null)
					return  Task.FromResult<bool>(true);
			}
			catch
			{
				
			}
			return Task.FromResult<bool>(false);
		}
		public virtual async Task<T> GetByIdAsync(int id)
		{
			
			try
			{
				 return  await _dbContex.Set<T>().FindAsync(id);
			}
			catch(InvalidOperationException ioe)
			{
				
				return await Task.FromResult<T>(null); 
			}
			
			
		}
		public async Task<IReadOnlyList<T>> ListAllAsync()
		{
			return await _dbContex.Set<T>().ToListAsync();
		}
		public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
		{
			return await ApplySpecification(spec).ToListAsync();
		}
		public async Task<T> AddAsync(T entity)
		{
			try
			{
				_dbContex.Set<T>().UpdateRange(entity);

				 _dbContex.SaveChanges();
				
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine($"Exception {ex.Message}");
			}
			finally
			{
				System.Diagnostics.Debug.WriteLine("Finally");
			}
			return entity;
		}
		public async Task UpdateAsync(T entity)
		{
			//_dbContex.Entry(entity).State = EntityState.Modified;
			_dbContex.Set<T>().UpdateRange(entity);
			await _dbContex.SaveChangesAsync();
		}
		public async Task DeleteAsync(T entity)
		{
			_dbContex.Set<T>().Remove(entity);
			await _dbContex.SaveChangesAsync();
		}
		public async Task<int> CountAsync(ISpecification<T> spec)
		{
			return await ApplySpecification(spec).CountAsync();
		}
		private IQueryable<T> ApplySpecification(ISpecification<T> spec)
		{
			return  SpecificationEvaluator<T>.GetQuery(_dbContex.Set<T>().AsQueryable(),spec);
		}
	}
}
