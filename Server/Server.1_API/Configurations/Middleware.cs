using System.Net;
using System.Text.Json;

namespace MarkGrader.Configurations;

public sealed class Middleware
{
	private readonly RequestDelegate _next;



	public Middleware(RequestDelegate next)
	{
		_next = next;
	}



	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception error)
		{
			var response = context.Response;
			response.ContentType = "application/json";


			response.StatusCode = error switch
			{
				KeyNotFoundException => (int)HttpStatusCode.NotFound,
				_ => (int)HttpStatusCode.InternalServerError,
			};

			object errorObject = new { message = "Error occurred in server", stackTrace = error.StackTrace };

			var result = JsonSerializer.Serialize(errorObject);
			await response.WriteAsync(result);
		}
	}
}
