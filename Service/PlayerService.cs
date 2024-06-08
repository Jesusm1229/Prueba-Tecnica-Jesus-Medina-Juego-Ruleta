using Contracts;
using Entities.Models;
using Service.Contracts;
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

		public PlayerService(IRepositoryManager repository, ILoggerManager logger)
		{
			_repository = repository;
			_logger = logger;
		}

		public IEnumerable<Player> GetAllPlayers(bool trackChanges)
		{
			try
			{
				var players = _repository.Player.GetAllPlayers(trackChanges);

				return players;
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong in the {nameof(GetAllPlayers)}: {ex.Message}");

				throw;
			}

		}
	}
}
