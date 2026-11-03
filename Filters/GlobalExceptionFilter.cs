namespace TestMVCSharpC.Filters;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Json;
using Exceptions;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;
    private readonly ITempDataDictionaryFactory _tempDataFactory;

    public GlobalExceptionFilter(
        ILogger<GlobalExceptionFilter> logger,
        ITempDataDictionaryFactory tempDataFactory)
    {
        _logger = logger;
        _tempDataFactory = tempDataFactory;
    }

    public void OnException(ExceptionContext context)
    {
        string message;
        int statusCode;

        switch (context.Exception)
        {
            case ResourceNotFoundException notFoundEx:
                _logger.LogWarning(notFoundEx, "Resource not found: {Message}", notFoundEx.Message);
                message = notFoundEx.Message;
                statusCode = StatusCodes.Status404NotFound;
                break;

            case ArgumentException argEx:
                _logger.LogError(argEx, "Bad argument: {Message}", argEx.Message);
                message = argEx.Message;
                statusCode = StatusCodes.Status400BadRequest;
                break;

            case InternalServerErrorException internalEx:
                _logger.LogError(internalEx, "Internal server error: {Message}", internalEx.Message);
                message = internalEx.Message + " : " + GetInnermostMessage(internalEx);
                statusCode = StatusCodes.Status500InternalServerError;
                break;

            default:
                _logger.LogError(context.Exception, "Unexpected error: {Message}", context.Exception.Message);
                message = "An unexpected error occurred.";
                statusCode = StatusCodes.Status500InternalServerError;
                break;
        }

        if (statusCode == StatusCodes.Status500InternalServerError)
        {
            var tempData = _tempDataFactory.GetTempData(context.HttpContext);
            tempData["PopupError"] = JsonSerializer.Serialize(message);

            var referer = context.HttpContext.Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer))
            {
                context.Result = new RedirectResult(referer);
                context.ExceptionHandled = true;
                return;
            }
        }

        context.HttpContext.Response.StatusCode = statusCode;
        context.HttpContext.Items["ErrorMessage"] = message;
        context.ExceptionHandled = true;
    }

    public static string GetInnermostMessage(Exception ex)
    {
        if (ex == null) return string.Empty;
        while (ex.InnerException != null) ex = ex.InnerException;
        return ex.Message;
    }
}
