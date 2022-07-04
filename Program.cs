using poc_cache.Data;
using poc_cache.Entidades;
using poc_cache.Repositorios;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<ICacheRepositorio, CacheRepositorio>();
builder.Services.AddScoped<ITodoRepositorio, TodoRepositorio>();
builder.Services.AddSingleton<IOrmContext, FakeContext>();


var redisConnectionString = builder.Configuration.GetConnectionString("DockerRedisConnection");
if (string.IsNullOrEmpty(redisConnectionString))
    throw new ArgumentNullException("Redis Connection String");

builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = redisConnectionString;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICacheRepositorio, CacheRepositorio>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    try
    {
        var repositorio = scope.ServiceProvider.GetRequiredService<ITodoRepositorio>();

        for (int i = 0; i < 1000; i++)
        {
            var todo = new Todo(Guid.NewGuid(), $"TASK {i}");
            repositorio.Salvar(todo);
        }
    }
    catch (RedisConnectionException ex)
    {
        logger.LogError(ex.Message, ex);
        throw;
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
        throw;
    }
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
