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

		
		//Place Bet
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
