using poc_cache.Entidades;

namespace poc_cache.Repositorios
{
    public interface ITodoRepositorio
    {
        void Salvar(Todo todo);
        Task<IList<Todo>> ObterTodos();
    }
}
