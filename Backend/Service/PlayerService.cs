using AutoMapper;
using Contracts;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Exceptions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Service
{
	internal sealed class PlayerService: IPlayerService
	{
		private readonly IRepositoryManager _repository;

		private readonly ILoggerManager _logger;

		private readonly IMapper _mapper;

		public  PlayerService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
		{
			_repository = repository;
			_logger = logger;
			_mapper = mapper;
		}

		//Method to get player and check if it exists. Reusable
		private async Task<Player> GetPlayerAndCheckIfItExists(Guid id, bool trackChanges)
		{
			var player = await _repository.Player.GetPlayer(id, trackChanges);

			if (player is null)
				throw new PlayerNotFoundException(id);

			return player;
		}



		public async Task<IEnumerable<PlayerDto>> GetAllPlayers(bool trackChanges)
		{
				var players = await _repository.Player.GetAllPlayers(trackChanges);

				//Mapper
				var playersDto = _mapper.Map<IEnumerable<PlayerDto>>(players);

				return playersDto;
			

		}

		public async Task<IEnumerable<PlayerWScoreDto>> GetTopPlayers(bool trackChanges)
		{
			var players = await _repository.Player.GetTopPlayers(trackChanges);

			//Mapper
			var playersDto = _mapper.Map<IEnumerable<PlayerWScoreDto>>(players);

			return playersDto;
		}

		public async Task<IEnumerable<PlayerWScoreDto>> GetPlayersWithScore(bool trackChanges)
		{
			var players = await _repository.Player.GetPlayersWithScore(trackChanges);

			//Mapper
			var playersDto = _mapper.Map<IEnumerable<PlayerWScoreDto>>(players);

			return playersDto;
		}

		public async Task<PlayerDto> GetPlayer(Guid id, bool trackChanges)
		{
			var player = await GetPlayerAndCheckIfItExists(id, trackChanges);

			//Check if null
			if (player == null)
			{
				throw new PlayerNotFoundException(id);
			}

			//Mapper
			var playerDto = _mapper.Map<PlayerDto>(player);

			return playerDto;
		}

		public async Task<PlayerDto> GetPlayerByUsername(string username, bool trackChanges)
		{
			var player = await _repository.Player.GetPlayerByUsername(username, trackChanges);

			//Check if null
			if (player == null)
			{
				throw new PlayerNotFoundException(username);
			}

			//Mapper
			var playerDto = _mapper.Map<PlayerDto>(player);

			return playerDto;
		}


		public async Task<PlayerDto> CreatePlayer(PlayerForCreationDto player)
		{
			//Find player by name
			//CheckIfPlayerExists TO DO


			//Mapper
			var playerEntity =  _mapper.Map<Player>(player);

			_repository.Player.CreatePlayer(playerEntity);

			await _repository.SaveAsync(); //Save to database

			//Now we map the player entity to a player dto to return it to controller
			var playerToReturn = _mapper.Map<PlayerDto>(playerEntity);

			return playerToReturn;
		}
	}
}
