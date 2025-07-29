using Carple.Application.Dto;
using Carple.Application.Interfaces;
using System.Text.Json;
using System.Text;

public class ApiKeyValidationMiddleware
{
    private readonly RequestDelegate _next;

    public ApiKeyValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IAuthRepo userRepo)
    {
        if (context.Request.Path.StartsWithSegments("/api/auth/login") &&
            context.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
        {
            context.Request.EnableBuffering();

            using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;

            var loginDto = JsonSerializer.Deserialize<LoginDto>(body, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (loginDto is not null)
            {
                var user = await userRepo.GetUserByEmailAsync(loginDto.Email);

                if (user == null)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Unauthorized - User not found.");
                    return;
                }

                if (string.IsNullOrEmpty(user.Apikey))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Unauthorized - API Key missing.");
                    return;
                }
            }
        }

        await _next(context);
    }
}
