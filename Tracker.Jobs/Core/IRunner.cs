using System.Threading;

namespace Tracker.Jobs.Core
{
	public interface IRunner
	{
		void Run(CancellationTokenSource tokenSource);
		string Name { get; }
	}
}