﻿using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
	public class PlayerRepository: RepositoryBase<Player>, IPlayerRepository
	{
		public PlayerRepository(RepositoryContext repositoryContext)
		: base(repositoryContext)
		{
		}

		public async Task<IEnumerable<Player>> GetAllPlayers(bool trackChanges) =>
			await FindAll(trackChanges)
			.OrderBy(p => p.UserName)
			.ToListAsync();

		public async Task<IEnumerable<Player>> GetTopPlayers(bool trackChanges) =>
			await FindAll(trackChanges)
			.Include(p => p.Score)
			.OrderByDescending(p => p.Score.Points)
			.Take(10)
			.ToListAsync();

		public async Task<IEnumerable<Player>> GetPlayersWithScore(bool trackChanges) =>
			await FindAll(trackChanges)
			.Include(p => p.Score)
			.OrderBy(p => p.UserName)
			.ToListAsync();

		public async Task <Player> GetPlayer(Guid playerId, bool trackChanges) =>
			await FindByCondition(p => p.Id.Equals(playerId), trackChanges)
			.SingleOrDefaultAsync();

		public async Task<Player> GetPlayerByUsername(string username, bool trackChanges) =>
			await FindByCondition(p => p.UserName.Equals(username), trackChanges)
			.SingleOrDefaultAsync();

		public void CreatePlayer(Player player) => Create(player);


	}
	
}
