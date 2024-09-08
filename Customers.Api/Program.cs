
using Customers.Application;
using Customers.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(option =>
{
    option.RoutePrefix = string.Empty;
    option.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
