using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WinWheel.Presentation.Controllers
{
	[Route("api/players")]
	[ApiController]
	[ResponseCache(CacheProfileName = "120SecondsDuration")]
	[ApiExplorerSettings(GroupName = "v1")]

	public class PlayersController: ControllerBase
	{
		//Injecting the service manager
		private readonly IServiceManager _serviceManager;

		public PlayersController(IServiceManager serviceManager)
		{
			_serviceManager = serviceManager;
		}

		/// <summary>
		/// Gets the list of all players
		/// </summary>
		/// <returns>The players list</returns>
		[HttpGet(Name = "GetPlayers")]		
		public async Task<IActionResult> GetPlayers()
		{
			
				var players = await _serviceManager.PlayerService.GetAllPlayers(trackChanges: false);

				return Ok(players);
			
		}

		//Getting a player by id
		[HttpGet("{id:guid}", Name = "PlayerById")]
		[ResponseCache(Duration = 60)] // Caché control for 60 seconds. This is a good practice to avoid overloading the server.
		public async Task<IActionResult> GetPlayer(Guid id)
		{
			
				var player = await _serviceManager.PlayerService.GetPlayer(id, trackChanges: false);

				return Ok(player);
			
		}

		//Create a player
		//We take fromboy the player object. On the contrary will create 

		/// <summary>
		/// Creates a new player
		/// </summary>
		/// <param name="player"></param>
		/// <returns>A newly created player</returns>
		/// <response code="201">Returns the newly created item</response>
		/// <response code="400">If the item is null</response>
		/// <response code="422">If the model is invalid</response>
		[HttpPost (Name = "CreatePlayer")]
		[ProducesResponseType(201)]
		[ProducesResponseType(400)]
		[ProducesResponseType(422)]

		public async Task<IActionResult> CreatePlayer([FromBody] PlayerForCreationDto player)
		{
			if(player == null)
				return BadRequest("PlayerForCreationDto object is null");

			if(!ModelState.IsValid)
				return UnprocessableEntity(ModelState);

			var createdPlayer = await _serviceManager.PlayerService.CreatePlayer(player);

			//Created at route will return the created player and the route to get it.
			//201 status code means is created.
			//The headeer will have the location of the created player
				return CreatedAtRoute("PlayerById", new { id = createdPlayer.Id }, createdPlayer);
			
		}


	}
}
