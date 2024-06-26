﻿using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
	public interface IPlayerService
	{
		Task<IEnumerable<PlayerDto>> GetAllPlayers(bool trackChanges);

		Task<IEnumerable<PlayerWScoreDto>> GetTopPlayers(bool trackChanges);

		Task<IEnumerable<PlayerWScoreDto>> GetPlayersWithScore(bool trackChanges);

		Task <PlayerDto> GetPlayer(Guid playerId, bool trackChanges);

		Task<PlayerDto> GetPlayerByUsername(string username, bool trackChanges);

		Task<PlayerDto> CreatePlayer(PlayerForCreationDto player);


	}
}
