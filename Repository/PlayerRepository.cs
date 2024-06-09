using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
	public class PlayerRepository: RepositoryBase<Player>, IPlayerRepository
	{
		public PlayerRepository(RepositoryContext repositoryContext)
		: base(repositoryContext)
		{
		}

		public IEnumerable<Player> GetAllPlayers(bool trackChanges) =>
			FindAll(trackChanges)
			.OrderBy(p => p.Name)
			.ToList();

		public Player GetPlayer(Guid playerId, bool trackChanges) =>
			FindByCondition(p => p.Id.Equals(playerId), trackChanges)
			.SingleOrDefault();

		public void CreatePlayer(Player player) => Create(player);
	}
	
}
