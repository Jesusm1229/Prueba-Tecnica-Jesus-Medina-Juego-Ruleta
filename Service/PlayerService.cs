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
			try
			{
				var players = _repository.Player.GetAllPlayers(trackChanges);

				//Mapper
				var playersDto = _mapper.Map<IEnumerable<PlayerDto>>(players);

				return playersDto;
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong in the {nameof(GetAllPlayers)}: {ex.Message}");

				throw;
			}

		}
	}
}
