namespace Hahn.WebApi.Extensions;

public static class ApiConfigurationExtension
{
    public static void AddCorsConfiguration(this WebApplicationBuilder builder)
    {
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
    }
}