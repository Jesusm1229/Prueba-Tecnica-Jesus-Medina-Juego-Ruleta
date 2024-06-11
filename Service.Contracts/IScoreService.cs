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
		IEnumerable<ScoreDto> GetScores(Guid playerId, bool trackChanges);

		ScoreDto GetScore(Guid playerId, Guid Id, bool trackChanges);

		ScoreDto CreateScoreForPlayer(Guid playerId, ScoreForCreationDto scoreForCreationDto, bool trackChanges);
	
		void UpdateScoreForPlayer(Guid playerId, Guid Id, ScoreForUpdateDto scoreForUpdateDto, bool playerTrackChanges, bool scoreTrackChanges);
	
		(ScoreForUpdateDto scoreToPatch, Score scoreEntity) GetScoreForPlayerPatch(Guid playerId, Guid id, bool playerTrackChanges, bool scoreTrackChanges);
	
		void SaveChangesForPatch(ScoreForUpdateDto scoreToPatch, Score scoreEntity);
	}
}
