using Tracker.DataAccess.Contracts.Contracts;
using Tracker.DataAccess.Contracts.Enums;

namespace Tracker.Jobs.Core
{
	public interface IEventProcessor
	{
		void ProcessEvent(EventType type, EventData data);
	}
}