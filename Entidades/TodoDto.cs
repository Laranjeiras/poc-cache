namespace poc_cache.Entidades
{
    public class TodoDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Task { get; set; }
    }
}
