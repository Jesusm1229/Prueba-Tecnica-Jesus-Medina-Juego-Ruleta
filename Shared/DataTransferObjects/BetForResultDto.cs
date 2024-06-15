using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
	public record BetForResultDto (			
		 int NewScore,
		 int? WinnerNumber,
		 string WinnerColor,
		 bool DidIWin
		);		
	
}
