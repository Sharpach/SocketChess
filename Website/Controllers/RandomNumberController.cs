using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi;

namespace Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomNumberController : ControllerBase
    {
	    // GET: api/RandomNumber
	    [HttpGet]
	    public int Get()
	    {
		    return RandomNumber.GetThreadRandom().Next(-10000, 50000);

		}
	 //   [HttpGet]
		
		


    }
}
