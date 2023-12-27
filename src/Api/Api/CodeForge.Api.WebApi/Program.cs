using CodeForge.Infrastructure.Persistence;
using CodeForge.Api.Application;
using FluentValidation.AspNetCore;
using CodeForge.Api.WebApi.Infrastructure.Extensions;
using CodeForge.Api.WebApi.Infrastructure.ActionFilters;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddControllers(opt => opt.Filters.Add<ValidateModelStateFilter>())
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = null;
    })
    .AddFluentValidation()
        .ConfigureApiBehaviorOptions(o => o.SuppressModelStateInvalidFilter = true);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

// builder.Services.ConfigureAuth(builder.Configuration);

// Add Cors
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ConfigureExceptionHandling(app.Environment.IsDevelopment());

// app.UseAuthentication();
// app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();

app.UseCors("MyPolicy");

app.Run();
