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
	public class PlayerConfiguration: IEntityTypeConfiguration<Player>
	{
		public void Configure(EntityTypeBuilder<Player> builder)
		{
			builder.HasData
			(
				new Player
				{
					Id = new Guid("b7e9e3a1-3e29-4e6f-8e1a-2f3b6e0b6e1f"),
					Name = "Keanu Reeves",
					//ScoreId = "b7e9e3a1-3e29-4e6f-8e1a-2f3b6e0b6e1f"
				},
				new Player
				{
					Id = new Guid("b7e9e3a1-3e29-4e6f-8e1a-2f3b6e0b6e1f"),
					Name = "Tom Cruise"
				}
			);		
		}
	}
}	

