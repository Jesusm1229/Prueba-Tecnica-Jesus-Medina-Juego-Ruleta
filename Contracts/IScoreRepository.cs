using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
	public interface IScoreRepository
	{
		IEnumerable<Score> GetScores(Guid playerId, bool trackChanges);

		Score GetScore(Guid playerId, Guid Id, bool trackChanges);
	}
}
