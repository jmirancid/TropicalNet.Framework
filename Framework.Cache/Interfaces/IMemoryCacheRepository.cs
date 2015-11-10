using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Cache.Interfaces
{
    public interface IMemoryCacheRepository : ICacheRepository<Framework.Cache.Entities.CacheEntry>
    {
        void Trim(int percentage);
    }
}
