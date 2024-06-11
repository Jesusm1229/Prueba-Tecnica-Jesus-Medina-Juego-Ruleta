using Microsoft.AspNetCore.JsonPatch;
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
		[HttpGet("{id:guid}", Name = "GetScoreForPlayer")]
		public IActionResult GetScoreForPlayer(Guid playerId, Guid id)
		{
			var score = _service.ScoreService.GetScore(playerId, id, trackChanges: false);

			return Ok(score);
		}

		//Create a score for a player.
		[HttpPost]
		public IActionResult CreateScoreForPlayer(Guid playerId, [FromBody] ScoreForCreationDto score)
		{
			if(score == null)
				return BadRequest("ScoreForCreationDto object is null");

			var scoreToReturn = _service.ScoreService.CreateScoreForPlayer(playerId, score, trackChanges: false);

			return CreatedAtRoute("GetScoreForPlayer", new { playerId, id = scoreToReturn.Id }, scoreToReturn);
		}

		//Update a score for a player.
		[HttpPut("{id:guid}")] //api/players/{playerId}/scores/{id}
		public IActionResult UpdateScoreForPlayer(Guid playerId, Guid id, [FromBody] ScoreForUpdateDto score)
		{
			if(score == null)
				return BadRequest("ScoreForUpdateDto object is null");

			_service.ScoreService.UpdateScoreForPlayer(playerId, id, score, playerTrackChanges: false, scoreTrackChanges: true);

			return NoContent();
		}

		//Partially update a score for a player.
		[HttpPatch("{id:guid}")] //api/players/{playerId}/scores/{id}
		public IActionResult PartiallyUpdateScoreForPlayer(Guid playerId, Guid id, [FromBody] JsonPatchDocument<ScoreForUpdateDto> patchDoc)
		{
			if(patchDoc == null)
				return BadRequest("patchDoc object is null");

			var result = _service.ScoreService.GetScoreForPlayerPatch(playerId, id, playerTrackChanges: false, scoreTrackChanges: true);

			patchDoc.ApplyTo(result.scoreToPatch);

			_service.ScoreService.SaveChangesForPatch(result.scoreToPatch, result.scoreEntity);

			return NoContent();
		}
	}	

}
