using Entities.Models;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
	public interface IScoreService
	{
		Task<IEnumerable<ScoreDto>> GetScores(Guid playerId, bool trackChanges);

		Task<ScoreDto> GetScore(Guid playerId, Guid Id, bool trackChanges);

		Task<ScoreDto> CreateScoreForPlayer(Guid playerId, ScoreForCreationDto scoreForCreationDto, bool trackChanges);
	
		Task UpdateScoreForPlayer(Guid playerId, Guid Id, ScoreForUpdateDto scoreForUpdateDto, bool playerTrackChanges, bool scoreTrackChanges);
	
		Task<(ScoreForUpdateDto scoreToPatch, Score scoreEntity)> GetScoreForPlayerPatch(Guid playerId, Guid id, bool playerTrackChanges, bool scoreTrackChanges);
		Task SaveChangesForPatch(ScoreForUpdateDto scoreToPatch, Score scoreEntity);
	}
}
