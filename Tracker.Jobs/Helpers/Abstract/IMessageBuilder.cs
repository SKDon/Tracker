using Tracker.DataAccess.Contracts.Contracts;
using Tracker.DataAccess.Contracts.Enums;

namespace Tracker.Jobs.Helpers.Abstract
{
	public interface IMessageBuilder
	{
		EmailMessage[] Get(EventType type, EventData eventData);
	}
}