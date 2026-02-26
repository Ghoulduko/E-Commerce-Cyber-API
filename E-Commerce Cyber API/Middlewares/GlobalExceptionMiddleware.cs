namespace E_Commerce_Cyber_API.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _requestDelegate;

    public GlobalExceptionMiddleware(RequestDelegate requestDelegate)
    {
        _requestDelegate = requestDelegate;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _requestDelegate(context);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occured while requesting endpoint");
            var response = new
            {
                Message = $"Error message: {ex.Message}",
                StatusCode = 500,
                Details = "An error occurred while processing your request. Please try again later."
            };
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
