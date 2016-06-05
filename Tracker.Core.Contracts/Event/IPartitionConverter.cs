namespace Tracker.Core.Contracts.Event
{
	public interface IPartitionConverter
	{
		int GetKey(long id);
	}
}
