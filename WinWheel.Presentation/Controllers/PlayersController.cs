using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WinWheel.Presentation.Controllers
{
	[Route("api/players")]
	[ApiController]
	public class PlayersController: ControllerBase
	{
		//Injecting the service manager
		private readonly IServiceManager _serviceManager;

		public PlayersController(IServiceManager serviceManager)
		{
			_serviceManager = serviceManager;
		}

		[HttpGet]
		public IActionResult GetPlayers()
		{
			
				var players = _serviceManager.PlayerService.GetAllPlayers(trackChanges: false);

				return Ok(players);
			
		}


	}
}
