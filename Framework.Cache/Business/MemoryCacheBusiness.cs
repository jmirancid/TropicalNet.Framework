using System;
using Framework.Cache.Interfaces;

namespace Framework.Cache.Business
{
    public class MemoryCacheBusiness : CacheBusiness<Framework.Cache.Entities.CacheEntry, IMemoryCacheRepository>
    {
        public override Lazy<IMemoryCacheRepository> Repository
        {
            get
            {
                if (base._repository == null)
                    base._repository = new Lazy<IMemoryCacheRepository>(() => new Framework.Cache.Repositories.MemoryCacheRepository());

                return base._repository;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Trim(int percent)
        {
            Repository.Value.Trim(percent);
        }
    }
}
