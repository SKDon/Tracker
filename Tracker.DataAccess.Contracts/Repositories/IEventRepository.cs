using Tracker.DataAccess.Contracts.Contracts;
using Tracker.DataAccess.Contracts.Enums;

namespace Tracker.DataAccess.Contracts.Repositories
{
	public interface IEventRepository
	{
		void Add(int partitionId, long? userId, EventType type, EventState state, byte[] data);
		EventData GetNext(EventType type, int partitionId);
		void SetState(long id, EventState state);
		void Delete(long id);
	}
}