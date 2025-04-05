using Scalar.AspNetCore;
using Hahn.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.MapDbContext();

builder.AddCryptoServices();

builder.AddCorsConfiguration();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference("/");

app.UseHttpsRedirection();
app.MapCryptoEndpoints();

app.UseCors("AllowAll");


app.Run();