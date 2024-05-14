using System.Net;
using System.Text.Json;

namespace MarkGrader.Configurations;

public class Middleware
{
	private readonly RequestDelegate _next;

	public Middleware(RequestDelegate next)
	{
		_next = next;
	}


	public async Task Invoke(HttpContext context)
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
			var result = JsonSerializer.Serialize(new { message = error?.Message });
			await response.WriteAsync(result);
		}
	}
}
