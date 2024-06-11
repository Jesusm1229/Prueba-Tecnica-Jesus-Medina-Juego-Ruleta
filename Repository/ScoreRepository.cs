using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
	public class ScoreRepository : RepositoryBase<Score>, IScoreRepository
	{
		public ScoreRepository(RepositoryContext repositoryContext)
		: base(repositoryContext)
		{
		}

		public IEnumerable<Score> GetScores(Guid playerId, bool trackChanges) =>
			FindByCondition(s => s.PlayerId.Equals(playerId), trackChanges)
			.ToList();

		public Score GetScore(Guid playerId, Guid Id, bool trackChanges) =>
			FindByCondition(s => s.PlayerId.Equals(playerId) && s.Id.Equals(Id), trackChanges)
			.SingleOrDefault(); 

		public void CreateScoreForPlayer(Guid playerId, Score score)
		{
			score.PlayerId = playerId;
			Create(score);
		}
	}	
}
