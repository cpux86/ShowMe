using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Application;
using Persistence;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using WebApi.Middleware;
using System.Text.Json.Serialization;
using System.Text.Json;
using MinimalApi.Endpoint.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddApplication();
builder.Services.AddPersistence();
builder.Services.AddControllers();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var problemDetails = new
        {
            status = "error",
            code = StatusCodes.Status400BadRequest,
            errors = new ValidationProblemDetails(context.ModelState).Errors
        };
        return new BadRequestObjectResult(problemDetails)
        {
            ContentTypes = { "application/problem+json", "application/problem+xml" }
        };
    };
});
builder.Services.AddCors();



builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCustomExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
