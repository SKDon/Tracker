using Tracker.DataAccess.Contracts.Contracts;
using Tracker.DataAccess.Contracts.Enums;

namespace Tracker.Jobs.Helpers.Abstract
{
	public interface IRecipientsFacade
	{
		RecipientData[] GetRecipients(EventType type, EventDataForEntity data);
	}
}