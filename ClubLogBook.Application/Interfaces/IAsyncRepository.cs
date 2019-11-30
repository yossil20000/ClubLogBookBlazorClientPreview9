using System;
using System.Collections.Generic;
using System.Text;
using ClubLogBook.Core.Entities;
using System.Threading.Tasks;
using ClubLogBook.Core.Common;
namespace ClubLogBook.Application.Interfaces
{
	public interface IAsyncRepository<T> where T : class
	{
		Task<bool> IdExist(int id);
		Task<T> GetByIdAsync(int id);
		Task<IReadOnlyList<T>> ListAllAsync();
		Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
		Task<T> AddAsync(T entity);
		Task UpdateAsync(T entity);
		Task DeleteAsync(T entity);
		Task<int> CountAsync(ISpecification<T> spec);
	}
}
