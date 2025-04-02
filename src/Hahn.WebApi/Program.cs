using Scalar.AspNetCore;
using Hahn.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.MapDbContext();

builder.AddCryptoServices();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference("/");

app.UseHttpsRedirection();
app.MapCryptoEndpoints();

app.Run();