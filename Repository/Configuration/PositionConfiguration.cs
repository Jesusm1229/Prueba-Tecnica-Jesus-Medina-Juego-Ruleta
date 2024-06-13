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
	public class PositionConfiguration : IEntityTypeConfiguration<Position>
	{
		public void Configure(EntityTypeBuilder<Position> builder)
		{
			builder.HasData
			(
				new Position
				{
					Id = new Guid("4c82aeb5-fe97-4e42-b5d3-08974c02f8c7"),
					Number = 0,
					ColorId = new Guid("2da7569e-6641-45e1-9d55-44fe86338902"),//Black
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("b6d4270e-a2d9-4410-a5ff-3df3aa07dafa"),
					Number = 1,					
					ColorId = new Guid("21d029a4-ac35-47b7-b3ac-a380b0ea355e"),//Red
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("de6d5485-f489-41bc-9f7e-b401ad3f0e08"),
					Number = 2,
					ColorId = new Guid("2da7569e-6641-45e1-9d55-44fe86338902"),//Black
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("e62b6ce4-abdf-4c44-a169-1f52aabb1bba"),
					Number = 3,
					ColorId = new Guid("21d029a4-ac35-47b7-b3ac-a380b0ea355e"),//Red
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("5c1e913c-4a4b-4b7b-9985-20314444859a"),
					Number = 4,	
					ColorId = new Guid("2da7569e-6641-45e1-9d55-44fe86338902"),//Black
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("3bc68cb9-1698-4057-be3d-09d49f126738"),
					Number = 5,
					ColorId = new Guid("21d029a4-ac35-47b7-b3ac-a380b0ea355e"),//Red
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("05e67785-80f6-4eef-9ac1-a949fc5545e5"),
					Number = 6,	
					ColorId = new Guid("2da7569e-6641-45e1-9d55-44fe86338902"),//Black
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("dec761d6-4c1e-495c-baec-0811b2a14883"),
					Number = 7,
					ColorId = new Guid("21d029a4-ac35-47b7-b3ac-a380b0ea355e"),//Red
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("13ca953f-4f56-4a54-858d-0b691313ac02"),
					Number = 8,	
					ColorId = new Guid("2da7569e-6641-45e1-9d55-44fe86338902"),//Black
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("37ed1a48-05f1-41e5-be83-5125a679c1f4"),
					Number = 9,
					ColorId = new Guid("21d029a4-ac35-47b7-b3ac-a380b0ea355e"),//Red
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("d2b66a01-fd00-4d07-82ca-f032c90b8fde"),
					Number = 10,	
					ColorId = new Guid("2da7569e-6641-45e1-9d55-44fe86338902"),//Black
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("bc5f3292-1c2a-471e-b1ac-6a7c7aaa36b0"),
					Number = 11,
					ColorId = new Guid("2da7569e-6641-45e1-9d55-44fe86338902"),//Black
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("a2fa8796-9b32-4f99-8b82-ef1f368044dc"),
					Number = 12,	
					ColorId = new Guid("21d029a4-ac35-47b7-b3ac-a380b0ea355e"),//Red
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("004f4a3a-b791-4113-99a5-e41093609e0f"),
					Number = 13,
					ColorId = new Guid("2da7569e-6641-45e1-9d55-44fe86338902"),//Black
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("524fdad5-4c82-458e-8fb0-2c1962eafc77"),
					Number = 14,	
					ColorId = new Guid("21d029a4-ac35-47b7-b3ac-a380b0ea355e"),//Red
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("794e4f00-a4e0-4a5d-a2d8-a04b6ea5a372"),
					Number = 15,
					ColorId = new Guid("2da7569e-6641-45e1-9d55-44fe86338902"),//Black
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("264a04a3-9e31-4876-a628-3a584bd3804f"),
					Number = 16,	
					ColorId = new Guid("21d029a4-ac35-47b7-b3ac-a380b0ea355e"),//Red
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("7f1cbb01-de93-41f2-8995-f9e311adb32a"),
					Number = 17,
					ColorId = new Guid("2da7569e-6641-45e1-9d55-44fe86338902"),//Black
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("45fd2e34-c84b-4667-b175-5af27f08fd11"),
					Number = 18,	
					ColorId = new Guid("21d029a4-ac35-47b7-b3ac-a380b0ea355e"),//Red
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("f122d0c4-8f6f-495e-997b-08ce0a7ed730"),
					Number = 19,
					ColorId = new Guid("21d029a4-ac35-47b7-b3ac-a380b0ea355e"),//Red
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("4b8d8e59-783a-4d7a-9d31-cc54186c8551"),
					Number = 20,	
					ColorId = new Guid("2da7569e-6641-45e1-9d55-44fe86338902"),//Black
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("98326633-e5cf-4a40-a4a1-d7ca2d795a20"),
					Number = 21,
					ColorId = new Guid("21d029a4-ac35-47b7-b3ac-a380b0ea355e"),//Red
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("91021e3a-a20c-4481-846a-9550ec1b142a"),
					Number = 22,	
					ColorId = new Guid("2da7569e-6641-45e1-9d55-44fe86338902"),//Black
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("004a0426-352b-424b-bd0f-8367926bcd9c"),
					Number = 23,
					ColorId = new Guid("21d029a4-ac35-47b7-b3ac-a380b0ea355e"),//Red
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("fdb84c5e-2bd8-4f1e-b7c9-8d7a0f4f416c"),
					Number = 24,	
					ColorId = new Guid("2da7569e-6641-45e1-9d55-44fe86338902"),//Black
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("50c361dd-bb20-4456-abd3-d9d637167803"),
					Number = 25,
					ColorId = new Guid("21d029a4-ac35-47b7-b3ac-a380b0ea355e"),//Red
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("51cfba07-6ef4-4d45-b8be-93fd15151ed5"),
					Number = 26,	
					ColorId = new Guid("2da7569e-6641-45e1-9d55-44fe86338902"),//Black
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("f83b88a5-57e4-4a56-b685-51c03464fc92"),
					Number = 27,
					ColorId = new Guid("21d029a4-ac35-47b7-b3ac-a380b0ea355e"),//Red
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("39c9ade8-3c67-4083-8f76-4aa6028856ab"),
					Number = 28,	
					ColorId = new Guid("2da7569e-6641-45e1-9d55-44fe86338902"),//Black
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("7338a90a-b13b-482b-b306-9893b3b1ece1"),
					Number = 29,
					ColorId = new Guid("2da7569e-6641-45e1-9d55-44fe86338902"),//Black
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				}
				,
				new Position
				{
					Id = new Guid("cc11efa3-82f2-431a-a928-b0288d46d6b9"),
					Number = 30,
					ColorId = new Guid("21d029a4-ac35-47b7-b3ac-a380b0ea355e"),//Red
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("44b63fbe-f49d-4495-88bc-5e6d221655f8"),
					Number = 31,
					ColorId = new Guid("2da7569e-6641-45e1-9d55-44fe86338902"),//Black
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("5be16b2c-1b85-43bc-a30d-c9b8029adc4a"),
					Number = 32,
					ColorId = new Guid("21d029a4-ac35-47b7-b3ac-a380b0ea355e"),//Red
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("4ed335e7-31af-4201-bae6-1c8946163d03"),
					Number = 33,
					ColorId = new Guid("2da7569e-6641-45e1-9d55-44fe86338902"),//Black
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("271c9fdb-c203-40f1-bc89-1b1ca0bc21b8"),
					Number = 34,
					ColorId = new Guid("21d029a4-ac35-47b7-b3ac-a380b0ea355e"),//Red
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("1145aaa0-61e0-43cf-81b1-aed7e1970eb0"),
					Number = 35,
					ColorId = new Guid("2da7569e-6641-45e1-9d55-44fe86338902"),//Black
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				},
				new Position
				{
					Id = new Guid("a08d5a0e-b245-4c7a-b51d-975bf4fe9a33"),
					Number = 36,
					ColorId = new Guid("21d029a4-ac35-47b7-b3ac-a380b0ea355e"),//Red
					RouletteId = new Guid("05b937f4-2625-4061-abcd-094cf6e2e46c")
				}
			);
		}
	}
}
