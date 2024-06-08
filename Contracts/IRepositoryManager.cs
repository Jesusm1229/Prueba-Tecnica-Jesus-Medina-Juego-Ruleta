using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
	public interface IRepositoryManager
	{
		IPlayerRepository Player { get; }
		IScoreRepository Score { get; }

		// Add the Save method to the IRepositoryManager interface. Only save when all changes are made.
		void Save();
		
	}
}
