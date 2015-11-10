using Framework.Interfaces.Repositories;

namespace Framework.Cache.Interfaces
{
    public interface ICacheRepository<TEntry> : IRepository<TEntry>
        where TEntry : class, new()
    {
        TEntry Get(string id);
    }
}
