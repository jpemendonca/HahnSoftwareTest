using Scalar.AspNetCore;
using Hahn.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.MapDbContext();

builder.AddCryptoServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference("/");

app.UseHttpsRedirection();
app.MapCryptoEndpoints();

app.UseCors("AllowAll");


app.Run();