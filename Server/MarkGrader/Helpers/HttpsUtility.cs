using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Repositories.DTOs;

namespace MarkGrader.Helpers;


internal sealed class HttpsUtility
{


	internal static IActionResult ReturnActionResult<T>(CustomResponse<T> customResponse)
	{
		// Handle success codes
		if (customResponse.IsSuccess)
			return GetSuccessActionResult(customResponse);
		// Handle fail codes
		else
			return GetFailActionResult(customResponse);
	}




	private static IActionResult GetSuccessActionResult<T>(CustomResponse<T> customResponse)
	{
		IActionResult? actionResult = null;

		switch (customResponse.StatusCode)
		{
			case StatusCodes.Status200OK: actionResult = new OkObjectResult(customResponse); break;
			case StatusCodes.Status201Created: actionResult = new CreatedResult(string.Empty, customResponse); break;
			case StatusCodes.Status204NoContent:
				actionResult = new ContentResult()
				{
					ContentType = "application/json",
					Content = customResponse.Message,
					StatusCode = StatusCodes.Status204NoContent,
				};
				break;
		}
		return actionResult!;
	}







	private static IActionResult GetFailActionResult<T>(CustomResponse<T> customResponse)
	{
		IActionResult? actionResult = null;

		switch (customResponse.StatusCode)
		{
			case StatusCodes.Status400BadRequest:
				actionResult = new BadRequestObjectResult(customResponse);
				break;


			case StatusCodes.Status401Unauthorized:
				actionResult = new UnauthorizedObjectResult(customResponse);
				break;


			case StatusCodes.Status404NotFound:
				actionResult = new NotFoundObjectResult(customResponse);
				break;


			case StatusCodes.Status403Forbidden:
			case StatusCodes.Status501NotImplemented:
			case StatusCodes.Status500InternalServerError:
				actionResult = new ObjectResult(customResponse)
				{
					StatusCode = customResponse.StatusCode,
					ContentTypes = new MediaTypeCollection() { "application/json" }
				};
				break;
		}
		return actionResult!;
	}
}

