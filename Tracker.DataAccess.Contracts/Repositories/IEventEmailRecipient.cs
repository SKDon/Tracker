using Tracker.DataAccess.Contracts.Enums;

namespace Tracker.DataAccess.Contracts.Repositories
{
	public interface IEventEmailRecipient
	{
		RoleType[] GetRecipientRoles(EventType eventType);
		void Set(EventType eventType, RoleType[] recipients);
	}
}