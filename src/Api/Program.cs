
using Api.Controllers;
using CloudNative.ServiceDefaults;
using Customer.Infrastructure.Configuration;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Product.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();


CustomerStartup.LoadCustomerModule(builder.Services);
ProductStartup.LoadProductModule(builder.Services);

builder.Services.AddControllers();

builder.AddServiceDefaults();

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
