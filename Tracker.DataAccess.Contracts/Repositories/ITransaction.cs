using System;

namespace Tracker.DataAccess.Contracts.Repositories
{
	public interface ITransaction : IDisposable
	{
		void Complete();
	}
}