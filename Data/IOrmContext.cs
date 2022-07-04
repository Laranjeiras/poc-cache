using poc_cache.Entidades;

namespace poc_cache.Data
{
    public interface IOrmContext
    {
        IList<Todo> Todos { get; }
    }
}
