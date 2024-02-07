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

        public async Task PatientTransactionListSave(List<patient_payments> transaction, long? createdBy, string transactionKey, decimal capturedAmount, DateTime chargeCreatedOn, bool isChargeCaptured, string payerType)
        {
            var payments = new List<patient_payments>();
            foreach (var item in transaction)
            {
                if (Convert.ToInt64(item.app_id) > 0)
                {
                    patient_payments? patientTransaction;
                    if (payerType == "Payer")
                    {
                        patientTransaction = await this.context.patient_Payments.FindAsync(item.pid);
                    }
                    else
                    {
                        patientTransaction = this.context.patient_Payments.FirstOrDefault(x => x.app_id == item.app_id && x.ppId == item.ppId);
                    }
                    if (patientTransaction != null)
                    {
                        if (item.paymentType == "GetDescription")
                        {
                            patientTransaction.amount = item.amount;
                            this.context.Entry(patientTransaction).State = EntityState.Modified;
                            this.context.SaveChanges();
                        }
                        if (item.paymentType == "CoInsurance")
                        {
                            this.context.Entry(patientTransaction).State = EntityState.Modified;
                            this.context.SaveChanges();
                        }
                    }
                }
                if (item.paymentMethod == "Cash")
                {
                    var staff = this.context.patient_Payments.FirstOrDefault();
                    if (staff != null)
                    {
                        item.paymentDescription = staff.card_Key + " " + staff.card_Key;
                        if (!string.IsNullOrEmpty(staff.card_Key))
                        {
                            item.paymentDescription += ", " + staff.card_Key;
                        }
                    }
                }
                payments.Add(new patient_payments()
                {
                    pid = item.pid,
                    ppId = item.ppId,
                    app_id = item.app_id,
                    card_id = item.card_id,
                    card_Key = item.card_Key,
                    transaction_Key = transactionKey,
                    paymentType = item.paymentType,
                    amount = item.amount,
                    paymentDateTime = DateTime.UtcNow,
                    is_active = true,
                    is_deleted = false,
                    createdBy = createdBy,
                    is_Otherpayment = item.is_Otherpayment,
                    paymentMethod = item.paymentMethod,
                    paymentDescription = item.paymentDescription,
                    StripeAmountCaptured = capturedAmount,
                    StripeChargeCreatedOn = chargeCreatedOn,
                    IsStripeChargeCaptured = isChargeCaptured
                });
            }
            await this.context.patient_Payments.AddRangeAsync(payments);
            await this.context.SaveChangesAsync();            
        }

    }
}
