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

		public async Task <IEnumerable<ScoreDto>> GetScores(Guid playerId, bool trackChanges)
		{
			var player = await _repository.Player.GetPlayer(playerId, trackChanges);

			if(player is null)
			{
				throw new PlayerNotFoundException(playerId);
			}

			var scoresFromDb = _repository.Score.GetScores(playerId, trackChanges);

			var scoresDto = _mapper.Map<IEnumerable<ScoreDto>>(scoresFromDb);
			

			return scoresDto;
		}

		public async Task<ScoreDto> GetScore(Guid playerId, Guid Id, bool trackChanges)
		{
			var player = await _repository.Player.GetPlayer(playerId, trackChanges);

			if(player is null)
			{
				throw new PlayerNotFoundException(playerId);
			}

			var scoreFromDb = await _repository.Score.GetScore(playerId, Id, trackChanges);

			if(scoreFromDb is null)
			{
				throw new ScoreNotFoundException(Id);
			}

			var scoreDto = _mapper.Map<ScoreDto>(scoreFromDb);

			return scoreDto;
		}


		// CreateScoreForPlayer method
		public async Task<ScoreDto> CreateScoreForPlayer(Guid playerId, ScoreForCreationDto scoreForCreationDto, bool trackChanges)
		{
			// Get the player
			var player = await _repository.Player.GetPlayer(playerId, trackChanges);

			if(player is null)
			{
				throw new PlayerNotFoundException(playerId);
			}

			var scoreEntity = _mapper.Map<Score>(scoreForCreationDto);

			_repository.Score.CreateScoreForPlayer(playerId, scoreEntity);

			await _repository.SaveAsync();

			var scoreDto = _mapper.Map<ScoreDto>(scoreEntity);

			return scoreDto;
		}

		//UpdateScoreForPlayer method
		public async Task UpdateScoreForPlayer(Guid playerId, Guid id, ScoreForUpdateDto scoreForUpdate, bool playerTrackChanges, bool scoreTrackChanges)
		{
			var player = await _repository.Player.GetPlayer(playerId, playerTrackChanges);

			if (player is null)
			{
				throw new PlayerNotFoundException(playerId);
			}			

			var scoreEntity = await _repository.Score.GetScore(playerId, id, scoreTrackChanges);


			if (scoreEntity is null)
			{
				throw new ScoreNotFoundException(id);
			}

			_mapper.Map(scoreForUpdate, scoreEntity);

			await _repository.SaveAsync();
		}


		//GetScoreForPlayerPatch method
		public async Task<(ScoreForUpdateDto scoreToPatch, Score scoreEntity)> GetScoreForPlayerPatch(Guid playerId, Guid id, bool playerTrackChanges, bool scoreTrackChanges)
		{
			var player = await _repository.Player.GetPlayer(playerId, playerTrackChanges);

			if (player is null)
			{
				throw new PlayerNotFoundException(playerId);
			}

			var scoreEntity = await _repository.Score.GetScore(playerId, id, scoreTrackChanges);

			if (scoreEntity is null)
			{
				throw new ScoreNotFoundException(id);
			}

			var scoreToPatch = _mapper.Map<ScoreForUpdateDto>(scoreEntity);

			return (scoreToPatch, scoreEntity);
		}

		public async Task SaveChangesForPatch(ScoreForUpdateDto scoreToPatch, Score scoreEntity)
		{
			_mapper.Map(scoreToPatch, scoreEntity);

			await _repository.SaveAsync();
		}
	}
}
