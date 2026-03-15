using ToDoAPI.Repositories.Interfaces;
using ToDoAPI.Repositories.Repos;
using ToDoAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Area de servicios

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITaskRepository, TaskRepositoryJson>();
builder.Services.AddScoped<ITaskService, TaskService>();

var app = builder.Build();

// Area de Middlewares

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
