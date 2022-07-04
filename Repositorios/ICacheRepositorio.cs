
namespace poc_cache.Repositorios
{
    public interface ICacheRepositorio
    {
        Task Save<T>(string key, T value);
        Task Save(string key, string value);
        Task<IList<T>?> ObterTodos<T>(string key);
    }
}