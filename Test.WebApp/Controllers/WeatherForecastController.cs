using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;
using Test.WebApp.Models;

namespace Test.WebApp.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class WeatherForecastController : ControllerBase
	{
		private DataBaseContext context;
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILogger<WeatherForecastController> _logger;

		public WeatherForecastController(ILogger<WeatherForecastController> logger, DataBaseContext baseContext)
		{
			_logger = logger;
			context = baseContext;
		}

		[HttpGet(Name = "GetWeatherForecast")]
		public IEnumerable<WeatherForecast> Get()
		{
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
				TemperatureC = Random.Shared.Next(-20, 55),
				Summary = Summaries[Random.Shared.Next(Summaries.Length)]
			})
			.ToArray();
		}

		[HttpGet]
		public async Task<IActionResult> SaveUser()
		{
			User user = new()
			{
				Name = "Abid Ali",
				Roles =
				[
					new(){Name = "Admin"},
					new(){Name = "Manager"},
					new(){Name = "CTO"},
				],
				Address = new ()
				{
					ParmanentAddress = "Gujranwala, Punjab, Pakistan.",
					CurrentAddress = "Lahore, Punjab, Pakistan."
				}
			};
			var data = await context.Users.Where(x => x.Name == "Abubakr").FirstOrDefaultAsync();
			var data1 = await context.Users.FirstOrDefaultAsync(x => x.Name == "Abubakr");


			await context.Users.AddAsync(user);
			await context.SaveChangesAsync();
			//string dataQuery = data.
			return Ok();
		}
	}
}
