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

		public PlayerDto GetPlayer(Guid playerId, bool trackChanges)
		{
			var player = _repository.Player.GetPlayer(playerId, trackChanges);

			//Check if null
			if (player == null)
			{
				throw new PlayerNotFoundException(playerId);
			}

			//Mapper
			var playerDto = _mapper.Map<PlayerDto>(player);

			return playerDto;
		}
	}
}
