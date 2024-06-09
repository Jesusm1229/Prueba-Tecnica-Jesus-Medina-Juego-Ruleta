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
	}
}
