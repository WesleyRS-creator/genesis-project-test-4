namespace TestMVCSharpC.Controllers;

using Microsoft.AspNetCore.Mvc;

public class ErrorController : Controller
{
    [Route("Error/{statusCode}")]
    public IActionResult HttpStatusCodeHandler(int statusCode)
    {
        string message = statusCode switch
        {
            404 => "Resource not found.",
            400 => "Bad argument.",
            500 => "An internal error occured.",
            _ => "An unexpected error occurred."
        };

        ViewData["ErrorMessage"] = message;
        return View("Error");
    }
}
