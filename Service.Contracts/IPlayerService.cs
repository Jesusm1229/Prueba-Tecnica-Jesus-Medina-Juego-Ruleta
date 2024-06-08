using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
	public interface IPlayerService
	{
		IEnumerable<Player> GetAllPlayers(bool trackChanges);
	}
}
