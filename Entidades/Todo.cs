namespace poc_cache.Entidades
{
    public class Todo
    {
        public Todo(Guid id, string? task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            Id = id;
            Task = task;
        }

        public Guid Id { get; private set; }
        public string Task { get; private set; }

        internal void Editar(string task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            Task = task;
        }
    }
}
