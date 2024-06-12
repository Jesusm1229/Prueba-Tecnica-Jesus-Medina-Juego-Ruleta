using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
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
		private readonly Lazy<IAuthenticationService>  _authenticationService;

		public ServiceManager(
			IRepositoryManager repositoryManager, 
			ILoggerManager loggerManager, 
			IMapper mapper,
			UserManager<Player> userManager,
			IConfiguration configuration)
			
		{
			_playerService = new Lazy<IPlayerService>(() => 
			new PlayerService(repositoryManager, loggerManager, mapper));

			_scoreService = new Lazy<IScoreService>(() => 
			new ScoreService(repositoryManager, loggerManager, mapper));

			_authenticationService = new Lazy<IAuthenticationService>(() =>
			new AuthenticationService(loggerManager, mapper, userManager, configuration));
			
		}

		public IPlayerService PlayerService => _playerService.Value;
		public IScoreService ScoreService => _scoreService.Value;
		public IAuthenticationService AuthenticationService => _authenticationService.Value;

	
	}
}
