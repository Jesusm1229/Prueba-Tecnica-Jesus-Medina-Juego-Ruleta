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
			
		}
	}
}
