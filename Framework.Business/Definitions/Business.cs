using System;
using System.Linq;
using Framework.Interfaces.Business;
using Framework.Interfaces.Repositories;
using Framework.Repositories;

namespace Framework.Business.Definitions
{
	public abstract class Business<TEntity, TRepository> : IBusiness<TEntity>
		where TEntity : class, new()
		where TRepository : IRepository<TEntity>
	{
		public Lazy<TRepository> Repository { get; set; }

		public Business()
        {
			this.Repository = new Lazy<TRepository>(() => { return RepositoryFactory.Resolve<TRepository>(); });
        }

		public virtual void Create(TEntity entity)
		{
			this.Repository.Value.Create(entity);
		}

		public virtual void Update(TEntity entity)
		{
			this.Repository.Value.Update(entity);
		}

		public virtual void Delete(TEntity entity)
		{
			this.Repository.Value.Delete(entity);
		}

		public virtual TEntity Get(int id)
		{
			return this.Repository.Value.Get(id);
		}

		public virtual TEntity GetBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
		{
			return this.Repository.Value.GetBy(predicate);
		}

		public virtual IQueryable<TEntity> All()
		{
			return this.Repository.Value.All();
		}

		public virtual IQueryable<TEntity> AllBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
		{
			return this.Repository.Value.AllBy(predicate);
		}

		public virtual int Count()
		{
			return this.Repository.Value.Count();
		}

		public virtual int CountBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
		{
			return this.Repository.Value.CountBy(predicate);
		}
	}
}
