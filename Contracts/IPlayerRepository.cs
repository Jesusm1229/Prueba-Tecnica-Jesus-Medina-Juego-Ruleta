﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
	public interface IPlayerRepository
	{
		IEnumerable<Player> GetAllPlayers(bool trackChanges);

		Player GetPlayer(Guid playerId, bool trackChanges);
	}
}
