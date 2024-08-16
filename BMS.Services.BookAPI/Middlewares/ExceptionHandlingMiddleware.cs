using System.Net;
using BMS.Services.BookAPI.Controllers;
using BMS.Services.BookAPI.Models;

namespace BMS.Services.BookAPI.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundEntityException ex)
        {
            await HandleExceptionAsync(context, Enums.StatusCodes.NotFound, ex.Message);
        }
        catch (DuplicateEntityException ex)
        {
            await HandleExceptionAsync(context, Enums.StatusCodes.BadRequest, ex.Message);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, Enums.StatusCodes.InternalServerError, ex.Message);
        }
    }
    private Task HandleExceptionAsync(HttpContext context, Enums.StatusCodes statusCode, string message)
    {
        var response = new ResponseDto
        {
            StatusCode = statusCode,
            Message = message,
            IsSuccess = false
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
    }
}