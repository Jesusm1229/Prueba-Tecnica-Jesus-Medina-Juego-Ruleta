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
		IEnumerable<Player> GetAllPlayers(bool trackChanges);
		//GEt specific player
		Player GetPlayer(Guid playerId, bool trackChanges);
		//Create player
		void CreatePlayer(Player player);
	}
}
