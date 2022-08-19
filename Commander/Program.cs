using Commander.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Setup newtonsoftjson
builder.Services.AddControllers().AddNewtonsoftJson(s =>
{
    s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Whenever application asks for ICommandRepo(which is in the constructor), return the MockCommanderRepo
builder.Services.AddScoped<ICommanderRepo, SqlCommanderRepo>();
// Add the database to this program and the options is to use the server we will store the db in
builder.Services.AddDbContext<CommanderDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection"));
});
// AutoMapper needs assemblies to work
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



var app = builder.Build();

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