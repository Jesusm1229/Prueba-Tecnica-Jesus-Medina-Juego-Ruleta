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

namespace Service
{
	internal sealed class PlayerService: IPlayerService
	{
		private readonly IRepositoryManager _repository;

		private readonly ILoggerManager _logger;

		private readonly IMapper _mapper;

		public PlayerService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
		{
			_repository = repository;
			_logger = logger;
			_mapper = mapper;
		}

		public IEnumerable<PlayerDto> GetAllPlayers(bool trackChanges)
		{
				var players = _repository.Player.GetAllPlayers(trackChanges);

				//Mapper
				var playersDto = _mapper.Map<IEnumerable<PlayerDto>>(players);

				return playersDto;
			

		}

		public PlayerDto GetPlayer(Guid id, bool trackChanges)
		{
			var player = _repository.Player.GetPlayer(id, trackChanges);

			//Check if null
			if (player == null)
			{
				throw new PlayerNotFoundException(id);
			}

			//Mapper
			var playerDto = _mapper.Map<PlayerDto>(player);

			return playerDto;
		}


		public PlayerDto CreatePlayer(PlayerForCreationDto player)
		{
			//Mapper
			var playerEntity = _mapper.Map<Player>(player);

			_repository.Player.CreatePlayer(playerEntity);

			_repository.Save(); //Save to database

			//Now we map the player entity to a player dto to return it to controller
			var playerToReturn = _mapper.Map<PlayerDto>(playerEntity);

			return playerToReturn;
		}
	}
}
