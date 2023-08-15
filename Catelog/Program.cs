using Catelog.Repositories;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);
SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
var connectionString= "Server=tcp:localhost.database.windows.net,1433;Initial Catalog=master;Persist Security Info=False;User ID=SA;Password= Password123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

// Add services to the container.
builder.Services.AddSingleton<IInMemItemRepository,InMemItemRepository>();
connectionStringBuilder.ConnectionString=connectionString;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
