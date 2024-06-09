using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
	internal sealed class ScoreService: IScoreService
	{
		private readonly IRepositoryManager _repository;
		private readonly ILoggerManager _logger;
		private readonly IMapper _mapper;
		public ScoreService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
		{
			_repository = repository;
			_logger = logger;
			_mapper = mapper;
		}

		public IEnumerable<ScoreDto> GetScores(Guid playerId, bool trackChanges)
		{
			var player = _repository.Player.GetPlayer(playerId, trackChanges);

			if(player is null)
			{
				throw new PlayerNotFoundException(playerId);
			}

			var scoresFromDb = _repository.Score.GetScores(playerId, trackChanges);

			var scoresDto = _mapper.Map<IEnumerable<ScoreDto>>(scoresFromDb);
			

			return scoresDto;
		}

		public ScoreDto GetScore(Guid playerId, Guid Id, bool trackChanges)
		{
			var player = _repository.Player.GetPlayer(playerId, trackChanges);

			if(player is null)
			{
				throw new PlayerNotFoundException(playerId);
			}

			var scoreFromDb = _repository.Score.GetScore(playerId, Id, trackChanges);

			if(scoreFromDb is null)
			{
				throw new ScoreNotFoundException(Id);
			}

			var scoreDto = _mapper.Map<ScoreDto>(scoreFromDb);

			return scoreDto;
		}

	}
}
