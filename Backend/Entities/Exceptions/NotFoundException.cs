using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
	// This class is an abstract class that inherits from the Exception class.
	//Will help us to create custom exceptions for when a resource is not found, like player
	public abstract class NotFoundException : Exception
	{
		protected NotFoundException(string message)
		: base(message)
		{ }
	}
}
