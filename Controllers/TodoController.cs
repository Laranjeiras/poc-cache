using Microsoft.AspNetCore.Mvc;
using poc_cache.Entidades;
using poc_cache.Repositorios;
using StackExchange.Redis;

namespace poc_cache.Controllers
{
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepositorio repositorio;
        private readonly ILogger<TodoController> logger;

        public TodoController(ITodoRepositorio repositorio, ILogger<TodoController> logger)
        {
            this.repositorio = repositorio;
            this.logger = logger;
        }

        [HttpPost("/api/todo")]
        public IActionResult Salvar(TodoDto dto)
        {
            try
            {
                var todo = new Todo(dto.Id, dto.Task);
                repositorio.Salvar(todo);
                return Ok();
            }
            catch (RedisConnectionException ex)
            {
                var msg = "Erro na conexão com Redis";
                logger.LogError(msg, ex);
                return StatusCode(500, msg);
            }
        }

        [HttpGet("/api/todos")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var todos = await repositorio.ObterTodos();
                return Ok(todos);
            } 
            catch(RedisConnectionException ex)
            {
                logger.LogError(ex.Message, ex);
                return StatusCode(500, "Erro na conexão com Redis");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                return StatusCode(500, "Ocorreu um erro ao consultar o Redis");
            }
        }
    }
}
