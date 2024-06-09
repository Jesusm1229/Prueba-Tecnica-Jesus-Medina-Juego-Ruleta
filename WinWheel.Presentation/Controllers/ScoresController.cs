using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinWheel.Presentation.Controllers
{
	[Route("api/players/{playerId}/scores")]
	[ApiController]
	public class ScoresController : ControllerBase
	{
		private readonly IServiceManager _service;
		public ScoresController(IServiceManager service) => _service = service;


		//Get player scores.
		//Like the playerId is in the route, is not needed to be passed in the HttpGet attribute.
		[HttpGet]
		public IActionResult GetScoresForPlayer(Guid playerId)
		{
			var scores = _service.ScoreService.GetScores(playerId, trackChanges: false);

			return Ok(scores);
		}

		//Get a player score.
		[HttpGet("{id:guid}")]
		public IActionResult GetScoreForPlayer(Guid playerId, Guid id)
		{
			var score = _service.ScoreService.GetScore(playerId, id, trackChanges: false);

			return Ok(score);
		}
	}	

}
