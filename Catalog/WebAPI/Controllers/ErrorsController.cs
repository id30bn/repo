using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[ApiController]
	[ApiExplorerSettings(IgnoreApi = true)]
	public class ErrorsController : ControllerBase
	{
		private readonly ILoggerFactory _loggerFactory;
		private readonly IWebHostEnvironment _environment;

		public ErrorsController(ILoggerFactory loggerFactory, IWebHostEnvironment environment)
		{
			_loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
			_environment = environment ?? throw new ArgumentNullException(nameof(environment));
		}

		[Route("/errors")]
		public IActionResult Error()
		{
			var exception = HttpContext
				.Features
				.Get<IExceptionHandlerFeature>()
				?.Error;

			return exception switch {
				EntityNotFoundException entityNotFoundException => HandleEntityNotFoundException(entityNotFoundException),
				_ => HandleUnknownException(exception),
			};
		}

		private IActionResult HandleEntityNotFoundException(EntityNotFoundException exception)
		{
			var problemDetails = new ProblemDetails() {
				Detail = exception.Message,
				Instance = string.Empty,
				Status = StatusCodes.Status404NotFound,
				Title = "A resource was not found",
				Type = $"https://httpstatuses.com/{StatusCodes.Status404NotFound}",
			};
			problemDetails.Extensions.Add("traceId", HttpContext.TraceIdentifier);
			HttpContext.Response.StatusCode = 404;
			return new ObjectResult(problemDetails);
		}

		private IActionResult HandleUnknownException(Exception? exception)
		{
			if (exception == null) {
				return NoContent();
			}

			_loggerFactory
				.CreateLogger("Global Exception Logger")
				.LogError(500, exception, exception.Message);

			var problemDetails = new ProblemDetails() {
				Instance = string.Empty,
				Status = StatusCodes.Status500InternalServerError,
				Type = $"https://httpstatuses.com/{StatusCodes.Status500InternalServerError}"
			};

			if (_environment.IsDevelopment()) {
				problemDetails.Detail = exception.StackTrace;
				problemDetails.Title = exception.Message;
			} else {
				problemDetails.Detail = string.Empty;
				problemDetails.Title = "An unpected server fault occurred";
			}

			problemDetails.Extensions.Add("traceId", HttpContext.TraceIdentifier);

			HttpContext.Response.StatusCode = 500;
			return new ObjectResult(problemDetails);
		}
	}
}
