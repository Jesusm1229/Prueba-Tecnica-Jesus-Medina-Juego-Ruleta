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
	public class ColorConfiguration : IEntityTypeConfiguration<Color>
	{
		public void Configure(EntityTypeBuilder<Color> builder)
		{
			builder.HasData
				(					
					new Color
					{
						Id = new Guid("21d029a4-ac35-47b7-b3ac-a380b0ea355e"),
						Name = "red"
					},
					new Color
					{
						Id = new Guid("2da7569e-6641-45e1-9d55-44fe86338902"),
						Name = "black"
					}
				);
		}
	}
}
