using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
	//The repository manager class is responsible for managing the repositories. It is a wrapper class that exposes the repositories to the rest of the application.
	public sealed class RepositoryManager : IRepositoryManager
	{
		private readonly RepositoryContext _repositoryContext;
		private readonly Lazy<IPlayerRepository> _playerRepository;
		private readonly Lazy<IScoreRepository> _scoreRepository;
		public RepositoryManager(RepositoryContext repositoryContext)
		{
			_repositoryContext = repositoryContext;

			_playerRepository = new Lazy<IPlayerRepository>(() => 
			new	PlayerRepository(repositoryContext));

			_scoreRepository = new Lazy<IScoreRepository>(() => 
			new	ScoreRepository(repositoryContext));
		}
		public IPlayerRepository Player => _playerRepository.Value;
		public IScoreRepository Score => _scoreRepository.Value;

		//The repository manager class also exposes a Save method that saves all changes made to the database.
		public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();

	}
}
