using Tracker.DataAccess.Contracts.Helpers;
using Tracker.ViewModels.Application;

namespace Tracker.Services.Abstract
{
	public interface IApplicationGrouper
	{
		ApplicationGroup[] Group(ApplicationListItem[] models, OrderType[] groups, long? clientId = null,
			long? senderId = null, long? forwarderId = null, long? carrierId = null);
	}
}