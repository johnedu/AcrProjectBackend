using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace Bow.EntityFramework.Repositories
{
    public abstract class BowRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<BowDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected BowRepositoryBase(IDbContextProvider<BowDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }
    }

    public abstract class BowRepositoryBase<TEntity> : BowRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected BowRepositoryBase(IDbContextProvider<BowDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }
    }
}
