using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
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

		public async Task<Score> GetScores(Guid playerId, bool trackChanges) =>
			await FindByCondition(s => s.PlayerId.Equals(playerId), trackChanges)
			.SingleOrDefaultAsync();

		public async Task<Score> GetScore(Guid playerId, Guid Id, bool trackChanges) =>
			await FindByCondition(s => s.PlayerId.Equals(playerId) && s.Id.Equals(Id), trackChanges)
			.SingleOrDefaultAsync(); 

		public void CreateScoreForPlayer(Guid playerId, Score score)
		{
			score.PlayerId = playerId;
			Create(score);
		}
	}	
}
