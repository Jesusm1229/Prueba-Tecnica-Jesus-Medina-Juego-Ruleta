using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
	public interface IPlayerRepository
	{
		//Get all player
		Task <IEnumerable<Player>> GetAllPlayers(bool trackChanges);

		//Get top players
		Task<IEnumerable<Player>> GetTopPlayers(bool trackChanges);

		Task<IEnumerable<Player>> GetPlayersWithScore(bool trackChanges);

		//GEt specific player
		Task<Player> GetPlayer(Guid playerId, bool trackChanges);

		Task<Player> GetPlayerByUsername(string username, bool trackChanges);		
			
		//Create player
		void CreatePlayer(Player player);		
	}
}
