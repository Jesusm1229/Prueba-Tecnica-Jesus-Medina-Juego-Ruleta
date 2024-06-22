using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace WinWheel
{
	public class MappingProfile: Profile
	{
		public MappingProfile()
		{
			CreateMap<Player, PlayerDto>();

			CreateMap<Score, ScoreDto>();

			CreateMap<PlayerForCreationDto, Player>();//create player

			CreateMap<ScoreForCreationDto, Score>();//create score

			CreateMap<ScoreForUpdateDto, Score>();

			CreateMap<ScoreForUpdateDto, Score>().ReverseMap();//PATCH METHOD

			CreateMap<Player, PlayerWScoreDto>();


			
		}
	}
}
