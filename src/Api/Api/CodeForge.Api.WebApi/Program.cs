using CodeForge.Infrastructure.Persistence;
using CodeForge.Api.Application;
using FluentValidation.AspNetCore;
using CodeForge.Api.WebApi.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

#pragma warning disable CS0618 // Type or member is obsolete

builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = null;
    })
    .AddFluentValidation();

#pragma warning restore CS0618 // Type or member is obsolete

builder.Services.ConfigureAuth(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ConfigureExceptionHandling(app.Environment.IsDevelopment());

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
