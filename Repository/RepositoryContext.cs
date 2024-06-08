using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Repository
{
	public class RepositoryContext : DbContext
	{
		public RepositoryContext(DbContextOptions options)
		: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{	
			modelBuilder.ApplyConfiguration(new PlayerConfiguration());
			//modelBuilder.ApplyConfiguration(new ScoreConfiguration());
		}

		public DbSet<Player> Players { get; set; }
		public DbSet<Score> Scores { get; set; }

	}

}