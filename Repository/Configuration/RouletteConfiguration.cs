using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
	public class RouletteConfiguration : IEntityTypeConfiguration<Roulette>
	{
		public void Configure(EntityTypeBuilder<Roulette> builder)
		{
			builder.HasData
			(
				new Roulette
				{
					Id = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c"),

				}
			);

		}
	}

}
