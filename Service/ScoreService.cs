using AutoMapper;
using Contracts;
using Entities.Exceptions;
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


		// CreateScoreForPlayer method
		public ScoreDto CreateScoreForPlayer(Guid playerId, ScoreForCreationDto scoreForCreationDto, bool trackChanges)
		{
			// Get the player
			var player = _repository.Player.GetPlayer(playerId, trackChanges);

			if(player is null)
			{
				throw new PlayerNotFoundException(playerId);
			}

			var scoreEntity = _mapper.Map<Score>(scoreForCreationDto);

			_repository.Score.CreateScoreForPlayer(playerId, scoreEntity);

			_repository.Save();

			var scoreDto = _mapper.Map<ScoreDto>(scoreEntity);

			return scoreDto;
		}

		//UpdateScoreForPlayer method
		public void UpdateScoreForPlayer(Guid playerId, Guid id, ScoreForUpdateDto scoreForUpdate, bool playerTrackChanges, bool scoreTrackChanges)
		{
			var player = _repository.Player.GetPlayer(playerId, playerTrackChanges);

			if (player is null)
			{
				throw new PlayerNotFoundException(playerId);
			}

			var scoreEntity = _repository.Score.GetScore(playerId, id, scoreTrackChanges);


			if (scoreEntity is null)
			{
				throw new ScoreNotFoundException(id);
			}

			_mapper.Map(scoreForUpdate, scoreEntity);

			_repository.Save();
		}


		//GetScoreForPlayerPatch method
		public (ScoreForUpdateDto scoreToPatch, Score scoreEntity) GetScoreForPatch(Guid playerId, Guid id, bool playerTrackChanges, bool scoreTrackChanges)
		{
			var player = _repository.Player.GetPlayer(playerId, playerTrackChanges);

			if (player is null)
			{
				throw new PlayerNotFoundException(playerId);
			}

			var scoreEntity = _repository.Score.GetScore(playerId, id, scoreTrackChanges);

			if (scoreEntity is null)
			{
				throw new ScoreNotFoundException(id);
			}

			var scoreToPatch = _mapper.Map<ScoreForUpdateDto>(scoreEntity);

			return (scoreToPatch, scoreEntity);
		}

		public void SaveChangesForPatch(ScoreForUpdateDto scoreToPatch, Score scoreEntity)
		{
			_mapper.Map(scoreToPatch, scoreEntity);

			_repository.Save();
		}
	}
}
