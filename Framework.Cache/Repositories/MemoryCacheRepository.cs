using System.Runtime.Caching;

namespace Framework.Cache.Repositories
{
    public class MemoryCacheRepository : CacheRepository<System.Runtime.Caching.MemoryCache, Framework.Cache.Entities.CacheEntry>, Framework.Cache.Interfaces.IMemoryCacheRepository
    {
        protected override System.Runtime.Caching.MemoryCache Context
        {
            get
            {
                return MemoryCache.Default;
            }
        }

        public void Trim(int percent)
        {
            Context.Trim(percent);
        }
    }
}
