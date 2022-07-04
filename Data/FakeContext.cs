using poc_cache.Entidades;

namespace poc_cache.Data
{
    public class FakeContext : IOrmContext
    {
        public IList<Todo> Todos { get; set; } = new List<Todo>()
            {
                new Todo(new Guid("5af08e54-c2ac-4b12-a823-a8eaaa3d14a4"), "ABRIR DOCUMENTO"),
                new Todo(new Guid("36c559f8-8d04-4b32-865c-decd7e779432"), "EDITAR DOCUMENTO"),
                new Todo(new Guid("43fd4ef8-dab7-493a-8141-224ac393d49a"), "SALVAR DOCUMENTO"),
                new Todo(new Guid("976608d1-75e5-4475-a4a7-f709d29df8c6"), "IMPRIMIR DOCUMENTO")
            };
    }
}
