using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Interfaces.Repositories
{
	public interface IRepository<TEntity>
		where TEntity : class, new()
	{
		void Create(TEntity entity);
		void Update(TEntity entity);
		void Delete(TEntity entity);

		TEntity Get(int id);
		TEntity GetBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);

		IQueryable<TEntity> All();
		IQueryable<TEntity> AllBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);

		int Count();
		int CountBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
	}
}
