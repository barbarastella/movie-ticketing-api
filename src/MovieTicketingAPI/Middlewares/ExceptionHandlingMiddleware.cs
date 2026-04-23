using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace MovieTicketingAPI.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);

            if (context.Response.StatusCode >= 400 && !context.Response.HasStarted)
                await WriteProblemDetailsAsync(context, context.Response.StatusCode);

        } catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu uma exceção durante o processamento.");

            var statusCode = ex switch
            {
                InvalidOperationException => (int)HttpStatusCode.Conflict,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                ArgumentException => (int)HttpStatusCode.BadRequest,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };

            await WriteProblemDetailsAsync(context, statusCode, ex.Message);
        }
    }

    private static Task WriteProblemDetailsAsync(HttpContext context, int statusCode, string? customDetail = null)
    {
        context.Response.ContentType = "application/problem+json";

        var detail = customDetail ?? GetDefaultMessageForStatus(statusCode);

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = GetTitle(statusCode),
            Detail = detail,
            Type = $"https://httpstatuses.io/{statusCode}",
            Instance = context.Request.Path
        };

        var jsonResponse = JsonSerializer.Serialize(problemDetails, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        return context.Response.WriteAsync(jsonResponse);
    }

    private static string GetTitle(int statusCode)
    {
        return statusCode switch
        {
            (int)HttpStatusCode.BadRequest => "Bad Request",
            (int)HttpStatusCode.Unauthorized => "Unauthorized",
            (int)HttpStatusCode.Forbidden => "Forbidden",
            (int)HttpStatusCode.NotFound => "Not Found",
            (int)HttpStatusCode.MethodNotAllowed => "Method Not Allowed",
            (int)HttpStatusCode.Conflict => "Conflict",
            (int)HttpStatusCode.InternalServerError => "Internal Server Error",
            _ => "Error"
        };
    }
    private static string GetDefaultMessageForStatus(int statusCode) => statusCode switch
    {
        401 => "Usuário não autenticado, forneça um token válido.",
        403 => "Usuário não possui permissão para acessar este recurso.",
        404 => "O recurso solicitado não foi encontrado.",
        405 => "O método HTTP utilizado não é permitido.",
        _ => "Ocorreu um erro ao processar a requisição."
    };
}
