using Tracker.DataAccess.Contracts.Helpers;
using Tracker.ViewModels.Application;

namespace Tracker.Services.Abstract
{
	public interface IApplicationListPresenter
	{
		ApplicationListCollection List(string language, int? take = null, int skip = 0, Order[] groups = null,
			long? clientId = null, long? senderId = null, long? forwarderId = null, long? carrierId = null);
	}
}