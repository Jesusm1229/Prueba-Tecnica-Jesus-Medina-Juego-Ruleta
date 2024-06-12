using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Repository
{
	public class RepositoryContext : IdentityDbContext<Player, IdentityRole<Guid>, Guid>
	{
		public RepositoryContext(DbContextOptions options)
		: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{	
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new PlayerConfiguration());
			modelBuilder.ApplyConfiguration(new ScoreConfiguration());
		}

		public DbSet<Player> Players { get; set; }
		public DbSet<Score> Scores { get; set; }

	}

}