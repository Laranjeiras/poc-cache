using poc_cache.Data;
using poc_cache.Entidades;

namespace poc_cache.Repositorios
{
    public class TodoRepositorio : ITodoRepositorio
    {
        private readonly ICacheRepositorio cache;
        private readonly IOrmContext context;

        public TodoRepositorio(ICacheRepositorio cache, IOrmContext context)
        {
            this.cache = cache;
            this.context = context;
        }

        public void Salvar(Todo todo)
        {
            var existe = context.Todos.FirstOrDefault(x => x.Id == todo.Id);
            if (existe is not null)
            {
                existe.Editar(todo.Task);
            }
            else
            {
                context.Todos.Add(todo);
            }

            var json = System.Text.Json.JsonSerializer.Serialize(context.Todos);
            cache.Save("todos", json);
        }

        public async Task<IList<Todo>> ObterTodos()
        {
            var todosFromCache = await cache.ObterTodos<Todo>("todos");

            if (todosFromCache is not null)
                return todosFromCache;

            var todos = context.Todos.ToList();

            if (todos is null)
                throw new Exception("Todo não encontrado");

            await cache.Save("todos", todos);

            foreach (var todo in todos)
                await cache.Save<Todo>(todo.Id.ToString(), todo);

            return todos;
        }
    }
}
