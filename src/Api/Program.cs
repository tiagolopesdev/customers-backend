
using Api.Controllers;
using Customer.Infrastructure.Configuration;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();


CustomerStartup.LoadCustomerModule(builder.Services);

builder.Services.AddControllers()
    .ConfigureApplicationPartManager(manager =>
    {
        manager.ApplicationParts.Add(
            new AssemblyPart(typeof(CustomerController).Assembly)
            );
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(option =>
{
    option.RoutePrefix = string.Empty;
    option.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});

//app.UseHttpsRedirection();

app.UseRouting();

//app.UseAuthorization();

app.MapControllers();

app.Run();
