using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
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
	[Route("api/players/{playerId}/scores")]
	[ApiController]
	public class ScoresController : ControllerBase
	{
		private readonly IServiceManager _service;
		public ScoresController(IServiceManager service) => _service = service;

		/// <summary>
		/// Gets the list of all scores for a player. The requerirements stablish a one to one relationship between player and score. So it will always return one score.
		/// </summary>
		/// <returns>The companies list</returns>
		//Get player scores.
		//Like the playerId is in the route, is not needed to be passed in the HttpGet attribute.
		[HttpGet]
		public async Task<IActionResult> GetScoresForPlayer(Guid playerId)
		{
			var scores = await _service.ScoreService.GetScores(playerId, trackChanges: false);

			return Ok(scores);
		}

		//Get a player score.
		/// <summary>
		/// Gets the speficic score for a player
		/// </summary>
		/// <returns>The companies list</returns>
		[HttpGet("{id:guid}", Name = "GetScoreForPlayer")]
		[Authorize]
		public async Task<IActionResult> GetScoreForPlayer(Guid playerId, Guid id)
		{
			var score = await _service.ScoreService.GetScore(playerId, id, trackChanges: false);

			return Ok(score);
		}

		//Create a score for a player.
		/// <summary>
		/// Creates a new score for player
		/// </summary>
		/// <param name="playerId"></param>
		/// <param name="score"></param>
		/// <returns>A newly created score</returns>
		/// <response code="201">Returns the newly created item</response>
		/// <response code="400">If the item is null</response>
		/// <response code="422">If the model is invalid</response>
		[HttpPost]
		[Authorize]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<IActionResult> CreateScoreForPlayer(Guid playerId, [FromBody] ScoreForCreationDto score)
		{
			//if(score == null)
			//	return BadRequest("ScoreForCreationDto object is null");

			//if(!ModelState.IsValid)
			//	return UnprocessableEntity(ModelState);

			var scoreToReturn = await _service.ScoreService.CreateScoreForPlayer(playerId, score, trackChanges: false);

			return CreatedAtRoute("GetScoreForPlayer", new { playerId, id = scoreToReturn.Id }, scoreToReturn);
		}

		//Update a score for a player.
		/// <summary>
		/// Updates a score for player
		/// </summary>
		/// <param name="playerId"></param>
		/// <param name="id"></param>
		/// <param name="score"></param>
		/// <returns>A newly updated score</returns>
		/// <response code="201">Returns the newly created item</response>
		/// <response code="400">If the item is null</response>
		/// <response code="422">If the model is invalid</response>

		[HttpPut("{id:guid}")] //api/players/{playerId}/scores/{id}
		[Authorize]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<IActionResult> UpdateScoreForPlayer(Guid playerId, Guid id, [FromBody] ScoreForUpdateDto score)
		{
			//if(score == null)
			//	return BadRequest("ScoreForUpdateDto object is null");

			//if(!ModelState.IsValid)
			//	return UnprocessableEntity(ModelState);

			await _service.ScoreService.UpdateScoreForPlayer(playerId, id, score, playerTrackChanges: false, scoreTrackChanges: true);

			return NoContent();
		}

		//Partially update a score for a player.
		/// <summary>
		/// Patch update a score for player
		/// </summary>
		/// <param name="playerId"></param>
		/// <param name="id"></param>
		/// <param name="patchDoc"></param>
		/// <returns>A newly updated score</returns>
		/// <response code="201">Returns the newly created item</response>
		/// <response code="400">If the item is null</response>
		/// <response code="422">If the model is invalid</response>
		[HttpPatch("{id:guid}")] //api/players/{playerId}/scores/{id}
		[Authorize]
		public async Task<IActionResult> PartiallyUpdateScoreForPlayer(Guid playerId, Guid id, [FromBody] JsonPatchDocument<ScoreForUpdateDto> patchDoc)
		{
			if(patchDoc == null)
				return BadRequest("patchDoc object is null");

			var result = await _service.ScoreService.GetScoreForPlayerPatch(playerId, id, playerTrackChanges: false, scoreTrackChanges: true);

			patchDoc.ApplyTo(result.scoreToPatch, ModelState);

			TryValidateModel(result.scoreToPatch);

			if(!ModelState.IsValid)
				return UnprocessableEntity(ModelState);

			await _service.ScoreService.SaveChangesForPatch(result.scoreToPatch, result.scoreEntity);

			return NoContent();
		}
	}	

}
