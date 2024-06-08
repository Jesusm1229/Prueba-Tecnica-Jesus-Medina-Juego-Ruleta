using AutoMapper;
using Contracts;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
	public sealed class ServiceManager: IServiceManager
	{
		private readonly Lazy<IPlayerService> _playerService;
		private readonly Lazy<IScoreService> _scoreService;

		public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
		{
			_playerService = new Lazy<IPlayerService>(() => new PlayerService(repositoryManager, loggerManager, mapper));
			_scoreService = new Lazy<IScoreService>(() => new ScoreService(repositoryManager, loggerManager, mapper));
			
		}

		public IPlayerService PlayerService => _playerService.Value;
		public IScoreService ScoreService => _scoreService.Value;
	
	}
}
