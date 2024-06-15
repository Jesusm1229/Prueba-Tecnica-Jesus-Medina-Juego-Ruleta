using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWheel.Presentation.ActionFilters;

namespace WinWheel.Presentation.Controllers
{
	[Route("api/bets")]
	[ApiController]
	public class BetController: ControllerBase
	{
		private readonly IServiceManager _service;

		public BetController(IServiceManager service) => _service = service;

		
		/// <summary>
		/// Place a bet. Categories: * "Straight": specific number (0-36) and color (Black - Red). * "Even": even numbers. * "Odd": odd numbers. * Color: "Black" or "Red"
		/// </summary>
		/// <param name="betForCalculationDto"></param>
		/// <returns>A result Including: Winner Number, Winner Color, a boolean indicating if he won or lose and the  New Score for player </returns>
		[HttpPost(Name = "PlaceBet")]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		[ServiceFilter(typeof(ValidationBetAttribute))]
		public async Task<IActionResult> PlaceBet([FromBody] BetForCalculationDto betForCalculationDto)
		{				
				
			var bet = await _service.BetService.PlaceBet(betForCalculationDto);

			return Ok(bet);
		}

	


	}
}
