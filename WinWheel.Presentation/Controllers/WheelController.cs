using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWheel.Presentation.ActionFilters;

namespace WinWheel.Presentation.Controllers
{
	[Route("api/wheels")]
	[ApiController]
	public class WheelController: ControllerBase
	{
		private readonly IServiceManager _service;

		public WheelController(IServiceManager service) => _service = service;

		
		//SpinWheel
		[HttpGet(Name = "SpinWheel")]
		[ServiceFilter(typeof(ValidationFilterAttribute))]		
		public async Task<IActionResult> SpinWheel()
		{				
				
			var positionResult = await _service.WheelService.SpinWheel();

			return Ok(positionResult);
		}
	}
}
