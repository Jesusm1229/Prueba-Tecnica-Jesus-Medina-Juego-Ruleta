using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configuration
{
	public class ScoreConfiguration : IEntityTypeConfiguration<Score>
	{
		public void Configure(EntityTypeBuilder<Score> builder)
		{
			builder.HasData
			(
				new Score
				{
					Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
					Points = 100,
					Player = new Player
					{
						Id = new Guid("b7e9e3a1-3e29-4e6f-8e1a-2f3b6e0b6e1f"),
						Name = "Keanu Reeves"
					}
				},
				new Score
				{
					Id = new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"),
					Points = 200,
					Player = new Player
					{
						Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
						Name = "Tom Cruise"
					}				
				}
			);
		}
	}
}

