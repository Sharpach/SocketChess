using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTask.WebApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RandomNumberController : ControllerBase
	{
		// GET: api/RandomNumber
		[HttpGet]
		public int Get(string firstNumber, string secondNumber)
		{
			try
			{
				if (string.IsNullOrEmpty(firstNumber) || string.IsNullOrEmpty(secondNumber))
				{
					return RandomNumber.GetThreadRandom().Next(-10000, 50000);

				}
				else
				{
					return RandomNumber.GetThreadRandom().Next(int.Parse(firstNumber), int.Parse(secondNumber));
				}

			}
			catch
			{
				throw new Exception("Метод Get не сработал");
			}
		}
	}
}
