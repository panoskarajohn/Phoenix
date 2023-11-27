using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Community.Exception;

public class ErrorHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (PhoenixExceptions ex)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(ex.Message);
        }
        catch (ValidationException validationException)
        {
            context.Response.StatusCode = 400;
            var errorMessage = string.Join(", ", validationException.Errors.Select(x => x.ErrorMessage));
            await context.Response.WriteAsync(errorMessage);
        }
        catch (ArgumentException ex)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(ex.Message);
        }
        catch (System.Exception ex)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync(ex.Message);
        }
    }
}